using Soenneker.Cloudflare.OpenApiClient.Accounts.Item.R2.Buckets.Item.Objects;
using Soenneker.Cloudflare.OpenApiClient.Models;
using Soenneker.Extensions.Task;
using Soenneker.Extensions.ValueTask;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Cloudflare.OpenApiClient;

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
