using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Cloudflare.R2.Abstract;

public partial interface ICloudflareR2Util
{
    /// <summary>
    /// Creates a time-limited URL that permits an unauthenticated caller to download a private R2 object.
    /// The URL is signed locally and no request is made to Cloudflare.
    /// </summary>
    /// <param name="accountId">The Cloudflare account identifier.</param>
    /// <param name="bucketName">The name of the bucket.</param>
    /// <param name="objectKey">The key of the object to download.</param>
    /// <param name="accessKeyId">The access key ID from an R2 API token or temporary credential.</param>
    /// <param name="secretAccessKey">The secret access key from an R2 API token or temporary credential.</param>
    /// <param name="validFor">How long the URL remains valid. Cloudflare permits values from one second through seven days.</param>
    /// <param name="sessionToken">The session token when temporary R2 credentials are used; otherwise <see langword="null"/>.</param>
    /// <param name="cancellationToken">The token used to cancel URL generation.</param>
    /// <returns>A value task containing the presigned HTTPS download URL.</returns>
    /// <exception cref="ArgumentException">A required string argument is empty or consists only of white-space characters.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="validFor"/> is shorter than one second or longer than seven days.</exception>
    ValueTask<string> GetPresignedDownloadUrl(string accountId, string bucketName, string objectKey, string accessKeyId, string secretAccessKey,
        TimeSpan validFor, string? sessionToken = null, CancellationToken cancellationToken = default);
}
