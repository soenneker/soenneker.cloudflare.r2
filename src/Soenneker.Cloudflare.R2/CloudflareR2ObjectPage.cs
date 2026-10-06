namespace Soenneker.Cloudflare.R2;

/// <summary>A page of keys and an opaque continuation cursor; null marks the last page.</summary>
public sealed record CloudflareR2ObjectPage(string[] Keys, string? Cursor);
