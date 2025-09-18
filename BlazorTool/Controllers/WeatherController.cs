using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace BlazorTool.Controllers
{
    [Route("api/v1/weather")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string ApiKey = "c7a4b97d6e2446b592994537251709"; // Provided token (public repo caution)
        private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10);
        private static readonly Dictionary<string, (DateTime Utc, object Data)> _cache = new();

        public WeatherController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("current")] // GET api/v1/weather/current?lat=..&lng=..&lang=pl
        public async Task<IActionResult> GetCurrent([FromQuery] double? lat, [FromQuery] double? lng, [FromQuery] string lang = "en")
        {
            try
            {
                string query = lat.HasValue && lng.HasValue ? $"{lat.Value.ToString(System.Globalization.CultureInfo.InvariantCulture)},{lng.Value.ToString(System.Globalization.CultureInfo.InvariantCulture)}" : "auto:ip";
                string cacheKey = $"cur::{query}::{lang}";
                if (_cache.TryGetValue(cacheKey, out var entry) && DateTime.UtcNow - entry.Utc < CacheDuration)
                {
                    return Ok(entry.Data);
                }
                string weatherProviderURL = "api.weatherapi.com";
                var http = _httpClientFactory.CreateClient();
                var url = $"https://{weatherProviderURL}/v1/current.json?key={ApiKey}&q={Uri.EscapeDataString(query)}&aqi=no&lang={Uri.EscapeDataString(lang)}";
                using var resp = await http.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                if (!resp.IsSuccessStatusCode)
                {
                    weatherProviderURL = "138.199.36.9"; // Fallback to direct IP if DNS fails
                    url = $"https://{weatherProviderURL}/v1/current.json?key={ApiKey}&q={Uri.EscapeDataString(query)}&aqi=no&lang={Uri.EscapeDataString(lang)}";
                    using var resp2 = await http.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                    if (!resp2.IsSuccessStatusCode)
                    {
                        return StatusCode((int)resp2.StatusCode);
                    }
                }
                var json = await resp.Content.ReadFromJsonAsync<CurrentRoot>();
                if (json == null)
                {
                    return NoContent();
                }
                _cache[cacheKey] = (DateTime.UtcNow, json);
                return Ok(json);
            }
            catch
            {
                return StatusCode(502);
            }
        }

        [HttpGet("astronomy")] // GET api/v1/weather/astronomy?lat=..&lng=..&lang=pl
        public async Task<IActionResult> GetAstronomy([FromQuery] double? lat, [FromQuery] double? lng, [FromQuery] string lang = "en")
        {
            try
            {
                string query = lat.HasValue && lng.HasValue ? $"{lat.Value.ToString(System.Globalization.CultureInfo.InvariantCulture)},{lng.Value.ToString(System.Globalization.CultureInfo.InvariantCulture)}" : "auto:ip";
                string cacheKey = $"astro::{query}::{lang}";
                if (_cache.TryGetValue(cacheKey, out var entry) && DateTime.UtcNow - entry.Utc < CacheDuration)
                {
                    return Ok(entry.Data);
                }

                var http = _httpClientFactory.CreateClient();
                var url = $"https://api.weatherapi.com/v1/astronomy.json?key={ApiKey}&q={Uri.EscapeDataString(query)}&dt={DateTime.UtcNow:yyyy-MM-dd}";
                using var resp = await http.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                if (!resp.IsSuccessStatusCode)
                {
                    return StatusCode((int)resp.StatusCode);
                }
                var json = await resp.Content.ReadFromJsonAsync<AstronomyRoot>();
                if (json == null)
                {
                    return NoContent();
                }
                _cache[cacheKey] = (DateTime.UtcNow, json);
                return Ok(json);
            }
            catch
            {
                return StatusCode(502);
            }
        }

        public class CurrentRoot
        {
            [JsonPropertyName("location")] public Location? Location { get; set; }
            [JsonPropertyName("current")] public Current? Current { get; set; }
        }
        public class Location { [JsonPropertyName("name")] public string? Name { get; set; } }
        public class Current { [JsonPropertyName("temp_c")] public double TempC { get; set; } [JsonPropertyName("condition")] public Condition? Condition { get; set; } }
        public class Condition { [JsonPropertyName("text")] public string? Text { get; set; } [JsonPropertyName("icon")] public string? Icon { get; set; } }

        public class AstronomyRoot { [JsonPropertyName("astronomy")] public Astronomy? Astronomy { get; set; } }
        public class Astronomy { [JsonPropertyName("astro")] public Astro? Astro { get; set; } }
        public class Astro { [JsonPropertyName("sunrise")] public string? Sunrise { get; set; } [JsonPropertyName("sunset")] public string? Sunset { get; set; } }
    }
}
