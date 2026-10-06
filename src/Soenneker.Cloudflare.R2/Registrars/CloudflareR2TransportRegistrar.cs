using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Cloudflare.R2.Abstract;

namespace Soenneker.Cloudflare.R2.Registrars;

/// <summary>Registers R2 transports with bounded timeouts, no redirects, and no automatic mutation retries.</summary>
public static class CloudflareR2TransportRegistrar
{
    public const string WorkerClientName = "Soenneker.Cloudflare.R2.Worker";

    /// <summary>Registers the authenticated Worker object store. Endpoint and credential factories run lazily on resolution.</summary>
    public static IServiceCollection AddCloudflareR2WorkerObjectStoreAsSingleton(this IServiceCollection services,
        Func<IServiceProvider, Uri> endpoint, Func<IServiceProvider, string> apiKey)
    {
        ArgumentNullException.ThrowIfNull(endpoint);
        ArgumentNullException.ThrowIfNull(apiKey);
        services.AddHttpClient(WorkerClientName, client => client.Timeout = TimeSpan.FromSeconds(30))
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { AllowAutoRedirect = false });
        services.TryAddSingleton<ICloudflareR2ObjectStore>(provider => new CloudflareR2WorkerObjectStore(
            provider.GetRequiredService<IHttpClientFactory>().CreateClient(WorkerClientName), endpoint(provider), apiKey(provider)));
        return services;
    }
}
