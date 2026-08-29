using Soenneker.Cloudflare.OpenApiClient.Accounts.Item.R2.Buckets.Item.Objects;
using Soenneker.Cloudflare.OpenApiClient.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Cloudflare.R2.Abstract;

public partial interface ICloudflareR2Util
{
    /// <summary>
    /// Lists objects in a bucket.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="bucketName">The name of the bucket.</param>
    /// <param name="configureQuery">An optional callback used to configure pagination and filtering query parameters.</param>
    /// <param name="apiKey">An optional Cloudflare API key. When omitted, the configured default key is used.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the object list response, or <see langword="null"/> when the API returns no response body.</returns>
    ValueTask<R2ListObjects200?> ListObjects(string accountId, string bucketName,
        Action<ObjectsRequestBuilder.ObjectsRequestBuilderGetQueryParameters>? configureQuery = null,
        string? apiKey = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Downloads an object as a stream.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="bucketName">The name of the bucket.</param>
    /// <param name="objectKey">The key of the object to download.</param>
    /// <param name="apiKey">An optional Cloudflare API key. When omitted, the configured default key is used.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the readable object stream, or <see langword="null"/> when the API returns no response body. The caller owns the returned stream.</returns>
    ValueTask<Stream?> GetObject(string accountId, string bucketName, string objectKey, string? apiKey = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Uploads an object from the current position of a stream. The stream is not disposed by this method.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="bucketName">The name of the bucket.</param>
    /// <param name="objectKey">The destination key for the object.</param>
    /// <param name="content">The readable stream whose remaining content is uploaded.</param>
    /// <param name="contentType">The optional media type to send in the <c>Content-Type</c> header.</param>
    /// <param name="apiKey">An optional Cloudflare API key. When omitted, the configured default key is used.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the upload response, or <see langword="null"/> when the API returns no response body.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="content"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="content"/> is not readable.</exception>
    ValueTask<R2PutObject200?> PutObject(string accountId, string bucketName, string objectKey, Stream content,
        string? contentType = null, string? apiKey = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an object.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="bucketName">The name of the bucket.</param>
    /// <param name="objectKey">The key of the object to delete.</param>
    /// <param name="apiKey">An optional Cloudflare API key. When omitted, the configured default key is used.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the deletion response, or <see langword="null"/> when the API returns no response body.</returns>
    ValueTask<R2DeleteObject200?> DeleteObject(string accountId, string bucketName, string objectKey,
        string? apiKey = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes multiple objects in one request.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="bucketName">The name of the bucket.</param>
    /// <param name="objectKeys">The collection of object keys to delete.</param>
    /// <param name="apiKey">An optional Cloudflare API key. When omitted, the configured default key is used.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A value task containing the bulk deletion response, or <see langword="null"/> when the API returns no response body.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="objectKeys"/> is <see langword="null"/>.</exception>
    ValueTask<R2DeleteObjects200?> DeleteObjects(string accountId, string bucketName, IReadOnlyCollection<string> objectKeys,
        string? apiKey = null, CancellationToken cancellationToken = default);
}
