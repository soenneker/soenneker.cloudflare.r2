using Soenneker.Cloudflare.OpenApiClient.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Cloudflare.R2.Abstract;

public partial interface ICloudflareR2Util
{
    ValueTask<R2GetBucketCorsPolicy200?> GetCorsPolicy(string accountId, string bucketName, CancellationToken cancellationToken = default);
    ValueTask<R2PutBucketCorsPolicy200?> PutCorsPolicy(string accountId, string bucketName, R2PutBucketCorsPolicy request,
        CancellationToken cancellationToken = default);
    ValueTask<R2DeleteBucketCorsPolicy200?> DeleteCorsPolicy(string accountId, string bucketName, CancellationToken cancellationToken = default);

    ValueTask<R2GetBucketLifecycleConfiguration200?> GetLifecycleConfiguration(string accountId, string bucketName,
        CancellationToken cancellationToken = default);
    ValueTask<R2PutBucketLifecycleConfiguration200?> PutLifecycleConfiguration(string accountId, string bucketName,
        R2PutBucketLifecycleConfiguration request, CancellationToken cancellationToken = default);

    ValueTask<R2GetBucketLockConfiguration200?> GetLockConfiguration(string accountId, string bucketName,
        CancellationToken cancellationToken = default);
    ValueTask<R2PutBucketLockConfiguration200?> PutLockConfiguration(string accountId, string bucketName,
        R2PutBucketLockConfiguration request, CancellationToken cancellationToken = default);

    ValueTask<R2GetBucketLocalUploadsConfiguration200?> GetLocalUploadsConfiguration(string accountId, string bucketName,
        CancellationToken cancellationToken = default);
    ValueTask<R2PutBucketLocalUploadsConfiguration200?> PutLocalUploadsConfiguration(string accountId, string bucketName,
        R2PutBucketLocalUploadsConfiguration request, CancellationToken cancellationToken = default);

    ValueTask<R2GetBucketSippyConfig200?> GetSippyConfiguration(string accountId, string bucketName,
        CancellationToken cancellationToken = default);
    ValueTask<R2PutBucketSippyConfig200?> PutSippyConfiguration(string accountId, string bucketName, R2PutBucketSippyConfig request,
        CancellationToken cancellationToken = default);
    ValueTask<R2DeleteBucketSippyConfig200?> DeleteSippyConfiguration(string accountId, string bucketName,
        CancellationToken cancellationToken = default);
}

