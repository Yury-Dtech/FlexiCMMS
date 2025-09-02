using BlazorTool.Client.Models;
using System.Diagnostics;
using System.Net.Http.Json;

namespace BlazorTool.Client.Services

{
    public partial class ApiServiceClient
    {
        private readonly HttpClient _http;
        private Dictionary<int, List<WorkOrder>> _workOrdersCache = new Dictionary<int, List<WorkOrder>>();
        private List<Device> _devicesCache = new List<Device>();
        private readonly UserState _userState;
        private List<WODict> _dictCache = new List<WODict>();
        private List<WODict> _actDictCache = new List<WODict>();
        private List<Person> _personsCache = new List<Person>();
        private Dictionary<int, ApiResponse<WorkOrderFile>> _workOrderFilesCache = new Dictionary<int, ApiResponse<WorkOrderFile>>();
        private Dictionary<string, SingleResponse<WorkOrderFileData>> _workOrderFileCache = new Dictionary<string, SingleResponse<WorkOrderFileData>>();

        private Dictionary<string, ApiResponse<DeviceStatus>> _deviceStatusCache = new Dictionary<string, ApiResponse<DeviceStatus>>();
        private Dictionary<string, SingleResponse<DeviceImage>> _deviceImageCache = new Dictionary<string, SingleResponse<DeviceImage>>();
        private Dictionary<string, ApiResponse<DeviceDict>> _deviceDictCache = new Dictionary<string, ApiResponse<DeviceDict>>();

        // Cache for work orders retrieved with person filter
        private Dictionary<int, List<WorkOrder>> _workOrdersWithPersonCache = new Dictionary<int, List<WorkOrder>>();
        public ApiServiceClient(HttpClient http, UserState userState)
        {
            _http = http;
            _userState = userState;            
        }

        #region other functions
        public async Task<HttpResponseMessage> CheckSessionAsync()
        {
            return await _http.GetAsync("identity/check-session");
        }

        private string GetColorByStateId(int stateId)
        {
            return stateId switch
            {
                5 => "bg-success-light",
                0 => "bg-success-light",
                1 => "bg-danger-light",
                2 => "bg-warning-light",
                6 => "bg-warning-light",
                _ => "bg-gray-light"
            };
        }

        /// <summary>
        /// Checks the validity of SMB credentials by calling the DeviceController's CheckCredentials endpoint.
        /// </summary>
        /// <param name="credentials">The SMB credentials to check.</param>
        /// <returns>A tuple indicating success (bool) and a message (string).</returns>
        public async Task<(bool IsValid, string Message)> CheckSmbCredentialsAsync(SmbCredentials credentials)
        {
            var url = "device/checksmb";
            try
            {
                var response = await _http.PostAsJsonAsync(url, credentials);

                if (response.IsSuccessStatusCode)
                {
                    var successMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"[{_userState.UserName}] ApiServiceClient: SMB credentials check successful: {successMessage}");
                    Debug.WriteLine($"[{_userState.UserName}] ApiServiceClient: SMB credentials check successful: {successMessage}");
                    return (true, successMessage);
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"[{_userState.UserName}] ApiServiceClient: SMB credentials check failed. Status: {response.StatusCode}, Details: {errorMessage}");
                    Debug.WriteLine($"[{_userState.UserName}] ApiServiceClient: SMB credentials check failed. Status: {response.StatusCode}, Details: {errorMessage}");
                    return (false, errorMessage);
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"[{_userState.UserName}] ApiServiceClient: HTTP Request error during POST to {url}: {ex.Message}");
                Debug.WriteLine($"[{_userState.UserName}] ApiServiceClient: HTTP Request error during POST to {url}: {ex.Message}");
                return (false, $"Network error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{_userState.UserName}] ApiServiceClient: Unexpected error during POST to {url}: {ex.Message}");
                Debug.WriteLine($"[{_userState.UserName}] ApiServiceClient: Unexpected error during POST to {url}: {ex.Message}");
                return (false, $"An unexpected error occurred: {ex.Message}");
            }
        }
        
        public async Task<ApiResponse<TResponse>> PostAsync<TRequest, TResponse>(string url, TRequest data)
        {
            try
            {
                var response = await _http.PostAsJsonAsync(url, data);
                response.EnsureSuccessStatusCode();
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<TResponse>>();
                return apiResponse ?? new ApiResponse<TResponse> { IsValid = false, Message = "Empty response from API." };
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"ApiServiceClient: HTTP Request error during POST to {url}: {ex.Message}");
                return new ApiResponse<TResponse> { IsValid = false, Message = $"Network error: {ex.Message}" };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ApiServiceClient: Unexpected error during POST to {url}: {ex.Message}");
                return new ApiResponse<TResponse> { IsValid = false, Message = $"An unexpected error occurred: {ex.Message}" };
            }
        }

        public async Task<SingleResponse<TResponse>> PostSingleAsync<TRequest, TResponse>(string url, TRequest data)
        {
            List<string>? errors = null;
            try
            {
                var response = await _http.PostAsJsonAsync(url, data);
                var apiResponse = await response.Content.ReadFromJsonAsync<SingleResponse<TResponse>>();
                errors = apiResponse?.Errors;
                response.EnsureSuccessStatusCode();
                return apiResponse ?? new SingleResponse<TResponse> { IsValid = false, Errors = errors ?? new List<string> { "Empty response from API." } };
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"ApiServiceClient: HTTP Request error during POST to {url}: {ex.Message}");
                return new SingleResponse<TResponse> { IsValid = false, Errors = errors ?? new List<string> { $"Network error: {ex.Message}" } };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ApiServiceClient: Unexpected error during POST to {url}: {ex.Message}");
                return new SingleResponse<TResponse> { IsValid = false, Errors = new List<string> { $"An unexpected error occurred: {ex.Message}" } };
            }
        }

        /// <summary>
        /// Sends a PUT request to a specified URL with a JSON payload and deserializes the response.
        /// </summary>
        public async Task<SingleResponse<TResponse>> PutSingleAsync<TRequest, TResponse>(string url, TRequest data)
        {
            try
            {
                var response = await _http.PutAsJsonAsync(url, data);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<SingleResponse<TResponse>>(responseContent)
                           ?? new SingleResponse<TResponse> { IsValid = false, Errors = new List<string> { "Failed to deserialize successful response." } };
                }

                try
                {
                    var errorResponse = System.Text.Json.JsonSerializer.Deserialize<SingleResponse<bool>>(responseContent);
                    if (errorResponse != null && (errorResponse.Errors?.Any() ?? false))
                    {
                        return new SingleResponse<TResponse>
                        {
                            Data = default,
                            IsValid = false,
                            Errors = errorResponse.Errors
                        };
                    }
                }
                catch {  }

                return new SingleResponse<TResponse>
                {
                    IsValid = false,
                    Errors = new List<string> { $"API request failed with status {response.StatusCode}.", responseContent }
                };
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"ApiServiceClient: HTTP Request error during PUT to {url}: {ex.Message}");
                return new SingleResponse<TResponse> { IsValid = false, Errors = new List<string> { $"Network error: {ex.Message}" } };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ApiServiceClient: Unexpected error during PUT to {url}: {ex.Message}");
                return new SingleResponse<TResponse> { IsValid = false, Errors = new List<string> { $"An unexpected error occurred: {ex.Message}" } };
            }
        }

        public async Task<(bool, string)> CheckApiAddressAsync(string address)
        {
            var url = "settings/check";
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("address", address)
            });
            try
            {
                var response = await _http.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("\n= = = = = = = = = CheckApiAddress error: " + response.ReasonPhrase + "\n");
                    return (false, "API address is invalid. " + response.ReasonPhrase);
                }
                var wrapper = await response.Content.ReadFromJsonAsync<SimpleResponse>();
                return (wrapper.Success,wrapper.Message);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"ApiServiceClient: HTTP Request error during POST to {url}: {ex.Message}");
                return (false, $"Network error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ApiServiceClient: Unexpected error during POST to {url}: {ex.Message}");
                return (false, $"An unexpected error occurred: {ex.Message}");
            }
        }

        #endregion other functions     
    }
}
