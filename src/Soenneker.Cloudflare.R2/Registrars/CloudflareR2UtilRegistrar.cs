using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Aws.Signing.V4.Registrars;
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
    /// <param name="services">The service collection to which the registrations are added.</param>
    /// <returns>The same service collection so that additional registrations can be chained.</returns>
    public static IServiceCollection AddCloudflareR2UtilAsSingleton(this IServiceCollection services)
    {
        services.AddCloudflareClientUtilAsSingleton().AddAwsSignatureV4SignerAsSingleton().TryAddSingleton<ICloudflareR2Util, CloudflareR2Util>();
        return services;
    }

    /// <summary>
    /// Adds <see cref="ICloudflareR2Util"/> as a scoped service.
    /// </summary>
    /// <param name="services">The service collection to which the registrations are added.</param>
    /// <returns>The same service collection so that additional registrations can be chained.</returns>
    public static IServiceCollection AddCloudflareR2UtilAsScoped(this IServiceCollection services)
    {
        services.AddCloudflareClientUtilAsSingleton().AddAwsSignatureV4SignerAsSingleton().TryAddScoped<ICloudflareR2Util, CloudflareR2Util>();
        return services;
    }
}
