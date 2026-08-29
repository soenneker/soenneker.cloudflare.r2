using Soenneker.Cloudflare.OpenApiClient.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Cloudflare.R2.Abstract;

public partial interface ICloudflareR2Util
{
    /// <summary>
    /// Lists the custom domains attached to a bucket.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="bucketName">The name of the bucket.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the custom domain list response, or <see langword="null"/> when the API returns no response body.</returns>
    ValueTask<R2ListCustomDomains200?> ListCustomDomains(string accountId, string bucketName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Attaches a custom domain to a bucket.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="bucketName">The name of the bucket.</param>
    /// <param name="request">The custom domain configuration to add.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the added custom domain response, or <see langword="null"/> when the API returns no response body.</returns>
    ValueTask<R2AddCustomDomain200?> AddCustomDomain(string accountId, string bucketName, R2AddCustomDomainRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the settings for a custom domain attached to a bucket.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="bucketName">The name of the bucket.</param>
    /// <param name="domain">The custom domain name.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the custom domain settings response, or <see langword="null"/> when the API returns no response body.</returns>
    ValueTask<R2GetCustomDomainSettings200?> GetCustomDomain(string accountId, string bucketName, string domain,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the settings for a custom domain attached to a bucket.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="bucketName">The name of the bucket.</param>
    /// <param name="domain">The custom domain name.</param>
    /// <param name="request">The custom domain settings to apply.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the updated custom domain response, or <see langword="null"/> when the API returns no response body.</returns>
    ValueTask<R2EditCustomDomainSettings200?> UpdateCustomDomain(string accountId, string bucketName, string domain,
        R2EditCustomDomainRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Detaches a custom domain from a bucket.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="bucketName">The name of the bucket.</param>
    /// <param name="domain">The custom domain name.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the deletion response, or <see langword="null"/> when the API returns no response body.</returns>
    ValueTask<R2DeleteCustomDomain200?> DeleteCustomDomain(string accountId, string bucketName, string domain,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the public access policy for the Cloudflare-managed domain of a bucket.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="bucketName">The name of the bucket.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the managed domain policy response, or <see langword="null"/> when the API returns no response body.</returns>
    ValueTask<R2GetBucketPublicPolicy200?> GetManagedDomain(string accountId, string bucketName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the public access policy for the Cloudflare-managed domain of a bucket.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="bucketName">The name of the bucket.</param>
    /// <param name="request">The managed domain settings to apply.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the updated managed domain policy response, or <see langword="null"/> when the API returns no response body.</returns>
    ValueTask<R2PutBucketPublicPolicy200?> UpdateManagedDomain(string accountId, string bucketName, R2EditManagedDomainRequest request,
        CancellationToken cancellationToken = default);
}
