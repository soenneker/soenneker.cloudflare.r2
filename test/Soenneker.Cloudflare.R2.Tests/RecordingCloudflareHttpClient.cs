using System;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Cloudflare.HttpClient.Abstract;

namespace Soenneker.Cloudflare.R2.Tests;

internal sealed class RecordingCloudflareHttpClient(System.Net.Http.HttpClient client, string token) : ICloudflareHttpClient
{
    public ValueTask<System.Net.Http.HttpClient> Get(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return ValueTask.FromResult(client);
    }

    public ValueTask<System.Net.Http.HttpClient> Get(string apiKey, CancellationToken cancellationToken = default)
    {
        if (apiKey != token)
            throw new InvalidOperationException("Unexpected test token.");
        return Get(cancellationToken);
    }

    public bool RemoveSync(CancellationToken cancellationToken = default) => false;
    public bool RemoveSync(string apiKey, CancellationToken cancellationToken = default) => false;
    public ValueTask<bool> Remove(CancellationToken cancellationToken = default) => ValueTask.FromResult(false);
    public ValueTask<bool> Remove(string apiKey, CancellationToken cancellationToken = default) => ValueTask.FromResult(false);

    // The test owns the recording client.
    public void Dispose() { }
    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}
