using Soenneker.Cloudflare.OpenApiClient.Accounts.Item.R2.Buckets;
using Soenneker.Cloudflare.OpenApiClient.Models;
using Soenneker.Extensions.Task;
using Soenneker.Extensions.ValueTask;
using System;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Cloudflare.OpenApiClient;

namespace Soenneker.Cloudflare.R2;

public sealed partial class CloudflareR2Util
{
    public async ValueTask<R2ListBuckets200Response?> ListBuckets(string accountId,
        Action<BucketsRequestBuilder.BucketsRequestBuilderGetQueryParameters>? configureQuery = null,
        string? apiKey = null, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await GetClient(apiKey, cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets.GetAsync(config => configureQuery?.Invoke(config.QueryParameters), cancellationToken).NoSync();
    }

    public async ValueTask<R2CreateBucket200Response?> CreateBucket(string accountId, R2CreateBucketRequest request, string? apiKey = null, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await GetClient(apiKey, cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets.PostAsync(request, cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<R2GetBucket200Response?> GetBucket(string accountId, string bucketName, string? apiKey = null, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await GetClient(apiKey, cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].GetAsync(cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<R2PatchBucket200Response?> UpdateBucket(string accountId, string bucketName, string? apiKey = null, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await GetClient(apiKey, cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].PatchAsync(cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<R2V4Response?> DeleteBucket(string accountId, string bucketName, string? apiKey = null, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await GetClient(apiKey, cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].DeleteAsync(cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<R2GetAccountLevelMetrics200Response?> GetMetrics(string accountId, string? apiKey = null, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await GetClient(apiKey, cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Metrics.GetAsync(cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<R2CreateTempAccessCredentials200Response?> CreateTemporaryAccessCredentials(string accountId, R2TempAccessCredsRequest request,
        string? apiKey = null, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await GetClient(apiKey, cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.TempAccessCredentials.PostAsync(request, cancellationToken: cancellationToken).NoSync();
    }
}
