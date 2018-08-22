using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace VASJ.BI
{
    public class SwaggerAccessMessageHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var isDevelopment = System.Configuration.ConfigurationManager.AppSettings["IsDebugMode"] == "1";

            //If user request to '../swagger' and in the Production environment
            if (IsSwagger(request) && !isDevelopment)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Redirect);

                // Redirect to login URL
                string uri = string.Format("{0}://{1}", request.RequestUri.Scheme, request.RequestUri.Authority);
                response.Headers.Location = new Uri(uri);
                return Task.FromResult(response);
            }
            else
            {
                return base.SendAsync(request, cancellationToken);
            }
        }

        private bool IsSwagger(HttpRequestMessage request)
        {
            return request.RequestUri.PathAndQuery.Contains("/swagger");
        }
    }
}