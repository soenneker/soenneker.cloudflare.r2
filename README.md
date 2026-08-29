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

Configure the Cloudflare credentials used by `Soenneker.Cloudflare.Utils.Client`, then register the utility:

```csharp
using Soenneker.Cloudflare.R2.Registrars;

services.AddCloudflareR2UtilAsSingleton();
// Or: services.AddCloudflareR2UtilAsScoped();
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
        await _r2.PutObject(accountId, "assets", "images/logo.png", content, "image/png", cancellationToken);
    }

    public ValueTask<Stream?> Download(string accountId, CancellationToken cancellationToken) =>
        _r2.GetObject(accountId, "assets", "images/logo.png", cancellationToken);
}
```

Listing methods expose the generated client's strongly typed query parameters when pagination or filtering is needed:

```csharp
var response = await r2.ListObjects(accountId, "assets", query =>
{
    query.Prefix = "images/";
    query.Limit = 100;
}, cancellationToken);
```

The caller owns streams returned by `GetObject`. Upload streams are read from their current position and are not disposed by the utility.

## Supported operations

- Create, list, inspect, update, and delete buckets
- Upload, download, list, and delete objects, including bulk deletion
- Manage CORS, lifecycle, object lock, local uploads, and Sippy configuration
- Manage custom domains and the R2-managed public domain
- Read account-level metrics and create temporary access credentials
