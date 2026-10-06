using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Cloudflare.R2.Abstract;

/// <summary>Uses an authenticated R2 Worker gateway exposing GET/PUT objects and GET ?prefix=&amp;cursor= listings.</summary>
/// <remarks>The gateway must forward If-Match/If-None-Match to R2.put onlyIf, return 412 only for a failed condition,
/// return ETag on successful reads/writes, and return CloudflareR2ObjectPage JSON for listings. Conditions are evaluated atomically by R2. The caller owns HttpClient and must configure it without automatic write retries or redirects.</remarks>
public interface ICloudflareR2ObjectStore
{
    /// <summary>Reads bytes and their ETag together; returns null only for a missing object. Enforces the supplied byte limit.</summary>
    ValueTask<CloudflareR2Object?> Read(string key, int maxBytes = 16 * 1024 * 1024, CancellationToken cancellationToken = default);

    /// <summary>Writes bytes if the ETag matches, or creates only when absent if expectedETag is null. Returns the new ETag, or null on a confirmed conflict.</summary>
    /// <remarks>Never retry transport failures automatically: a dispatched write can have committed despite a lost response.</remarks>
    ValueTask<string?> Write(string key, ReadOnlyMemory<byte> content, string? expectedETag = null, CancellationToken cancellationToken = default);

    /// <summary>Lists one page of object keys under a prefix. Listings are not a multi-object transactional snapshot.</summary>
    ValueTask<CloudflareR2ObjectPage> List(string prefix, string? cursor = null, CancellationToken cancellationToken = default);
}
