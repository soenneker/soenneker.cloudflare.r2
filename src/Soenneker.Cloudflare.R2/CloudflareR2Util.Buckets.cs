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
    public async ValueTask<R2ListBuckets200?> ListBuckets(string accountId,
        Action<BucketsRequestBuilder.BucketsRequestBuilderGetQueryParameters>? configureQuery = null,
        CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets.GetAsync(config => configureQuery?.Invoke(config.QueryParameters), cancellationToken).NoSync();
    }

    public async ValueTask<R2CreateBucket200?> CreateBucket(string accountId, R2CreateBucket request, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets.PostAsync(request, cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<R2GetBucket200?> GetBucket(string accountId, string bucketName, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].GetAsync(cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<R2PatchBucket200?> UpdateBucket(string accountId, string bucketName, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].PatchAsync(cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<R2V4Response?> DeleteBucket(string accountId, string bucketName, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].DeleteAsync(cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<R2GetAccountLevelMetrics200?> GetMetrics(string accountId, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Metrics.GetAsync(cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<R2CreateTempAccessCredentials200?> CreateTemporaryAccessCredentials(string accountId, R2TempAccessCredsRequest request,
        CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.TempAccessCredentials.PostAsync(request, cancellationToken: cancellationToken).NoSync();
    }
}
