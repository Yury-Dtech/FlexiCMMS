using BlazorTool.Client.Models;
using System.Net.Http.Json;

namespace BlazorTool.Client.Services
{
    public partial class ApiServiceClient
    {

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
            if (_personsCache.Any())
            {
                return _personsCache;
            }

            var url = "other/getuserslist";
            try
            {
                Console.WriteLine("====== Start GetAllPersons() request: " + _http.BaseAddress + url);
                var response = await _http.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("\n= = = = = = = = = Users response error: " + response.ReasonPhrase + "\n");
                    return new List<Person>();
                }
                var wrapper = await response.Content.ReadFromJsonAsync<ApiResponse<Person>>();
                Console.WriteLine($"\n= = = = = = = = = response {_http.BaseAddress}{url} \n====== Users: " + wrapper?.Data.Count.ToString() + "\n");

                _personsCache = wrapper?.Data ?? new List<Person>();
                return _personsCache;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"ApiServiceClient: HTTP Request error during GET to {url}: {ex.Message}");
                return new List<Person>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ApiServiceClient: Unexpected error during GET to {url}: {ex.Message}");
                return new List<Person>();
            }
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
        #endregion

    }
}
