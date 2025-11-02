using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace XUnitTestProject
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri!.AbsoluteUri.Contains("token"))
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("""
                {
                    "access_token": "fake-token",
                    "token_type": "Bearer",
                    "expires_in": 3600
                }
                """, System.Text.Encoding.UTF8, "application/json")
                });
            }

            if (request.RequestUri!.AbsoluteUri.Contains("userinfo"))
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("""
                {
                    "id": "123",
                    "email": "user@example.com",
                    "name": "John Doe",
                    "picture": "https://example.com/avatar.jpg"
                }
                """, System.Text.Encoding.UTF8, "application/json")
                });
            }

            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
        }
    }
}
