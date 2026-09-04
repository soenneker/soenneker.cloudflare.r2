using Soenneker.Cloudflare.R2.Abstract;
using Soenneker.Cloudflare.OpenApiClient;
using Soenneker.Cloudflare.Utils.Client.Abstract;
using Soenneker.Aws.Signing.V4;
using Soenneker.Aws.Signing.V4.Abstract;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Cloudflare.R2;

/// <inheritdoc cref="ICloudflareR2Util" />
public sealed partial class CloudflareR2Util : ICloudflareR2Util
{
    private readonly ICloudflareClientUtil _clientUtil;
    private readonly IAwsSignatureV4Signer _signatureV4Signer;

    public CloudflareR2Util(ICloudflareClientUtil clientUtil) : this(clientUtil, new AwsSignatureV4Signer())
    {
    }

    public CloudflareR2Util(ICloudflareClientUtil clientUtil, IAwsSignatureV4Signer signatureV4Signer)
    {
        ArgumentNullException.ThrowIfNull(clientUtil);
        ArgumentNullException.ThrowIfNull(signatureV4Signer);
        _clientUtil = clientUtil;
        _signatureV4Signer = signatureV4Signer;
    }

    private ValueTask<CloudflareOpenApiClient> GetClient(string? apiKey, CancellationToken cancellationToken) =>
        apiKey is null ? _clientUtil.Get(cancellationToken) : _clientUtil.Get(apiKey, cancellationToken);
}
