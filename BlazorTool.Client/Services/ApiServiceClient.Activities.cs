using BlazorTool.Client.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Activity = BlazorTool.Client.Models.Activity;

namespace BlazorTool.Client.Services
{
    public partial class ApiServiceClient
    {
        #region Activity
        public async Task<List<Models.Activity>> GetActivitiesByWO(int workorder_id)
        {
            var url = $"activity/getlist?woID={workorder_id}&lang={_userState.LangCode}";
            try
            {
                var response = await _http.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var wrapper = JsonConvert.DeserializeObject<ApiResponse<Activity>>(content);
                return wrapper?.Data ?? new List<Activity>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"ApiServiceClient: HTTP Request error during GET to {url}: {ex.Message}");
                return new List<Activity>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ApiServiceClient: Unexpected error during GET to {url}: {ex.Message}");
                return new List<Activity>();
            }
        }

        public async Task<SingleResponse<NewActivityResponse>> CreateActivityAsync(AddActivity activity)
        {
            var url = "activity/create";
            try
            {
                var response = await _http.PostAsJsonAsync(url, activity);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var activityResponse = JsonConvert.DeserializeObject<NewActivityResponse>(content);
                    return new SingleResponse<NewActivityResponse> { IsValid = true, Data = activityResponse };
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"ApiServiceClient: Failed to create activity. Status: {response.StatusCode}, Details: {errorContent}");
                    return new SingleResponse<NewActivityResponse> { IsValid = false, Errors = new List<string> { $"Server error: {errorContent}" } };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ApiServiceClient: Unexpected error during POST to {url}: {ex.Message}");
                return new SingleResponse<NewActivityResponse> { IsValid = false, Errors = new List<string> { $"An unexpected error occurred: {ex.Message}" } };
            }
        }
        public async Task<SingleResponse<AddToActivityResponse>> JoinToActivityAsync(JoinToActivity activity)
        {
            var url = "activity/add";
            try
            {
                var response = await _http.PostAsJsonAsync(url, activity);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var activityResponse = JsonConvert.DeserializeObject<AddToActivityResponse>(content);
                    return new SingleResponse<AddToActivityResponse> { IsValid = true, Data = activityResponse };
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"ApiServiceClient: Failed to create activity. Status: {response.StatusCode}, Details: {errorContent}");
                    return new SingleResponse<AddToActivityResponse> { IsValid = false, Errors = new List<string> { $"Server error: {errorContent}" } };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ApiServiceClient: Unexpected error during POST to {url}: {ex.Message}");
                return new SingleResponse<AddToActivityResponse> { IsValid = false, Errors = new List<string> { $"An unexpected error occurred: {ex.Message}" } };
            }
        }
        #endregion

    }
}
