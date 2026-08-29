# Soenneker.Cloudflare.R2

[![NuGet](https://img.shields.io/nuget/v/Soenneker.Cloudflare.R2.svg)](https://www.nuget.org/packages/Soenneker.Cloudflare.R2/)
[![Build](https://github.com/soenneker/soenneker.cloudflare.r2/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/soenneker/soenneker.cloudflare.r2/actions/workflows/build-and-test.yml)

A convenient, dependency-injection friendly utility for Cloudflare R2. It wraps `Soenneker.Cloudflare.OpenApiClient` and provides direct operations for buckets, objects, configuration, domains, metrics, and temporary credentials.

## Installation

```bash
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

