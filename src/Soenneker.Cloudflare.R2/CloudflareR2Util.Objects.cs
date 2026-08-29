using Soenneker.Cloudflare.OpenApiClient.Accounts.Item.R2.Buckets.Item.Objects;
using Soenneker.Cloudflare.OpenApiClient.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Cloudflare.R2;

public sealed partial class CloudflareR2Util
{
    public ValueTask<R2ListObjects200?> ListObjects(string accountId, string bucketName,
        Action<ObjectsRequestBuilder.ObjectsRequestBuilderGetQueryParameters>? configureQuery = null,
        CancellationToken cancellationToken = default)
    {
        ValidateBucketName(bucketName);
        return Execute(accountId, nameof(ListObjects),
            client => client.Accounts[accountId].R2.Buckets[bucketName].Objects.GetAsync(
                config => configureQuery?.Invoke(config.QueryParameters), cancellationToken), cancellationToken);
    }

    public ValueTask<Stream?> GetObject(string accountId, string bucketName, string objectKey, CancellationToken cancellationToken = default)
    {
        ValidateBucketName(bucketName);
        ValidateObjectKey(objectKey);
        return Execute(accountId, nameof(GetObject),
            client => client.Accounts[accountId].R2.Buckets[bucketName].Objects[objectKey].GetAsync(cancellationToken: cancellationToken), cancellationToken);
    }

    public ValueTask<R2PutObject200?> PutObject(string accountId, string bucketName, string objectKey, Stream content,
        string? contentType = null, CancellationToken cancellationToken = default)
    {
        ValidateBucketName(bucketName);
        ValidateObjectKey(objectKey);
        ArgumentNullException.ThrowIfNull(content);
        if (!content.CanRead)
            throw new ArgumentException("The content stream must be readable.", nameof(content));

        return Execute(accountId, nameof(PutObject),
            client => client.Accounts[accountId].R2.Buckets[bucketName].Objects[objectKey].PutAsync(content, config =>
            {
                if (!string.IsNullOrWhiteSpace(contentType))
                    config.Headers.Add("Content-Type", contentType);
            }, cancellationToken), cancellationToken);
    }

    public ValueTask<R2DeleteObject200?> DeleteObject(string accountId, string bucketName, string objectKey,
        CancellationToken cancellationToken = default)
    {
        ValidateBucketName(bucketName);
        ValidateObjectKey(objectKey);
        return Execute(accountId, nameof(DeleteObject),
            client => client.Accounts[accountId].R2.Buckets[bucketName].Objects[objectKey].DeleteAsync(cancellationToken: cancellationToken), cancellationToken);
    }

    public ValueTask<R2DeleteObjects200?> DeleteObjects(string accountId, string bucketName, IReadOnlyCollection<string> objectKeys,
        CancellationToken cancellationToken = default)
    {
        ValidateBucketName(bucketName);
        ArgumentNullException.ThrowIfNull(objectKeys);
        if (objectKeys.Count == 0)
            throw new ArgumentException("At least one object key is required.", nameof(objectKeys));

        var keys = objectKeys.ToList();
        if (keys.Any(string.IsNullOrWhiteSpace))
            throw new ArgumentException("Object keys cannot be null or whitespace.", nameof(objectKeys));

        return Execute(accountId, nameof(DeleteObjects),
            client => client.Accounts[accountId].R2.Buckets[bucketName].Objects.DeleteAsync(keys, cancellationToken: cancellationToken), cancellationToken);
    }
}

