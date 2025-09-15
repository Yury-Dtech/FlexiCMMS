using BlazorTool.Client.Models;
using BlazorTool.Client.Services;
using BlazorTool.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Threading;
using static System.Net.WebRequestMethods;

namespace BlazorTool.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SettingsController : Controller
    {
        private readonly ApiServiceClient _apiServiceClient;
        private readonly HttpClient _http = new HttpClient();
        private const string SettingsDirectory = "Settings";
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IHostApplicationLifetime _lifetime;
        private static readonly SemaphoreSlim _fileLock = new SemaphoreSlim(1, 1);
        public SettingsController(ApiServiceClient apiServiceClient, IWebHostEnvironment env, IConfiguration configuration, IHostApplicationLifetime lifetime)
        {
            _apiServiceClient = apiServiceClient;
            _env = env;
            _configuration = configuration;
            _lifetime = lifetime;
        }

        // GET: Settings var url = $"api/v1/settings/get?key=address}";
        [HttpGet("get")]
        public string Read(string key, string? user)
        {
            if (key == "apiAddress")
            {
                return _configuration["ExternalApi:BaseUrl"] ?? string.Empty;
            }

            if (string.IsNullOrEmpty(user))
            {
                return _configuration[key] ?? string.Empty;
            }
            //read settings from server local file
            string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SettingsDirectory, user + ".json");
            var settings = new UserSettings(file);
           return settings.GetSetting<string>(user, key) ?? string.Empty;
        }

        
        [HttpPost("set")]
        public async Task<bool> Save([FromForm] string key, [FromForm] string value, [FromForm] string user)
        {
            try
            {
                if (key == "apiAddress")
                {
                    await _fileLock.WaitAsync();
                    var appSettingsPath = Path.Combine(_env.ContentRootPath, "appsettings.json");
                    var json = System.IO.File.ReadAllText(appSettingsPath);
                    var jsonObj = System.Text.Json.Nodes.JsonNode.Parse(json).AsObject();

                    var externalApiNode = jsonObj.ContainsKey("ExternalApi") 
                        ? jsonObj["ExternalApi"].AsObject() 
                        : new System.Text.Json.Nodes.JsonObject();
                    
                    externalApiNode["BaseUrl"] = value;
                    jsonObj["ExternalApi"] = externalApiNode;

                    var options = new System.Text.Json.JsonSerializerOptions { WriteIndented = true };
                    System.IO.File.WriteAllText(appSettingsPath, jsonObj.ToJsonString(options));
                    Console.WriteLine($"API address saved to appsettings.json: {value}");
                    Console.WriteLine("Application will now shut down to apply changes.");
                    _fileLock.Release();
                    _lifetime.StopApplication();

                    return true; 
                }

                string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SettingsDirectory, user + ".json");
                var settings = new UserSettings(file);
                await settings.SetSetting(user, key, value);
                Console.WriteLine($"Setting saved: {user} - {key} = {value}");
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error saving setting: {ex.Message}");
                Debug.WriteLine($"Error saving setting: {ex.Message}");
            }
            return false;
        }

        [HttpGet("get-view-settings")]
        public ViewSettings<WorkOrder> GetViewSettings(string user, string settingsName)
        {
            if (string.IsNullOrEmpty(user))
            {
                return null;
            }
            if (string.IsNullOrEmpty(settingsName))
            {
                return null;
            }

            try
            {
                string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SettingsDirectory, user + ".json");
                var settings = new UserSettings(file);
                var viewSettings = settings.GetUserSettings<ViewSettings<WorkOrder>>(user, settingsName);
                return viewSettings;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting view settings: {ex.Message}");
                //return StatusCode(500, "An error occurred while retrieving view settings.");
                return null;
            }
        }

        [HttpPost("save-view-settings")]
        public IActionResult SaveViewSettings([FromQuery] string user, [FromQuery] string settingsName, [FromBody] ViewSettings<WorkOrder> viewSettings)
        {
            if (string.IsNullOrEmpty(user))
            {
                return BadRequest("User cannot be empty.");
            }
            if (string.IsNullOrEmpty(settingsName))
            {
                return BadRequest("SettingsName cannot be empty.");
            }

            if (viewSettings == null)
            {
                return BadRequest("ViewSettings object cannot be null.");
            }

            try
            {
                string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SettingsDirectory, user + ".json");
                var settings = new UserSettings(file);
                settings.SaveUserSettings(user, settingsName, viewSettings);
                //Console.WriteLine($"View settings saved for user: {user}");
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving view settings: {ex.Message}");
                Debug.WriteLine($"Error saving view settings: {ex.Message}");
                return StatusCode(500, "An error occurred while saving view settings.");
            }
        }

        [HttpPost("check")]
        public async Task<IActionResult> CheckAsync([FromForm] string address)
        {
            //make test request to the API address
            try
            {
                //ping nie dziala na Linux
                //string ipAddress = ExtractIpAddress(address);
                //if (string.IsNullOrEmpty(ipAddress))
                //{
                //    return Ok(new { success = false, message = "API address is invalid. No IP address found." });
                //}

                //Console.WriteLine("Pinging IP address: " + ipAddress);
                //if (PingIpAddress(ipAddress))
                //{
                //    Console.WriteLine("Ping successful.");
                //}
                //else
                //{
                //    return Ok(new { success = false, message = "API address is invalid. Ping failed." });
                //}

                //combine url address
                if (!address.StartsWith("http://")) address = "http://" + address;
                if (!address.EndsWith("/")) address += "/";
                var testUrl = address + "wo/getlist?Lang=pl-PL&DeviceID=0";
                var response = await _http.GetAsync(testUrl);
                if (response.IsSuccessStatusCode)
                {
                    return Ok(new { success = true, message = "API address is valid." });
                }
                else
                {
                    try
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            return Ok(new { success = true, message = "API address is valid." });
                        // Try to read the response content as JSON
                        //if json valid and contains field Error - good
                        var responseContent = await response.Content.ReadFromJsonAsync<SingleResponse<WorkOrder>>();
                        if (responseContent == null)
                            return Json(new { success = false, message = $"API address is invalid. Status code: {response.StatusCode}" });
                        if (responseContent.Errors != null && responseContent.Errors.Count > 0)
                        {
                            return Json(new { success = true, message = "API address is valid" });
                        }
                        else
                        {
                            return Json(new { success = false, message = $"API address is invalid. Status code: {response.StatusCode}, Errors: {string.Join(", ", responseContent.Errors)}" });
                        }
                    }
                    catch (Exception)
                    {
                        return Json(new { success = false, message = $"API address is invalid. Status code: {response.StatusCode}" });
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                return Json(new { success = false, message = $"API address is invalid. Error: {ex.Message}" });
            }
        }

        // POST: Settings/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Settings/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Settings/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public static string ExtractIpAddress(string apiAddress)
        {
            // Регулярное выражение для извлечения IPv4-адреса
            Match match = Regex.Match(apiAddress, @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
            if (match.Success)
            {
                return match.Value;
            }
            return null;
        }

        public static bool PingIpAddress(string ipAddress)
        {
            using (Ping pingSender = new Ping())
            {
                try
                {
                    int timeout = 1000;

                    PingReply reply = pingSender.Send(ipAddress, timeout);

                    if (reply.Status == IPStatus.Success)
                    {
                        Console.WriteLine($"  response from {reply.Address}: bytes={reply.Buffer.Length} timeout={reply.RoundtripTime}ms TTL={reply.Options.Ttl}");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"  Ping failed. Status: {reply.Status}");
                        return false;
                    }
                }
                catch (PingException ex)
                {
                    Console.WriteLine($"  Ping error: {ex.Message}");
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  Unknown error: {ex.Message}");
                    return false;
                }
            }
        }
    }
}
