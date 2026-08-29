using Soenneker.Cloudflare.OpenApiClient.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Cloudflare.R2;

public sealed partial class CloudflareR2Util
{
    public ValueTask<R2GetBucketCorsPolicy200?> GetCorsPolicy(string accountId, string bucketName, CancellationToken cancellationToken = default)
    {
        ValidateBucketName(bucketName);
        return Execute(accountId, nameof(GetCorsPolicy),
            client => client.Accounts[accountId].R2.Buckets[bucketName].Cors.GetAsync(cancellationToken: cancellationToken), cancellationToken);
    }

    public ValueTask<R2PutBucketCorsPolicy200?> PutCorsPolicy(string accountId, string bucketName, R2PutBucketCorsPolicy request,
        CancellationToken cancellationToken = default)
    {
        ValidateBucketName(bucketName);
        ArgumentNullException.ThrowIfNull(request);
        return Execute(accountId, nameof(PutCorsPolicy),
            client => client.Accounts[accountId].R2.Buckets[bucketName].Cors.PutAsync(request, cancellationToken: cancellationToken), cancellationToken);
    }

    public ValueTask<R2DeleteBucketCorsPolicy200?> DeleteCorsPolicy(string accountId, string bucketName, CancellationToken cancellationToken = default)
    {
        ValidateBucketName(bucketName);
        return Execute(accountId, nameof(DeleteCorsPolicy),
            client => client.Accounts[accountId].R2.Buckets[bucketName].Cors.DeleteAsync(cancellationToken: cancellationToken), cancellationToken);
    }

    public ValueTask<R2GetBucketLifecycleConfiguration200?> GetLifecycleConfiguration(string accountId, string bucketName,
        CancellationToken cancellationToken = default)
    {
        ValidateBucketName(bucketName);
        return Execute(accountId, nameof(GetLifecycleConfiguration),
            client => client.Accounts[accountId].R2.Buckets[bucketName].Lifecycle.GetAsync(cancellationToken: cancellationToken), cancellationToken);
    }

    public ValueTask<R2PutBucketLifecycleConfiguration200?> PutLifecycleConfiguration(string accountId, string bucketName,
        R2PutBucketLifecycleConfiguration request, CancellationToken cancellationToken = default)
    {
        ValidateBucketName(bucketName);
        ArgumentNullException.ThrowIfNull(request);
        return Execute(accountId, nameof(PutLifecycleConfiguration),
            client => client.Accounts[accountId].R2.Buckets[bucketName].Lifecycle.PutAsync(request, cancellationToken: cancellationToken), cancellationToken);
    }

    public ValueTask<R2GetBucketLockConfiguration200?> GetLockConfiguration(string accountId, string bucketName,
        CancellationToken cancellationToken = default)
    {
        ValidateBucketName(bucketName);
        return Execute(accountId, nameof(GetLockConfiguration),
            client => client.Accounts[accountId].R2.Buckets[bucketName].Lock.GetAsync(cancellationToken: cancellationToken), cancellationToken);
    }

    public ValueTask<R2PutBucketLockConfiguration200?> PutLockConfiguration(string accountId, string bucketName,
        R2PutBucketLockConfiguration request, CancellationToken cancellationToken = default)
    {
        ValidateBucketName(bucketName);
        ArgumentNullException.ThrowIfNull(request);
        return Execute(accountId, nameof(PutLockConfiguration),
            client => client.Accounts[accountId].R2.Buckets[bucketName].Lock.PutAsync(request, cancellationToken: cancellationToken), cancellationToken);
    }

    public ValueTask<R2GetBucketLocalUploadsConfiguration200?> GetLocalUploadsConfiguration(string accountId, string bucketName,
        CancellationToken cancellationToken = default)
    {
        ValidateBucketName(bucketName);
        return Execute(accountId, nameof(GetLocalUploadsConfiguration),
            client => client.Accounts[accountId].R2.Buckets[bucketName].LocalUploads.GetAsync(cancellationToken: cancellationToken), cancellationToken);
    }

    public ValueTask<R2PutBucketLocalUploadsConfiguration200?> PutLocalUploadsConfiguration(string accountId, string bucketName,
        R2PutBucketLocalUploadsConfiguration request, CancellationToken cancellationToken = default)
    {
        ValidateBucketName(bucketName);
        ArgumentNullException.ThrowIfNull(request);
        return Execute(accountId, nameof(PutLocalUploadsConfiguration),
            client => client.Accounts[accountId].R2.Buckets[bucketName].LocalUploads.PutAsync(request, cancellationToken: cancellationToken), cancellationToken);
    }

    public ValueTask<R2GetBucketSippyConfig200?> GetSippyConfiguration(string accountId, string bucketName,
        CancellationToken cancellationToken = default)
    {
        ValidateBucketName(bucketName);
        return Execute(accountId, nameof(GetSippyConfiguration),
            client => client.Accounts[accountId].R2.Buckets[bucketName].Sippy.GetAsync(cancellationToken: cancellationToken), cancellationToken);
    }

    public ValueTask<R2PutBucketSippyConfig200?> PutSippyConfiguration(string accountId, string bucketName, R2PutBucketSippyConfig request,
        CancellationToken cancellationToken = default)
    {
        ValidateBucketName(bucketName);
        ArgumentNullException.ThrowIfNull(request);
        return Execute(accountId, nameof(PutSippyConfiguration),
            client => client.Accounts[accountId].R2.Buckets[bucketName].Sippy.PutAsync(request, cancellationToken: cancellationToken), cancellationToken);
    }

    public ValueTask<R2DeleteBucketSippyConfig200?> DeleteSippyConfiguration(string accountId, string bucketName,
        CancellationToken cancellationToken = default)
    {
        ValidateBucketName(bucketName);
        return Execute(accountId, nameof(DeleteSippyConfiguration),
            client => client.Accounts[accountId].R2.Buckets[bucketName].Sippy.DeleteAsync(cancellationToken: cancellationToken), cancellationToken);
    }
}

