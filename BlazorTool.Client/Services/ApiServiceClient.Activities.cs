using BlazorTool.Client.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Activity = BlazorTool.Client.Models.Activity;

namespace BlazorTool.Client.Services
{
    public partial class ApiServiceClient
    {
        // Cache for activities per work order
        private readonly Dictionary<int, List<Activity>> _activitiesCache = new Dictionary<int, List<Activity>>();
        private readonly object _activitiesCacheLock = new object();
        #region Activity
        /// <summary>
        /// Retrieves activities for a work order.
        /// </summary>
        public async Task<List<Activity>> GetActivitiesByWO(int workorder_id)
        {
            var url = $"activity/getlist?woID={workorder_id}&lang={_userState.LangCode}";
            try
            {
                var response = await _http.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var wrapper = JsonConvert.DeserializeObject<ApiResponse<Activity>>(content);
                var result = wrapper?.Data ?? new List<Activity>();

                lock (_activitiesCacheLock)
                {
                    // store a shallow copy to avoid accidental external mutation
                    _activitiesCache[workorder_id] = new List<Activity>(result);
                }

                return result;
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

        /// <summary>
        /// Returns cached activities for a work order if present, otherwise new request.
        /// </summary>
        public async Task<List<Activity>> GetCachedActivitiesByWO(int workorder_id)
        {
            lock (_activitiesCacheLock)
            {
                if (_activitiesCache.TryGetValue(workorder_id, out var cached))
                {
                    return cached;
                }
            }
            return await GetActivitiesByWO(workorder_id);
        }

        /// <summary>
        /// Retrieves activities for a list of work orders. Uses cache when available and fetches missing ones in parallel.
        /// Returns a mapping from work order id to its activities (empty list if none or request failed).
        /// </summary>
        public async Task<List<Activity>> GetActivitiesByWO(List<WorkOrder> orders)
        {
            if (orders == null) throw new ArgumentNullException(nameof(orders));

            var tasks = new List<Task<List<Activity>>>();
            var ids = new List<int>();

            foreach (var order in orders)
            {
                ids.Add(order.WorkOrderID);
                tasks.Add(GetActivitiesByWO(order.WorkOrderID));
            }

            var results = await Task.WhenAll(tasks);

            var acts = new List<Activity>();
            for (int i = 0; i < ids.Count; i++)
            {
                acts.AddRange(results[i] ?? new List<Activity>());
            }

            return acts;
        }

        public async Task<List<Activity>> GetCachedActivitiesByWO(List<WorkOrder> orders)
        {
            if (orders == null) throw new ArgumentNullException(nameof(orders));

            var tasks = new List<Task<List<Activity>>>();
            var ids = new List<int>();

            foreach (var order in orders)
            {
                ids.Add(order.WorkOrderID);
                tasks.Add(GetCachedActivitiesByWO(order.WorkOrderID));
            }

            var results = await Task.WhenAll(tasks);

            var map = new List<Activity>();
            for (int i = 0; i < ids.Count; i++)
            {
                map.AddRange(results[i] ?? new List<Activity>());
            }

            return map;
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
