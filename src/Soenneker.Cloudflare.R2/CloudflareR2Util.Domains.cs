using Soenneker.Cloudflare.OpenApiClient.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Cloudflare.R2;

public sealed partial class CloudflareR2Util
{
    public ValueTask<R2ListCustomDomains200?> ListCustomDomains(string accountId, string bucketName, CancellationToken cancellationToken = default)
    {
        ValidateBucketName(bucketName);
        return Execute(accountId, nameof(ListCustomDomains),
            client => client.Accounts[accountId].R2.Buckets[bucketName].Domains.Custom.GetAsync(cancellationToken: cancellationToken), cancellationToken);
    }

    public ValueTask<R2AddCustomDomain200?> AddCustomDomain(string accountId, string bucketName, R2AddCustomDomainRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateBucketName(bucketName);
        ArgumentNullException.ThrowIfNull(request);
        return Execute(accountId, nameof(AddCustomDomain),
            client => client.Accounts[accountId].R2.Buckets[bucketName].Domains.Custom.PostAsync(request, cancellationToken: cancellationToken), cancellationToken);
    }

    public ValueTask<R2GetCustomDomainSettings200?> GetCustomDomain(string accountId, string bucketName, string domain,
        CancellationToken cancellationToken = default)
    {
        ValidateBucketName(bucketName);
        ArgumentException.ThrowIfNullOrWhiteSpace(domain);
        return Execute(accountId, nameof(GetCustomDomain),
            client => client.Accounts[accountId].R2.Buckets[bucketName].Domains.Custom[domain].GetAsync(cancellationToken: cancellationToken), cancellationToken);
    }

    public ValueTask<R2EditCustomDomainSettings200?> UpdateCustomDomain(string accountId, string bucketName, string domain,
        R2EditCustomDomainRequest request, CancellationToken cancellationToken = default)
    {
        ValidateBucketName(bucketName);
        ArgumentException.ThrowIfNullOrWhiteSpace(domain);
        ArgumentNullException.ThrowIfNull(request);
        return Execute(accountId, nameof(UpdateCustomDomain),
            client => client.Accounts[accountId].R2.Buckets[bucketName].Domains.Custom[domain].PutAsync(request, cancellationToken: cancellationToken), cancellationToken);
    }

    public ValueTask<R2DeleteCustomDomain200?> DeleteCustomDomain(string accountId, string bucketName, string domain,
        CancellationToken cancellationToken = default)
    {
        ValidateBucketName(bucketName);
        ArgumentException.ThrowIfNullOrWhiteSpace(domain);
        return Execute(accountId, nameof(DeleteCustomDomain),
            client => client.Accounts[accountId].R2.Buckets[bucketName].Domains.Custom[domain].DeleteAsync(cancellationToken: cancellationToken), cancellationToken);
    }

    public ValueTask<R2GetBucketPublicPolicy200?> GetManagedDomain(string accountId, string bucketName, CancellationToken cancellationToken = default)
    {
        ValidateBucketName(bucketName);
        return Execute(accountId, nameof(GetManagedDomain),
            client => client.Accounts[accountId].R2.Buckets[bucketName].Domains.Managed.GetAsync(cancellationToken: cancellationToken), cancellationToken);
    }

    public ValueTask<R2PutBucketPublicPolicy200?> UpdateManagedDomain(string accountId, string bucketName, R2EditManagedDomainRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateBucketName(bucketName);
        ArgumentNullException.ThrowIfNull(request);
        return Execute(accountId, nameof(UpdateManagedDomain),
            client => client.Accounts[accountId].R2.Buckets[bucketName].Domains.Managed.PutAsync(request, cancellationToken: cancellationToken), cancellationToken);
    }
}

