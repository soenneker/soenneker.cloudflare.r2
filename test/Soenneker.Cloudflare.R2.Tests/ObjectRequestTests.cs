using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.Cloudflare.Utils.Client;

namespace Soenneker.Cloudflare.R2.Tests;

public sealed class ObjectRequestTests
{
    [Test, Arguments("a/b c/#?/%2F/λ", "a/b%20c/%23%3F/%252F/%CE%BB"), Arguments("/a//b/", "/a//b/"), Arguments("single key", "single%20key")]
    public async Task SingleObjectOperationsUseExactKey(string key, string escaped, CancellationToken cancellationToken)
    {
        using var recorder = new ObjectRequestRecorder();
        using var http = new System.Net.Http.HttpClient(recorder) { BaseAddress = new Uri("https://api.cloudflare.com/client/v4/") };
        var transport = new RecordingCloudflareHttpClient(http, "token");
        using var config = new ConfigurationManager();
        config["Cloudflare:ApiKey"] = "token";
        config["Cloudflare:RequestResponseLogging"] = "true";
        await using var clients = new CloudflareClientUtil(transport, config);
        var r2 = new CloudflareR2Util(clients);

        using var content = new MemoryStream([42]);
        var written = await r2.PutObject("account", "bucket", key, content, "application/x-ndjson", cancellationToken: cancellationToken);
        using var downloaded = await r2.GetObject("account", "bucket", key, cancellationToken: cancellationToken);
        await r2.DeleteObject("account", "bucket", key, cancellationToken: cancellationToken);

        string target = "/client/v4/accounts/account/r2/buckets/bucket/objects/" + escaped;
        await Assert.That(written?.Success).IsTrue();
        await Assert.That(downloaded?.ReadByte()).IsEqualTo(42);
        await Assert.That(recorder.Requests.Count).IsEqualTo(3);
        await Assert.That(recorder.Requests[0]).IsEqualTo((HttpMethod.Put, target, "application/x-ndjson"));
        await Assert.That(recorder.Requests[1].Target).IsEqualTo(target);
        await Assert.That(recorder.Requests[2].Target).IsEqualTo(target);
        await Assert.ThrowsAsync<ArgumentException>(() => r2.DeleteObject("account", "bucket", "a/../other", cancellationToken: cancellationToken).AsTask());
        await Assert.That(recorder.Requests.Count).IsEqualTo(3);
    }
}
