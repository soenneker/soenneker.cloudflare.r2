using Soenneker.Cloudflare.OpenApiClient.Accounts.Item.R2.Buckets;
using Soenneker.Cloudflare.OpenApiClient.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Cloudflare.R2.Abstract;

public partial interface ICloudflareR2Util
{
    /// <summary>Lists the R2 buckets in an account.</summary>
    ValueTask<R2ListBuckets200?> ListBuckets(string accountId,
        Action<BucketsRequestBuilder.BucketsRequestBuilderGetQueryParameters>? configureQuery = null,
        CancellationToken cancellationToken = default);

    /// <summary>Creates an R2 bucket.</summary>
    ValueTask<R2CreateBucket200?> CreateBucket(string accountId, R2CreateBucket request, CancellationToken cancellationToken = default);

    /// <summary>Gets an R2 bucket.</summary>
    ValueTask<R2GetBucket200?> GetBucket(string accountId, string bucketName, CancellationToken cancellationToken = default);

    /// <summary>Updates the mutable settings of an R2 bucket.</summary>
    ValueTask<R2PatchBucket200?> UpdateBucket(string accountId, string bucketName, CancellationToken cancellationToken = default);

    /// <summary>Deletes an empty R2 bucket.</summary>
    ValueTask<R2V4Response?> DeleteBucket(string accountId, string bucketName, CancellationToken cancellationToken = default);

    /// <summary>Gets account-level R2 metrics.</summary>
    ValueTask<R2GetAccountLevelMetrics200?> GetMetrics(string accountId, CancellationToken cancellationToken = default);

    /// <summary>Creates temporary R2 access credentials.</summary>
    ValueTask<R2CreateTempAccessCredentials200?> CreateTemporaryAccessCredentials(string accountId, R2TempAccessCredsRequest request,
        CancellationToken cancellationToken = default);
}

