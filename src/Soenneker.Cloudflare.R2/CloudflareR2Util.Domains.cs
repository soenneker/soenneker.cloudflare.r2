using Soenneker.Cloudflare.OpenApiClient.Models;
using Soenneker.Extensions.Task;
using Soenneker.Extensions.ValueTask;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Cloudflare.OpenApiClient;

namespace Soenneker.Cloudflare.R2;

public sealed partial class CloudflareR2Util
{
    public async ValueTask<R2ListCustomDomains200?> ListCustomDomains(string accountId, string bucketName, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].Domains.Custom.GetAsync(cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<R2AddCustomDomain200?> AddCustomDomain(string accountId, string bucketName, R2AddCustomDomainRequest request,
        CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].Domains.Custom.PostAsync(request, cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<R2GetCustomDomainSettings200?> GetCustomDomain(string accountId, string bucketName, string domain,
        CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].Domains.Custom[domain].GetAsync(cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<R2EditCustomDomainSettings200?> UpdateCustomDomain(string accountId, string bucketName, string domain,
        R2EditCustomDomainRequest request, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].Domains.Custom[domain].PutAsync(request, cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<R2DeleteCustomDomain200?> DeleteCustomDomain(string accountId, string bucketName, string domain,
        CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].Domains.Custom[domain].DeleteAsync(cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<R2GetBucketPublicPolicy200?> GetManagedDomain(string accountId, string bucketName, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].Domains.Managed.GetAsync(cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<R2PutBucketPublicPolicy200?> UpdateManagedDomain(string accountId, string bucketName, R2EditManagedDomainRequest request,
        CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].Domains.Managed.PutAsync(request, cancellationToken: cancellationToken).NoSync();
    }
}
