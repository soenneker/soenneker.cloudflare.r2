using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Cloudflare.R2.Tests;

internal sealed class ObjectRequestRecorder : HttpMessageHandler
{
    public List<(HttpMethod Method, string Target, string? ContentType)> Requests { get; } = [];

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Requests.Add((request.Method, request.RequestUri!.PathAndQuery, request.Content?.Headers.ContentType?.MediaType));
        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = request.Method == HttpMethod.Get
                ? new ByteArrayContent([42])
                : new StringContent("""{"success":true}""", Encoding.UTF8, "application/json")
        });
    }
}
