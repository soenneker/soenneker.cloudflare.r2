using Soenneker.Cloudflare.R2.Abstract;
using Soenneker.Cloudflare.Utils.Client.Abstract;

namespace Soenneker.Cloudflare.R2;

/// <summary>
/// Provides convenient access to Cloudflare R2 bucket, object, configuration, domain, metrics, and temporary credential operations.
/// </summary>
public sealed partial class CloudflareR2Util : ICloudflareR2Util
{
    private readonly ICloudflareClientUtil _clientUtil;

    /// <summary>
    /// Initializes a new instance of the <see cref="CloudflareR2Util"/> class.
    /// </summary>
    /// <param name="clientUtil">The utility used to retrieve the authenticated Cloudflare API client.</param>
    public CloudflareR2Util(ICloudflareClientUtil clientUtil)
    {
        _clientUtil = clientUtil;
    }

}
