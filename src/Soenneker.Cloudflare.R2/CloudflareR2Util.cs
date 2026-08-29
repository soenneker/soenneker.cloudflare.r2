using Microsoft.Extensions.Logging;
using Soenneker.Cloudflare.OpenApiClient;
using Soenneker.Cloudflare.R2.Abstract;
using Soenneker.Cloudflare.Utils.Client.Abstract;
using Soenneker.Extensions.ValueTask;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Cloudflare.R2;

/// <inheritdoc cref="ICloudflareR2Util"/>
public sealed partial class CloudflareR2Util : ICloudflareR2Util
{
    private readonly ICloudflareClientUtil _clientUtil;
    private readonly ILogger<CloudflareR2Util> _logger;

    public CloudflareR2Util(ICloudflareClientUtil clientUtil, ILogger<CloudflareR2Util> logger)
    {
        _clientUtil = clientUtil;
        _logger = logger;
    }

    private async ValueTask<T?> Execute<T>(string accountId, string operation, Func<CloudflareOpenApiClient, Task<T?>> action,
        CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(accountId);
        ArgumentNullException.ThrowIfNull(action);

        _logger.LogDebug("Executing Cloudflare R2 operation {Operation} for account {AccountId}", operation, accountId);

        try
        {
            CloudflareOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();
            return await action(client).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Cloudflare R2 operation {Operation} failed for account {AccountId}", operation, accountId);
            throw;
        }
    }

    private static void ValidateBucketName(string bucketName) => ArgumentException.ThrowIfNullOrWhiteSpace(bucketName);

    private static void ValidateObjectKey(string objectKey) => ArgumentException.ThrowIfNullOrWhiteSpace(objectKey);
}

