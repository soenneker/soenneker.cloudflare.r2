using Soenneker.Cloudflare.OpenApiClient.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Cloudflare.R2.Abstract;

public partial interface ICloudflareR2Util
{
    /// <summary>
    /// Gets the CORS policy for a bucket.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="bucketName">The name of the bucket.</param>
    /// <param name="apiKey">An optional Cloudflare API key. When omitted, the configured default key is used.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the CORS policy response, or <see langword="null"/> when the API returns no response body.</returns>
    ValueTask<R2GetBucketCorsPolicy200Response?> GetCorsPolicy(string accountId, string bucketName, string? apiKey = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates or replaces the CORS policy for a bucket.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="bucketName">The name of the bucket.</param>
    /// <param name="request">The CORS policy to apply.</param>
    /// <param name="apiKey">An optional Cloudflare API key. When omitted, the configured default key is used.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the updated CORS policy response, or <see langword="null"/> when the API returns no response body.</returns>
    ValueTask<R2PutBucketCorsPolicy200Response?> PutCorsPolicy(string accountId, string bucketName, R2PutBucketCorsPolicyRequest request,
        string? apiKey = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the CORS policy from a bucket.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="bucketName">The name of the bucket.</param>
    /// <param name="apiKey">An optional Cloudflare API key. When omitted, the configured default key is used.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the deletion response, or <see langword="null"/> when the API returns no response body.</returns>
    ValueTask<R2DeleteBucketCorsPolicy200Response?> DeleteCorsPolicy(string accountId, string bucketName, string? apiKey = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the object lifecycle configuration for a bucket.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="bucketName">The name of the bucket.</param>
    /// <param name="apiKey">An optional Cloudflare API key. When omitted, the configured default key is used.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the lifecycle configuration response, or <see langword="null"/> when the API returns no response body.</returns>
    ValueTask<R2GetBucketLifecycleConfiguration200Response?> GetLifecycleConfiguration(string accountId, string bucketName,
        string? apiKey = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates or replaces the object lifecycle configuration for a bucket.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="bucketName">The name of the bucket.</param>
    /// <param name="request">The lifecycle configuration to apply.</param>
    /// <param name="apiKey">An optional Cloudflare API key. When omitted, the configured default key is used.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the updated lifecycle configuration response, or <see langword="null"/> when the API returns no response body.</returns>
    ValueTask<R2PutBucketLifecycleConfiguration200Response?> PutLifecycleConfiguration(string accountId, string bucketName,
        R2PutBucketLifecycleConfigurationRequest request, string? apiKey = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the object lock configuration for a bucket.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="bucketName">The name of the bucket.</param>
    /// <param name="apiKey">An optional Cloudflare API key. When omitted, the configured default key is used.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the lock configuration response, or <see langword="null"/> when the API returns no response body.</returns>
    ValueTask<R2GetBucketLockConfiguration200Response?> GetLockConfiguration(string accountId, string bucketName,
        string? apiKey = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates or replaces the object lock configuration for a bucket.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="bucketName">The name of the bucket.</param>
    /// <param name="request">The object lock configuration to apply.</param>
    /// <param name="apiKey">An optional Cloudflare API key. When omitted, the configured default key is used.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the updated lock configuration response, or <see langword="null"/> when the API returns no response body.</returns>
    ValueTask<R2PutBucketLockConfiguration200Response?> PutLockConfiguration(string accountId, string bucketName,
        R2PutBucketLockConfigurationRequest request, string? apiKey = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the local uploads configuration for a bucket.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="bucketName">The name of the bucket.</param>
    /// <param name="apiKey">An optional Cloudflare API key. When omitted, the configured default key is used.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the local uploads configuration response, or <see langword="null"/> when the API returns no response body.</returns>
    ValueTask<R2GetBucketLocalUploadsConfiguration200Response?> GetLocalUploadsConfiguration(string accountId, string bucketName,
        string? apiKey = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates or replaces the local uploads configuration for a bucket.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="bucketName">The name of the bucket.</param>
    /// <param name="request">The local uploads configuration to apply.</param>
    /// <param name="apiKey">An optional Cloudflare API key. When omitted, the configured default key is used.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the updated local uploads configuration response, or <see langword="null"/> when the API returns no response body.</returns>
    ValueTask<R2PutBucketLocalUploadsConfiguration200Response?> PutLocalUploadsConfiguration(string accountId, string bucketName,
        R2PutBucketLocalUploadsConfigurationRequest request, string? apiKey = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the Sippy configuration for a bucket.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="bucketName">The name of the bucket.</param>
    /// <param name="apiKey">An optional Cloudflare API key. When omitted, the configured default key is used.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the Sippy configuration response, or <see langword="null"/> when the API returns no response body.</returns>
    ValueTask<R2GetBucketSippyConfig200Response?> GetSippyConfiguration(string accountId, string bucketName,
        string? apiKey = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates or replaces the Sippy configuration for a bucket.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="bucketName">The name of the bucket.</param>
    /// <param name="request">The Sippy configuration to apply.</param>
    /// <param name="apiKey">An optional Cloudflare API key. When omitted, the configured default key is used.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the updated Sippy configuration response, or <see langword="null"/> when the API returns no response body.</returns>
    ValueTask<R2PutBucketSippyConfig200Response?> PutSippyConfiguration(string accountId, string bucketName, R2PutBucketSippyConfigRequest request,
        string? apiKey = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the Sippy configuration from a bucket.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="bucketName">The name of the bucket.</param>
    /// <param name="apiKey">An optional Cloudflare API key. When omitted, the configured default key is used.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the deletion response, or <see langword="null"/> when the API returns no response body.</returns>
    ValueTask<R2DeleteBucketSippyConfig200Response?> DeleteSippyConfiguration(string accountId, string bucketName,
        string? apiKey = null, CancellationToken cancellationToken = default);
}
