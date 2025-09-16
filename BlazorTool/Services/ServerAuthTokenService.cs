using BlazorTool.Client.Models; 
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http; 

namespace BlazorTool.Services 
{
    public class ServerAuthTokenService
    {
        private readonly IMemoryCache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor; 

        public ServerAuthTokenService(IMemoryCache cache, IHttpContextAccessor httpContextAccessor) 
        {
            _cache = cache;
            _httpContextAccessor = httpContextAccessor; 
        }

        public async Task<string?> GetTokenAsync() 
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                Console.WriteLine("ServerAuthTokenService: HttpContext is null.");
                return null;
            }

            // Get PersonID from custom header
            if (!httpContext.Request.Headers.TryGetValue("X-Person-ID", out var personIdHeader) || !int.TryParse(personIdHeader, out int personID))
            {
                Console.WriteLine("ServerAuthTokenService: X-Person-ID header not found or invalid.");
                return null;
            }

            // Try to get IdentityData from cache using PersonID
            if (_cache.TryGetValue($"IdentityData_{personID}", out IdentityData? identityData) && identityData != null && !string.IsNullOrEmpty(identityData.Token))
            {
                return identityData.Token;
            }

            Console.WriteLine($"ServerAuthTokenService: Token not found in cache for PersonID: {personID}.");
            // Fallback: forward token from incoming Authorization header (useful for local dev)
            if (httpContext.Request.Headers.TryGetValue("Authorization", out var authHeaderValues))
            {
                var authHeader = authHeaderValues.ToString();
                const string bearerPrefix = "Bearer ";
                if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith(bearerPrefix, StringComparison.OrdinalIgnoreCase))
                {
                    var token = authHeader.Substring(bearerPrefix.Length).Trim();
                    if (!string.IsNullOrEmpty(token))
                    {
                        return token;
                    }
                }
            }
            return null; 
        }
    }
}
