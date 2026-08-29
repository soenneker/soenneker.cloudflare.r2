using Soenneker.Cloudflare.OpenApiClient.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Cloudflare.R2.Abstract;

public partial interface ICloudflareR2Util
{
    ValueTask<R2ListCustomDomains200?> ListCustomDomains(string accountId, string bucketName, CancellationToken cancellationToken = default);
    ValueTask<R2AddCustomDomain200?> AddCustomDomain(string accountId, string bucketName, R2AddCustomDomainRequest request,
        CancellationToken cancellationToken = default);
    ValueTask<R2GetCustomDomainSettings200?> GetCustomDomain(string accountId, string bucketName, string domain,
        CancellationToken cancellationToken = default);
    ValueTask<R2EditCustomDomainSettings200?> UpdateCustomDomain(string accountId, string bucketName, string domain,
        R2EditCustomDomainRequest request, CancellationToken cancellationToken = default);
    ValueTask<R2DeleteCustomDomain200?> DeleteCustomDomain(string accountId, string bucketName, string domain,
        CancellationToken cancellationToken = default);
    ValueTask<R2GetBucketPublicPolicy200?> GetManagedDomain(string accountId, string bucketName, CancellationToken cancellationToken = default);
    ValueTask<R2PutBucketPublicPolicy200?> UpdateManagedDomain(string accountId, string bucketName, R2EditManagedDomainRequest request,
        CancellationToken cancellationToken = default);
}

