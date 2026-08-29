using Soenneker.Cloudflare.R2.Abstract;
using Soenneker.Tests.HostedUnit;

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
    public void Default()
    {

    }
}
