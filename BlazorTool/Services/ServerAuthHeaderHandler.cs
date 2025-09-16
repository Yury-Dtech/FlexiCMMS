using BlazorTool.Services; 
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorTool.Services
{
    /// <summary>
    /// add Bearer-token to requests ApiServiceClient to server.
    /// </summary>
    public class ServerAuthHeaderHandler : DelegatingHandler
    {
        private readonly ServerAuthTokenService _tokenService;

        public ServerAuthHeaderHandler(ServerAuthTokenService tokenService)
        {
            _tokenService = tokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _tokenService.GetTokenAsync();

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                Console.WriteLine("ServerAuthHeaderHandler: No token available for outgoing request.");
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
