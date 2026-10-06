using System.Text.Json.Serialization;

namespace Soenneker.Cloudflare.R2;

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(CloudflareR2ObjectPage))]
internal partial class CloudflareR2ObjectJsonContext : JsonSerializerContext;
