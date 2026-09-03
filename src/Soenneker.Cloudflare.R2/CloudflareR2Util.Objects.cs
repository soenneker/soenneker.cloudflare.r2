using Soenneker.Cloudflare.OpenApiClient;
using Soenneker.Cloudflare.OpenApiClient.Accounts.Item.R2.Buckets.Item.Objects;
using Soenneker.Cloudflare.OpenApiClient.Models;
using Soenneker.Enums.JsonOptions;
using Soenneker.Extensions.Task;
using Soenneker.Extensions.ValueTask;
using Soenneker.Utils.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Cloudflare.R2;

public sealed partial class CloudflareR2Util
{
    public async ValueTask<R2ListObjects200?> ListObjects(string accountId, string bucketName,
        Action<ObjectsRequestBuilder.ObjectsRequestBuilderGetQueryParameters>? configureQuery = null,
        string? apiKey = null, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await GetClient(apiKey, cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].Objects.GetAsync(
            config => configureQuery?.Invoke(config.QueryParameters), cancellationToken).NoSync();
    }

    public async ValueTask<Stream?> GetObject(string accountId, string bucketName, string objectKey, string? apiKey = null, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await GetClient(apiKey, cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].Objects[objectKey].GetAsync(cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<R2PutObject200?> PutObject(string accountId, string bucketName, string objectKey, Stream content,
        string? contentType = null, string? apiKey = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(content);
        if (!content.CanRead)
            throw new ArgumentException("The content stream must be readable.", nameof(content));

        CloudflareOpenApiClient client = await GetClient(apiKey, cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].Objects[objectKey].PutAsync(content, config =>
        {
            if (!string.IsNullOrWhiteSpace(contentType))
                config.Headers.Add("Content-Type", contentType);
        }, cancellationToken).NoSync();
    }

    public async ValueTask<R2PutObject200?> PutObject(string accountId, string bucketName, string objectKey, string content,
        string? contentType = "text/plain; charset=utf-8", string? apiKey = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(content);

        byte[] bytes = Encoding.UTF8.GetBytes(content);
        await using var stream = new MemoryStream(bytes, writable: false);
        return await PutObject(accountId, bucketName, objectKey, stream, contentType, apiKey, cancellationToken).NoSync();
    }

    public async ValueTask<R2PutObject200?> PutObject(string accountId, string bucketName, string objectKey, byte[] content,
        string? contentType = "application/octet-stream", string? apiKey = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(content);

        await using var stream = new MemoryStream(content, writable: false);
        return await PutObject(accountId, bucketName, objectKey, stream, contentType, apiKey, cancellationToken).NoSync();
    }

    public async ValueTask<R2PutObject200?> PutObject(string accountId, string bucketName, string objectKey, object content,
        JsonOptionType? jsonOptionType = null, string? contentType = "application/json; charset=utf-8", string? apiKey = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(content);

        byte[] bytes = JsonUtil.SerializeToUtf8Bytes(content, jsonOptionType);
        await using var stream = new MemoryStream(bytes, writable: false);
        return await PutObject(accountId, bucketName, objectKey, stream, contentType, apiKey, cancellationToken).NoSync();
    }

    public async ValueTask<R2DeleteObject200?> DeleteObject(string accountId, string bucketName, string objectKey,
        string? apiKey = null, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await GetClient(apiKey, cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].Objects[objectKey].DeleteAsync(cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<R2DeleteObjects200?> DeleteObjects(string accountId, string bucketName, IReadOnlyCollection<string> objectKeys,
        string? apiKey = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(objectKeys);
        List<string> keys = objectKeys.ToList();
        CloudflareOpenApiClient client = await GetClient(apiKey, cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].Objects.DeleteAsync(keys, cancellationToken: cancellationToken).NoSync();
    }
}
