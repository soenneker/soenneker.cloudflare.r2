using System.Threading;
using Soenneker.Cloudflare.R2.Abstract;
using Soenneker.Tests.HostedUnit;
using System;
using System.Threading.Tasks;

namespace Soenneker.Cloudflare.R2.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class CloudflareR2UtilTests : HostedUnitTest
{
    private readonly ICloudflareR2Util _util;

    public CloudflareR2UtilTests(Host host) : base(host)
    {
        _util = Resolve<ICloudflareR2Util>(true);
    }

    [Test]
    public async Task GetPresignedDownloadUrl_should_include_target_credentials_and_expiration(CancellationToken cancellationToken)
    {
        const string accessKeyId = "access-key-one";

        string url = await _util.GetPresignedDownloadUrl("account-id", "private-bucket", "reports/annual report.pdf", accessKeyId,
            "secret-key-one", TimeSpan.FromMinutes(15), cancellationToken: cancellationToken);

        await Assert.That(url).StartsWith("https://account-id.r2.cloudflarestorage.com/private-bucket/reports/annual%20report.pdf?");
        await Assert.That(url).Contains("X-Amz-Expires=900");
        await Assert.That(url).Contains($"X-Amz-Credential={accessKeyId}%2F");
        await Assert.That(url).Contains("X-Amz-Signature=");
    }

    [Test]
    public async Task GetPresignedDownloadUrl_should_support_temporary_credentials(CancellationToken cancellationToken)
    {
        string url = await _util.GetPresignedDownloadUrl("account-id", "private-bucket", "report.pdf", "temporary-access-key",
            "temporary-secret-key", TimeSpan.FromMinutes(5), "temporary-session-token", cancellationToken);

        await Assert.That(url).Contains("X-Amz-Credential=temporary-access-key%2F");
        await Assert.That(url).Contains("X-Amz-Security-Token=temporary-session-token");
    }

    [Test]
    public async Task GetPresignedDownloadUrl_should_reject_invalid_duration(CancellationToken cancellationToken)
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            _util.GetPresignedDownloadUrl("account-id", "private-bucket", "report.pdf", "access-key", "secret-key", TimeSpan.Zero, cancellationToken: cancellationToken).AsTask());
    }
}
