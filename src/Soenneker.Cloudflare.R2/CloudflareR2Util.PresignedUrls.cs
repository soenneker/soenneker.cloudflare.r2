using Soenneker.Aws.Signing.V4.Dtos;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Cloudflare.R2;

public sealed partial class CloudflareR2Util
{
    public ValueTask<string> GetPresignedDownloadUrl(string accountId, string bucketName, string objectKey, string accessKeyId,
        string secretAccessKey, TimeSpan validFor, string? sessionToken = null, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(accountId);
        ArgumentException.ThrowIfNullOrWhiteSpace(bucketName);
        ArgumentException.ThrowIfNullOrWhiteSpace(objectKey);
        cancellationToken.ThrowIfCancellationRequested();

        string url = _signatureV4Signer.PresignUrl(new AwsSignatureV4PresignRequest
        {
            Endpoint = new Uri($"https://{accountId}.r2.cloudflarestorage.com"),
            Path = $"/{bucketName}/{objectKey}",
            Method = HttpMethod.Get,
            Region = "auto",
            Service = "s3",
            Credentials = new AwsSignatureV4Credentials
            {
                AccessKeyId = accessKeyId,
                SecretAccessKey = secretAccessKey,
                SessionToken = sessionToken
            },
            Expires = validFor
        });

        return ValueTask.FromResult(url);
    }
}
