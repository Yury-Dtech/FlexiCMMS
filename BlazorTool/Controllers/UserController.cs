using BlazorTool.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace BlazorTool.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetUsersList()
        {
            try
            {
                var bearerAuthClient = _httpClientFactory.CreateClient("ExternalApiBearerAuthClient");
                var wrapper = await bearerAuthClient.GetFromJsonAsync<ApiResponse<UserInfo>>("user/getlist");
                Console.WriteLine($"Request UserInfo list. Count = {wrapper?.Data?.Count ?? 0}");
                return Ok(wrapper);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error UserController. ==> getlist. :: {ex.Message}");
                return StatusCode(500, new ApiResponse<UserInfo>
                {
                    Data = new List<UserInfo>(),
                    IsValid = false,
                    Errors = new List<string> { $"Error calling external API: {ex.Message}" }
                });
            }
            catch (JsonSerializationException ex)
            {
                Console.WriteLine($"Error deserializing UserController. ==> getlist. :: {ex.Message}");
                return StatusCode(500, new ApiResponse<UserInfo>
                {
                    Data = new List<UserInfo>(),
                    IsValid = false,
                    Errors = new List<string> { $"Error deserializing external API response: {ex.Message}" }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in UserController. ==> getlist. :: {ex.Message}");
                return StatusCode(500, new ApiResponse<UserInfo>
                {
                    Data = new List<UserInfo>(),
                    IsValid = false,
                    Errors = new List<string> { $"An unexpected error occurred: {ex.Message}" }
                });
            }
        }
    }
}
