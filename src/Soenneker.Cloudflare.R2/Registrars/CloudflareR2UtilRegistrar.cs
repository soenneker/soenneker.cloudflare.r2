using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Cloudflare.R2.Abstract;
using Soenneker.Cloudflare.Utils.Client.Registrars;

namespace Soenneker.Cloudflare.R2.Registrars;

/// <summary>
/// Registration extensions for <see cref="ICloudflareR2Util"/>.
/// </summary>
public static class CloudflareR2UtilRegistrar
{
    /// <summary>
    /// Adds <see cref="ICloudflareR2Util"/> as a singleton service.
    /// </summary>
    public static IServiceCollection AddCloudflareR2UtilAsSingleton(this IServiceCollection services)
    {
        services.AddCloudflareClientUtilAsSingleton().TryAddSingleton<ICloudflareR2Util, CloudflareR2Util>();
        return services;
    }

    /// <summary>
    /// Adds <see cref="ICloudflareR2Util"/> as a scoped service.
    /// </summary>
    public static IServiceCollection AddCloudflareR2UtilAsScoped(this IServiceCollection services)
    {
        services.AddCloudflareClientUtilAsSingleton().TryAddScoped<ICloudflareR2Util, CloudflareR2Util>();
        return services;
    }
}

