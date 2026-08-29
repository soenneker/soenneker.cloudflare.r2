using Soenneker.Cloudflare.OpenApiClient.Accounts.Item.R2.Buckets;
using Soenneker.Cloudflare.OpenApiClient.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Cloudflare.R2;

public sealed partial class CloudflareR2Util
{
    public ValueTask<R2ListBuckets200?> ListBuckets(string accountId,
        Action<BucketsRequestBuilder.BucketsRequestBuilderGetQueryParameters>? configureQuery = null,
        CancellationToken cancellationToken = default)
    {
        return Execute(accountId, nameof(ListBuckets),
            client => client.Accounts[accountId].R2.Buckets.GetAsync(config => configureQuery?.Invoke(config.QueryParameters), cancellationToken),
            cancellationToken);
    }

    public ValueTask<R2CreateBucket200?> CreateBucket(string accountId, R2CreateBucket request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        return Execute(accountId, nameof(CreateBucket), client => client.Accounts[accountId].R2.Buckets.PostAsync(request, cancellationToken: cancellationToken),
            cancellationToken);
    }

    public ValueTask<R2GetBucket200?> GetBucket(string accountId, string bucketName, CancellationToken cancellationToken = default)
    {
        ValidateBucketName(bucketName);
        return Execute(accountId, nameof(GetBucket), client => client.Accounts[accountId].R2.Buckets[bucketName].GetAsync(cancellationToken: cancellationToken),
            cancellationToken);
    }

    public ValueTask<R2PatchBucket200?> UpdateBucket(string accountId, string bucketName, CancellationToken cancellationToken = default)
    {
        ValidateBucketName(bucketName);
        return Execute(accountId, nameof(UpdateBucket), client => client.Accounts[accountId].R2.Buckets[bucketName].PatchAsync(cancellationToken: cancellationToken),
            cancellationToken);
    }

    public ValueTask<R2V4Response?> DeleteBucket(string accountId, string bucketName, CancellationToken cancellationToken = default)
    {
        ValidateBucketName(bucketName);
        return Execute(accountId, nameof(DeleteBucket), client => client.Accounts[accountId].R2.Buckets[bucketName].DeleteAsync(cancellationToken: cancellationToken),
            cancellationToken);
    }

    public ValueTask<R2GetAccountLevelMetrics200?> GetMetrics(string accountId, CancellationToken cancellationToken = default) =>
        Execute(accountId, nameof(GetMetrics), client => client.Accounts[accountId].R2.Metrics.GetAsync(cancellationToken: cancellationToken), cancellationToken);

    public ValueTask<R2CreateTempAccessCredentials200?> CreateTemporaryAccessCredentials(string accountId, R2TempAccessCredsRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        return Execute(accountId, nameof(CreateTemporaryAccessCredentials),
            client => client.Accounts[accountId].R2.TempAccessCredentials.PostAsync(request, cancellationToken: cancellationToken), cancellationToken);
    }
}

