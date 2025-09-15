using BlazorTool.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorTool.Controllers
{
    [ApiController]
    [Route("api/v1/identity")]
    public class IdentityController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;

        public IdentityController(IHttpClientFactory httpClientFactory, IConfiguration configuration, IMemoryCache cache)       
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _cache = cache;
        }

        [HttpPost("loginpassword")]
        public async Task<IActionResult> LoginPassword([FromBody] BlazorTool.Client.Models.LoginRequest loginRequest)
        {
            var client = _httpClientFactory.CreateClient("ExternalApiBasicAuthClient");

            // Basic Auth from appsettings.json (ExternalApi:)
            var basicAuthUsername = _configuration["ExternalApi:BasicAuthUsername"] ?? string.Empty;
            var basicAuthPassword = _configuration["ExternalApi:BasicAuthPassword"] ?? string.Empty;
            if (!string.IsNullOrEmpty(basicAuthUsername) && !string.IsNullOrEmpty(basicAuthPassword))
            {
                var basicAuth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{basicAuthUsername}:{basicAuthPassword}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", basicAuth);
            }
            var externalApiRelativeUrl = $"identity/loginpass?UserName={Uri.EscapeDataString(loginRequest.Username)}&Password={Uri.EscapeDataString(loginRequest.Password)}";
            var request = new HttpRequestMessage(HttpMethod.Get, externalApiRelativeUrl);
            Console.WriteLine($"Request URL: {request.RequestUri}");
            Console.WriteLine("BaseUrl= " + client.BaseAddress);
            var response = await client.SendAsync(request);
            var statusCode = (int)response.StatusCode;
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = JsonSerializer.Deserialize<SingleResponse<IdentityData>>(content);
                if (apiResponse != null && apiResponse.IsValid && apiResponse.Data != null)
                {
                    // Cache the IdentityData for the user
                    _cache.Set($"IdentityData_{apiResponse.Data.PersonID}", apiResponse.Data, TimeSpan.FromHours(5));
                    Console.WriteLine();
                    Console.WriteLine($"==========>  User {apiResponse.Data.Name} logged in successfully <==============");
                    Console.WriteLine();
                }
            }
            else
                {
                    Console.WriteLine();
                    Console.WriteLine($"==========>  Login failed with status code {statusCode}: {content} <==============");
                    Console.WriteLine();
                }
            foreach (var header in response.Content.Headers)
            {
                Response.Headers[header.Key] = header.Value.ToArray();
            }
            return new ContentResult
            {
                StatusCode = statusCode,
                Content = content,
                ContentType = response.Content.Headers.ContentType?.ToString() ?? "application/json"
            };
        }

        [HttpGet("check-session")]
        public IActionResult CheckSession()
        {
            if (Request.Headers.TryGetValue("X-Person-ID", out var personIdValues) && int.TryParse(personIdValues.FirstOrDefault(), out int personId))
            {
                if (_cache.TryGetValue($"IdentityData_{personId}", out _))
                {
                    return Ok("Session is active.");
                }
            }
            return Unauthorized("Session not found or PersonID header is missing/invalid.");
        }
    }

    public class LoginPasswordRequest
    {
        [JsonPropertyName("PersonID")]
        public int PersonID { get; set; }

        [JsonPropertyName("Password")]
        public string Password { get; set; } = default!;
    }
}
