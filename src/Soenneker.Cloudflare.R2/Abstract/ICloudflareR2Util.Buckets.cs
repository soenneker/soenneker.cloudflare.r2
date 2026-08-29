using Soenneker.Cloudflare.OpenApiClient.Accounts.Item.R2.Buckets;
using Soenneker.Cloudflare.OpenApiClient.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Cloudflare.R2.Abstract;

public partial interface ICloudflareR2Util
{
    /// <summary>
    /// Lists the R2 buckets in an account.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="configureQuery">An optional callback used to configure pagination and filtering query parameters.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the bucket list response, or <see langword="null"/> when the API returns no response body.</returns>
    ValueTask<R2ListBuckets200?> ListBuckets(string accountId,
        Action<BucketsRequestBuilder.BucketsRequestBuilderGetQueryParameters>? configureQuery = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates an R2 bucket.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="request">The bucket creation request.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the created bucket response, or <see langword="null"/> when the API returns no response body.</returns>
    ValueTask<R2CreateBucket200?> CreateBucket(string accountId, R2CreateBucket request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets an R2 bucket by name.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="bucketName">The name of the bucket.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the bucket response, or <see langword="null"/> when the API returns no response body.</returns>
    ValueTask<R2GetBucket200?> GetBucket(string accountId, string bucketName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the mutable settings of an R2 bucket.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="bucketName">The name of the bucket.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the updated bucket response, or <see langword="null"/> when the API returns no response body.</returns>
    ValueTask<R2PatchBucket200?> UpdateBucket(string accountId, string bucketName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an empty R2 bucket.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="bucketName">The name of the bucket to delete.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the deletion response, or <see langword="null"/> when the API returns no response body.</returns>
    ValueTask<R2V4Response?> DeleteBucket(string accountId, string bucketName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets account-level R2 metrics.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the account metrics response, or <see langword="null"/> when the API returns no response body.</returns>
    ValueTask<R2GetAccountLevelMetrics200?> GetMetrics(string accountId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates temporary R2 access credentials.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="request">The requested scope, permissions, and lifetime of the temporary credentials.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the temporary credentials response, or <see langword="null"/> when the API returns no response body.</returns>
    ValueTask<R2CreateTempAccessCredentials200?> CreateTemporaryAccessCredentials(string accountId, R2TempAccessCredsRequest request,
        CancellationToken cancellationToken = default);
}
