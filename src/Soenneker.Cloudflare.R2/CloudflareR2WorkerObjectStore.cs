using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Cloudflare.R2.Abstract;

namespace Soenneker.Cloudflare.R2;

public sealed class CloudflareR2WorkerObjectStore : ICloudflareR2ObjectStore
{
    private readonly System.Net.Http.HttpClient _http;
    private readonly Uri _endpoint;
    private readonly string _apiKey;

    public CloudflareR2WorkerObjectStore(System.Net.Http.HttpClient httpClient, Uri endpoint, string apiKey)
    {
        ArgumentNullException.ThrowIfNull(httpClient);
        ArgumentNullException.ThrowIfNull(endpoint);
        ArgumentException.ThrowIfNullOrWhiteSpace(apiKey);
        if (!endpoint.IsAbsoluteUri || endpoint.Scheme != "https" || endpoint.UserInfo.Length != 0 || endpoint.Query.Length != 0 || endpoint.Fragment.Length != 0)
            throw new ArgumentException("The R2 gateway must be an absolute HTTPS URL without credentials, query or fragment.", nameof(endpoint));
        _http = httpClient;
        _endpoint = new Uri(endpoint.AbsoluteUri.TrimEnd('/') + "/");
        _apiKey = apiKey;
    }

    private HttpRequestMessage Request(HttpMethod method, string suffix)
    {
        var request = new HttpRequestMessage(method, new Uri(_endpoint.AbsoluteUri + suffix));
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        return request;
    }

    private static string Key(string key)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        // Encode segments individually so object-key slashes survive, but never allow URI dot-segment normalization.
        string[] parts = key.Split('/');
        for (int i = 0; i < parts.Length; i++)
        {
            if (parts[i] is "" or "." or "..") throw new ArgumentException("Object keys cannot contain empty or dot segments.", nameof(key));
            parts[i] = Uri.EscapeDataString(parts[i]);
        }
        return string.Join('/', parts);
    }

    private static string ETag(HttpResponseMessage response) =>
        response.Headers.ETag is { IsWeak: false } tag ? tag.ToString() : throw new InvalidDataException("R2 gateway did not return a strong ETag.");

    public async ValueTask<CloudflareR2Object?> Read(string key, int maxBytes = 16 * 1024 * 1024, CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxBytes);
        using HttpRequestMessage request = Request(HttpMethod.Get, Key(key));
        using HttpResponseMessage response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) return null;
        response.EnsureSuccessStatusCode();
        string etag = ETag(response);
        await using Stream input = await response.Content.ReadAsStreamAsync(cancellationToken);
        using var output = new MemoryStream();
        var buffer = new byte[Math.Min(maxBytes, 81920)];
        int count;
        while ((count = await input.ReadAsync(buffer, cancellationToken)) != 0)
        {
            if (output.Length + count > maxBytes) throw new InvalidDataException("R2 object exceeds the configured byte limit.");
            output.Write(buffer, 0, count);
        }
        return new CloudflareR2Object(output.ToArray(), etag);
    }

    public async ValueTask<string?> Write(string key, ReadOnlyMemory<byte> content, string? expectedETag = null, CancellationToken cancellationToken = default)
    {
        using HttpRequestMessage request = Request(HttpMethod.Put, Key(key));
        request.Content = new ReadOnlyMemoryContent(content);
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        if (expectedETag is null) request.Headers.IfNoneMatch.Add(EntityTagHeaderValue.Any);
        else
        {
            EntityTagHeaderValue expected = EntityTagHeaderValue.Parse(expectedETag);
            if (expected.IsWeak || expected.Tag == "*")
                throw new ArgumentException("A conditional write requires a strong object ETag, not a wildcard.", nameof(expectedETag));
            request.Headers.IfMatch.Add(expected);
        }
        using HttpResponseMessage response = await _http.SendAsync(request, cancellationToken);
        if (response.StatusCode == HttpStatusCode.PreconditionFailed) return null;
        response.EnsureSuccessStatusCode();
        return ETag(response);
    }

    public async ValueTask<CloudflareR2ObjectPage> List(string prefix, string? cursor = null, CancellationToken cancellationToken = default)
    {
        using HttpRequestMessage request = Request(HttpMethod.Get, "?prefix=" + Uri.EscapeDataString(prefix) +
            (cursor is null ? "" : "&cursor=" + Uri.EscapeDataString(cursor)));
        using HttpResponseMessage response = await _http.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
        await using Stream content = await response.Content.ReadAsStreamAsync(cancellationToken);
        return await JsonSerializer.DeserializeAsync(content, CloudflareR2ObjectJsonContext.Default.CloudflareR2ObjectPage, cancellationToken)
            ?? throw new InvalidDataException("R2 gateway returned a null object listing.");
    }
}
