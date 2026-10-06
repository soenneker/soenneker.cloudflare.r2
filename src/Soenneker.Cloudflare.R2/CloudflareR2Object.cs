using System;

namespace Soenneker.Cloudflare.R2;

/// <summary>An R2 object's bytes and the ETag read with those bytes.</summary>
public sealed record CloudflareR2Object(ReadOnlyMemory<byte> Content, string ETag);
