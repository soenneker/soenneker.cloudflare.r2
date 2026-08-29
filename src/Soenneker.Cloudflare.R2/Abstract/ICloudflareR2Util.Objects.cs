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
    /// <summary>Lists objects in a bucket.</summary>
    ValueTask<R2ListObjects200?> ListObjects(string accountId, string bucketName,
        Action<ObjectsRequestBuilder.ObjectsRequestBuilderGetQueryParameters>? configureQuery = null,
        CancellationToken cancellationToken = default);

    /// <summary>Downloads an object as a stream. The caller owns the returned stream.</summary>
    ValueTask<Stream?> GetObject(string accountId, string bucketName, string objectKey, CancellationToken cancellationToken = default);

    /// <summary>Uploads an object from the current position of a stream.</summary>
    ValueTask<R2PutObject200?> PutObject(string accountId, string bucketName, string objectKey, Stream content,
        string? contentType = null, CancellationToken cancellationToken = default);

    /// <summary>Deletes an object.</summary>
    ValueTask<R2DeleteObject200?> DeleteObject(string accountId, string bucketName, string objectKey,
        CancellationToken cancellationToken = default);

    /// <summary>Deletes multiple objects in one request.</summary>
    ValueTask<R2DeleteObjects200?> DeleteObjects(string accountId, string bucketName, IReadOnlyCollection<string> objectKeys,
        CancellationToken cancellationToken = default);
}

