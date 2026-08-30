[![](https://img.shields.io/nuget/v/soenneker.cloudflare.r2.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.cloudflare.r2/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.cloudflare.r2/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.cloudflare.r2/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.cloudflare.r2.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.cloudflare.r2/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.cloudflare.r2/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.cloudflare.r2/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Cloudflare.R2
### A utility for managing Cloudflare R2 storage

This library uses the generated `Soenneker.Cloudflare.OpenApiClient` (Kiota) client and provides dependency-injection-friendly operations for buckets, objects, configuration, domains, metrics, and temporary credentials.

## Installation

```
dotnet add package Soenneker.Cloudflare.R2
```

## Registration

Configure a Cloudflare API token with the R2 permissions required by the operations your application performs:

```json
{
  "Cloudflare": {
    "ApiKey": "<Cloudflare API token>"
  }
}
```

The token is sent as a bearer credential for Cloudflare management API calls. Keep it in a secret provider rather than source control or a checked-in settings file.

Then register the utility:

```csharp
using Soenneker.Cloudflare.R2.Registrars;

services.AddCloudflareR2UtilAsSingleton();
// Or: services.AddCloudflareR2UtilAsScoped();
```

The configured token is used by default. Any management operation can use another token for that call through the parameter named `apiKey`:

```csharp
await r2.GetBucket(accountId, "assets", apiKey: tenantApiKey, cancellationToken: cancellationToken);
```

## Usage

```csharp
using Soenneker.Cloudflare.R2.Abstract;

public sealed class AssetStore
{
    private readonly ICloudflareR2Util _r2;

    public AssetStore(ICloudflareR2Util r2)
    {
        _r2 = r2;
    }

    public async ValueTask Upload(string accountId, Stream content, CancellationToken cancellationToken)
    {
        await _r2.PutObject(accountId, "assets", "images/logo.png", content, "image/png", cancellationToken: cancellationToken);
    }

    public ValueTask<Stream?> Download(string accountId, CancellationToken cancellationToken) =>
        _r2.GetObject(accountId, "assets", "images/logo.png", cancellationToken: cancellationToken);
}
```

Listing methods expose the generated client's strongly typed query parameters when pagination or filtering is needed:

```csharp
var response = await r2.ListObjects(accountId, "assets", query =>
{
    query.Prefix = "images/";
    query.Limit = 100;
}, cancellationToken: cancellationToken);
```

The caller owns streams returned by `GetObject`. Upload streams are read from their current position and are not disposed by the utility.

Private objects can be shared for a limited time with an R2 access key ID and secret access key. Credentials are supplied per call, so the same utility instance can safely use different R2 credential sets:

```csharp
string downloadUrl = await r2.GetPresignedDownloadUrl(
    accountId,
    "assets",
    "private/report.pdf",
    accessKeyId,
    secretAccessKey,
    TimeSpan.FromMinutes(15),
    cancellationToken: cancellationToken);
```

Pass the optional `sessionToken` when signing with temporary R2 credentials. Presigned URLs may remain valid from one second through seven days. URL signing is provided by `Soenneker.Aws.Signing.V4`; no Amazon SDK package is required.

The R2 access key ID and secret access key used for presigning are S3-compatible R2 credentials. They are separate from the Cloudflare management API token in `Cloudflare:ApiKey`. Treat the complete presigned URL as a credential until it expires and do not log it.

## Supported operations

- Create, list, inspect, update, and delete buckets
- Upload, download, list, and delete objects, including bulk deletion
- Generate time-limited download URLs for private objects using call-specific R2 credentials
- Manage CORS, lifecycle, object lock, local uploads, and Sippy configuration
- Manage custom domains and the R2-managed public domain
- Read account-level metrics and create temporary access credentials

## Behavior

- Generated response bodies are nullable because Cloudflare may return no body for some successful operations.
- Management API failures are surfaced through the generated Kiota client rather than converted to library-specific exceptions.
- Per-call API tokens are cached by the underlying client utility for reuse. Prefer a bounded set of long-lived tokens rather than passing unbounded, one-off credentials through a singleton registration.
- Cancellation is passed through client acquisition and the Cloudflare request. Presigned URL creation is local and observes cancellation before signing.
