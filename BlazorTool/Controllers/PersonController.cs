using BlazorTool.Client.Models;
using BlazorTool.Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; // To access configuration for Basic Auth credentials
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BlazorTool.Controllers
{
    [Route("api/v1/other")] // Changed to "other" as per the request URL "api/v1/other/getuserslist"
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public PersonController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet("getuserslist")]
        public async Task<IActionResult> GetUsersList()
        {
            try
            {
                var basicAuthClient = _httpClientFactory.CreateClient("ExternalApiBasicAuthClient");

                // Get Basic Auth credentials from configuration
                var username = _configuration["ExternalApi:BasicAuthUsername"];
                var password = _configuration["ExternalApi:BasicAuthPassword"];

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    return StatusCode(500, new ApiResponse<Person>
                    {
                        Data = new List<Person>(),
                        IsValid = false,
                        Errors = new() { "Basic Auth credentials (ExternalApi:BasicAuthUsername or ExternalApi:BasicAuthPassword) are not configured." }
                    });
                }

                var byteArray = Encoding.UTF8.GetBytes($"{username}:{password}");
                basicAuthClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                // Directly call the external API using the basicAuthClient
                var wrapper = await basicAuthClient.GetFromJsonAsync<ApiResponse<Person>>("other/getuserslist");
                Console.WriteLine($"Request persons. Count = {wrapper?.Data?.Count ?? 0}");
                return Ok(wrapper);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error PersonController. ==> getuserslist. :: {ex.Message}");
                return StatusCode(500, new ApiResponse<Person>
                {
                    Data = new List<Person>(),
                    IsValid = false,
                    Errors = new List<string> { $"Error calling external API: {ex.Message}" }
                });
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing PersonController. ==> getuserslist. :: {ex.Message}");
                return StatusCode(500, new ApiResponse<Person>
                {
                    Data = new List<Person>(),
                    IsValid = false,
                    Errors = new List<string> { $"Error deserializing external API response: {ex.Message}" }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in PersonController. ==> getuserslist. :: {ex.Message}");
                return StatusCode(500, new ApiResponse<Person>
                {
                    Data = new List<Person>(),
                    IsValid = false,
                    Errors = new List<string> { $"An unexpected error occurred: {ex.Message}" }
                });
            }
        }
    }
}
