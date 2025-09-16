using BlazorTool.Client.Models;
using System.Net.Http.Json;

namespace BlazorTool.Client.Services
{
    public partial class ApiServiceClient
    {
        private Task<List<Person>>? _personsLoadingTask;
        private readonly object _personsLock = new();

        #region users
        public async Task<List<UserInfo>> GetUsersInfoList()
        {
            if (_usersInfoCache.Any())
            {
                return _usersInfoCache;
            }

            var url = "user/getlist";
            try
            {
                var response = await _http.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("\n= = = = = = = = = UsersInfo response error: " + response.ReasonPhrase + "\n");
                    return new List<UserInfo>();
                }
                var wrapper = await response.Content.ReadFromJsonAsync<ApiResponse<UserInfo>>();
                _usersInfoCache = wrapper?.Data ?? new List<UserInfo>();
                return _usersInfoCache;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"ApiServiceClient: HTTP Request error during GET to {url}: {ex.Message}");
                return new List<UserInfo>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ApiServiceClient: Unexpected error during GET to {url}: {ex.Message}");
                return new List<UserInfo>();
            }
        }

        public List<UserInfo> GetUsersInfoByDepartmentIdCached(int departmentId)
        {
            if (_usersInfoCache.Any())
            {
                return _usersInfoCache.Where(u => u.DepartmentID == departmentId).ToList();
            }
            return new List<UserInfo>();
        }

        public List<UserInfo> GetUsersInfoByDepartmentIdCached(List<int> departmentIds)
        {
            if (_usersInfoCache.Any())
            {
                return _usersInfoCache.Where(u => departmentIds.Contains(u.DepartmentID)).ToList();
            }
            return new List<UserInfo>();
        }

        public async Task<List<Person>> GetAllPersons()
        {
            // Already cached
            if (_personsCache.Count > 0)
                return _personsCache;

            Task<List<Person>> loadingTask;
            lock (_personsLock)
            {
                if (_personsLoadingTask != null)
                    loadingTask = _personsLoadingTask;
                else
                    _personsLoadingTask = loadingTask = LoadPersonsInternalAsync();
            }

            try
            {
                return await loadingTask;
            }
            finally
            {
                // Allow refresh later if cache was cleared
                lock (_personsLock)
                {
                    _personsLoadingTask = null;
                }
            }
        }

        private async Task<List<Person>> LoadPersonsInternalAsync()
        {
            try
            {
                var url = $"other/getuserslist?lang={_userState.LangCode}";
                var response = await _http.GetFromJsonAsync<ApiResponse<Person>>(url);

                if (response?.Data != null && response.IsValid)
                {
                    _personsCache = response.Data;
                    _logger.LogInformation("==> Loaded {Count} persons.", _personsCache.Count);
                }
                else
                {
                    _logger.LogWarning("==> GetAllPersons: invalid or empty response.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllPersons failed.");
            }
            return _personsCache;
        }

        public List<Person> GetAllPersonsCached()
        {
            if (_personsCache.Any())
            {
                return _personsCache;
            }
            return new List<Person>();
        }

        public Person? GetPersonByIDCached(int personID)
        {
            if (_personsCache.Any())
            {
                return _personsCache.FirstOrDefault(p => p.PersonId == personID);
            }
            return null;
        }

        public void InvalidatePersonsCache()
        {
            _personsCache.Clear();
        }
        #endregion

    }
}
