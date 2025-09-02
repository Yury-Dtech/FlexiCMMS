using BlazorTool.Client.Models;
using System.Diagnostics;
using System.Net.Http.Json;

namespace BlazorTool.Client.Services
{
    public partial class ApiServiceClient
    {

        #region Settings
        public async Task<ViewSettings<WorkOrder>> GetViewSettingsAsync(string user, string settingsName)
        {
            var url = $"settings/get-view-settings?user={Uri.EscapeDataString(user)}&settingsName={Uri.EscapeDataString(settingsName)}";
            try
            {
                var response = await _http.GetFromJsonAsync<ViewSettings<WorkOrder>>(url);
                return response ?? new ViewSettings<WorkOrder>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"ApiServiceClient: HTTP Request error during GET to {url}: {ex.Message}");
                Debug.WriteLine($"ApiServiceClient: HTTP Request error during GET to {url}: {ex.Message}");
                return new ViewSettings<WorkOrder>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ApiServiceClient: Unexpected error during GET to {url}: {ex.Message}");
                Debug.WriteLine($"ApiServiceClient: Unexpected error during GET to {url}: {ex.Message}");
                return new ViewSettings<WorkOrder>();
            }
        }

        public async Task<SingleResponse<bool>> SaveViewSettingsAsync(string user, string settingsName, ViewSettings<WorkOrder> viewSettings)
        {
            var url = $"settings/save-view-settings?user={Uri.EscapeDataString(user)}&settingsName={Uri.EscapeDataString(settingsName)}";
            try
            {
                var response = await _http.PostAsJsonAsync(url, viewSettings);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return new SingleResponse<bool> { IsValid = true, Data = true };
                }
                else
                {
                    // Attempt to deserialize error response if available
                    try
                    {
                        var errorResponse = System.Text.Json.JsonSerializer.Deserialize<SingleResponse<bool>>(responseContent);
                        if (errorResponse != null && (errorResponse.Errors?.Any() ?? false))
                        {
                            return new SingleResponse<bool>
                            {
                                Data = false,
                                IsValid = false,
                                Errors = errorResponse.Errors
                            };
                        }
                    }
                    catch { /* Ignore deserialization errors, fall through to generic error */ }

                    Console.WriteLine($"ApiServiceClient: Failed to save view settings. Status: {response.StatusCode}, Details: {responseContent}");
                    Debug.WriteLine($"ApiServiceClient: Failed to save view settings. Status: {response.StatusCode}, Details: {responseContent}");
                    return new SingleResponse<bool> { IsValid = false, Errors = new List<string> { $"Server error: {response.StatusCode} - {responseContent}" } };
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"ApiServiceClient: HTTP Request error during POST to {url}: {ex.Message}");
                Debug.WriteLine($"ApiServiceClient: HTTP Request error during POST to {url}: {ex.Message}");
                return new SingleResponse<bool> { IsValid = false, Errors = new List<string> { $"Network error: {ex.Message}" } };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ApiServiceClient: Unexpected error during POST to {url}: {ex.Message}");
                Debug.WriteLine($"ApiServiceClient: Unexpected error during POST to {url}: {ex.Message}");
                return new SingleResponse<bool> { IsValid = false, Errors = new List<string> { $"An unexpected error occurred: {ex.Message}" } };
            }
        }

        public async Task<(bool, string?)> LoadUserStateSettingsAsync(LoginRequest loginRequest)
        {
            var response = await PostSingleAsync<LoginRequest, IdentityData>("identity/loginpassword", loginRequest);
            await _userState.InitializationTask;
            if (response != null && response.IsValid && response.Data != null)
            {
                var identityData = response.Data;
                _userState.UserName = identityData.Name;
                _userState.Token = identityData.Token;
                _userState.Password = loginRequest.Password;
                _userState.LangCode = identityData.LangCode;
                _userState.UseOriginalColors = bool.TryParse(await LoadSettingAsync("useOriginalColors", loginRequest.Username), out var useColors) && useColors;
                _userState.NetworkShareUsername = await LoadSettingAsync("NetworkShareUsername", loginRequest.Username);
                _userState.NetworkSharePassword = await LoadSettingAsync("NetworkSharePassword", loginRequest.Username);
                _userState.NetworkShareServer = await LoadSettingAsync("NetworkShareServer", loginRequest.Username);
                identityData.NetworkShareServer = _userState.NetworkShareServer;
                identityData.NetworkSharePassword = _userState.NetworkSharePassword;
                identityData.NetworkShareUsername = _userState.NetworkShareUsername;
                identityData.UseOriginalColors = _userState.UseOriginalColors;
                bool isForceReload = await _userState.SaveIdentityDataToCacheAsync(identityData); // Save identityData to local storage
                return (isForceReload, null);
            }
            else
            {
                var errorMessage = response?.Errors?.FirstOrDefault();
                Console.WriteLine($"ApiServiceClient: Failed to load user state settings: {errorMessage}");
                Debug.WriteLine($"ApiServiceClient: Failed to load user state settings: {errorMessage}");
                return (false, string.Join(',', response?.Errors ?? Enumerable.Empty<string>()));
            }
        }
        public async Task<string> LoadSettingAsync(string key, string user)
        {
            var url = $"settings/get?key={Uri.EscapeDataString(key)}&user={Uri.EscapeDataString(user)}";
            try
            {
                var response = await _http.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var settingValue = await response.Content.ReadAsStringAsync();

                return settingValue;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error in GetSettingAsync: {ex.Message}");
                return $"Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in GetSettingAsync: {ex.Message}");
                return $"Error: {ex.Message}";
            }
        }

        public async Task<bool> SaveSettingAsync(string key, string value, string user)
        {
            var url = "settings/set";
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("key", key),
                new KeyValuePair<string, string>("value", value),
                new KeyValuePair<string, string>("user", user)
            });
            try
            {
                var response = await _http.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("\n= = = = = = = = = SaveSettingAsync error: " + response.ReasonPhrase + "\n");
                    return false;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"ApiServiceClient: HTTP Request error during POST to {url}: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ApiServiceClient: Unexpected error during POST to {url}: {ex.Message}");
                return false;
            }
        }

        #endregion

    }
}
