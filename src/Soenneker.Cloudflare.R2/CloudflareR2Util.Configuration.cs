using Soenneker.Cloudflare.OpenApiClient.Models;
using Soenneker.Extensions.Task;
using Soenneker.Extensions.ValueTask;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Cloudflare.OpenApiClient;

namespace Soenneker.Cloudflare.R2;

public sealed partial class CloudflareR2Util
{
    public async ValueTask<R2GetBucketCorsPolicy200Response?> GetCorsPolicy(string accountId, string bucketName, string? apiKey = null, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await GetClient(apiKey, cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].Cors.GetAsync(cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<R2PutBucketCorsPolicy200Response?> PutCorsPolicy(string accountId, string bucketName, R2PutBucketCorsPolicyRequest request,
        string? apiKey = null, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await GetClient(apiKey, cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].Cors.PutAsync(request, cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<R2DeleteBucketCorsPolicy200Response?> DeleteCorsPolicy(string accountId, string bucketName, string? apiKey = null, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await GetClient(apiKey, cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].Cors.DeleteAsync(cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<R2GetBucketLifecycleConfiguration200Response?> GetLifecycleConfiguration(string accountId, string bucketName,
        string? apiKey = null, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await GetClient(apiKey, cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].Lifecycle.GetAsync(cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<R2PutBucketLifecycleConfiguration200Response?> PutLifecycleConfiguration(string accountId, string bucketName,
        R2PutBucketLifecycleConfigurationRequest request, string? apiKey = null, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await GetClient(apiKey, cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].Lifecycle.PutAsync(request, cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<R2GetBucketLockConfiguration200Response?> GetLockConfiguration(string accountId, string bucketName,
        string? apiKey = null, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await GetClient(apiKey, cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].Lock.GetAsync(cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<R2PutBucketLockConfiguration200Response?> PutLockConfiguration(string accountId, string bucketName,
        R2PutBucketLockConfigurationRequest request, string? apiKey = null, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await GetClient(apiKey, cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].Lock.PutAsync(request, cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<R2GetBucketLocalUploadsConfiguration200Response?> GetLocalUploadsConfiguration(string accountId, string bucketName,
        string? apiKey = null, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await GetClient(apiKey, cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].LocalUploads.GetAsync(cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<R2PutBucketLocalUploadsConfiguration200Response?> PutLocalUploadsConfiguration(string accountId, string bucketName,
        R2PutBucketLocalUploadsConfigurationRequest request, string? apiKey = null, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await GetClient(apiKey, cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].LocalUploads.PutAsync(request, cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<R2GetBucketSippyConfig200Response?> GetSippyConfiguration(string accountId, string bucketName,
        string? apiKey = null, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await GetClient(apiKey, cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].Sippy.GetAsync(cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<R2PutBucketSippyConfig200Response?> PutSippyConfiguration(string accountId, string bucketName, R2PutBucketSippyConfigRequest request,
        string? apiKey = null, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await GetClient(apiKey, cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].Sippy.PutAsync(request, cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<R2DeleteBucketSippyConfig200Response?> DeleteSippyConfiguration(string accountId, string bucketName,
        string? apiKey = null, CancellationToken cancellationToken = default)
    {
        CloudflareOpenApiClient client = await GetClient(apiKey, cancellationToken).NoSync();
        return await client.Accounts[accountId].R2.Buckets[bucketName].Sippy.DeleteAsync(cancellationToken: cancellationToken).NoSync();
    }
}
