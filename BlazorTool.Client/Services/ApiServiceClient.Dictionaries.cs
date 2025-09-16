using BlazorTool.Client.Models;
using System.Diagnostics;
using System.Net.Http.Json;

namespace BlazorTool.Client.Services
{
    public partial class ApiServiceClient
    {

        #region WO Dictionaries
        public async Task<List<WODict>> GetWODictionaries(int? personID, string? lang = null)
        {
            if (personID == null || personID <= 0)
            {
                Console.WriteLine($"[{_userState.UserName}] = = = = = = Invalid PersonID: {personID}");
                Debug.WriteLine($"[{_userState.UserName}] = = = = = = Invalid PersonID: {personID}");
                return new List<WODict>();
            }
            var qp = new List<string>();
            if (string.IsNullOrWhiteSpace(lang))
            {
                lang = _userState.LangCode;
            }

            qp.Add($"PersonID={personID}");
            qp.Add($"Lang={lang}");
            var url = "wo/getdict?" + string.Join("&", qp);
            try
            {
                var wrapper = await _http.GetFromJsonAsync<ApiResponse<WODict>>(url);
                Console.WriteLine("\n");
                Console.WriteLine($"[{_userState.UserName}] = = = = = = = response Dict.Count: " + wrapper?.Data.Count.ToString());
                Console.WriteLine("\n");
                Debug.WriteLine($"[{_userState.UserName}] = = = = = = = response Dict.Count: " + wrapper?.Data.Count.ToString());
                if (wrapper != null && wrapper.Data != null && wrapper.IsValid)
                {
                    if (wrapper.Errors.Count == 0)
                        // Cache the dictionaries
                        _dictCache = wrapper.Data;
                    else
                    {
                        Console.WriteLine($"[{_userState.UserName}] = = = = = = Errors in GetWODictionaries: " + string.Join(", ", wrapper.Errors));
                        Debug.WriteLine($"[{_userState.UserName}] = = = = = = Errors in GetWODictionaries: " + string.Join(", ", wrapper.Errors));
                    }
                }

                return wrapper?.Data ?? new List<WODict>();
            }
            catch (HttpRequestException ex)
            {
                //await _userState.ClearAsync();
                Console.WriteLine($"ApiServiceClient: HTTP Request error during GET to {url}: {ex.Message}");
                Debug.WriteLine($"ApiServiceClient: HTTP Request error during GET to {url}: {ex.Message}");
                return new List<WODict>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ApiServiceClient: Unexpected error during GET to {url}: {ex.Message}");
                return new List<WODict>();
            }
        }
        public async Task<List<WODict>> GetWODictionariesCached(int? personID = null, string? lang = null)
        {
            if (_dictCache.Count == 0)
            {//is need cache with personID and lang ?
                _dictCache = await GetWODictionaries(personID ?? _userState.PersonID, lang ?? _userState.LangCode);
            }
            return _dictCache;

        }

        public async Task<string> GetStateColor(int? workorderID)
        {
            var stateId = await GetWOStateID(workorderID);
            if (stateId == 0)
                return "primary";

            return GetColorByStateId(stateId);
        }

        public string ConvertStateColor(string state)
        {
            var states = GetWOStatesCached();
            var stateId = states.FirstOrDefault(s => s.Name == state)?.Id;
            return GetColorByStateId(stateId ?? 0);
        }

        public async Task<List<WODict>> GetWOCategories()
        {
            return (await GetWODictionariesCached()).Where(d => d.ListType == (int)WOListTypeEnum.Category)
                .Distinct()
                .ToList();
        }

        public async Task<List<WODict>> GetWOCategoriesByDeviceCategory(int devCategoryId)
        {

            //var dics = GetWODictionariesCached();
            //Console.WriteLine($"=== GetWODictionariesCached = {dics.Count()}");
            //var dicCat = dics.Where(d => d.ListType == (int)WOListTypeEnum.Category);
            //Console.WriteLine($"=== Where(d => d.ListType == (int)WOListTypeEnum.Category = {dicCat.Count()}");
            //var dicFiltered = dicCat.Where(d => d.MachineCategoryID == devCategoryId || d.MachineCategoryID == null);
            //Console.WriteLine($"=== Where(d.MachineCategoryID == devCategoryId || d.MachineCategoryID == null == {dicFiltered.Count()}");
            //Console.WriteLine("===.....devCategoryId = " + devCategoryId);
            //foreach(var item in dicFiltered)
            //{
            //    Console.WriteLine("===............."+item.Name);
            //}
            //Console.WriteLine();
            return (await GetWODictionariesCached()).Where(d => d.ListType == (int)WOListTypeEnum.Category
                                                    && (d.MachineCategoryID == devCategoryId || d.MachineCategoryID == null))
                .Distinct()
                .ToList();
        }

        public async Task<List<WODict>> GetWOStates()
        {
            return (await GetWODictionariesCached()).Where(d => d.ListType == (int)WOListTypeEnum.State)
                .Distinct()
                .ToList();
        }

        public List<WODict> GetWOStatesCached()
        {
            return _dictCache.Where(d => d.ListType == (int)WOListTypeEnum.State)
                .Distinct()
                .ToList();
        }

        public async Task<List<WODict>> GetWOLevels()
        {
            return (await GetWODictionariesCached()).Where(d => d.ListType == (int)WOListTypeEnum.Level)
                .Distinct()
                .ToList();
        }

        public async Task<List<WODict>> GetWOReasons()
        {
            return (await GetWODictionariesCached()).Where(d => d.ListType == (int)WOListTypeEnum.Reason)
                .Distinct()
                .ToList();
        }

        public async Task<List<WODict>> GetWOReasonsByDeviceCategory(int devCategoryId)
        {
            return (await GetWODictionariesCached()).Where(d => d.ListType == (int)WOListTypeEnum.Reason
            && (d.MachineCategoryID == devCategoryId || d.MachineCategoryID == null))
                .Distinct()
                .ToList();
        }

        public async Task<List<WODict>> GetWODepartments()
        {
            return (await GetWODictionariesCached()).Where(d => d.ListType == (int)WOListTypeEnum.Department)
                .Distinct()
                .ToList();
        }

        #endregion

        #region Activity Dictionaries
        public async Task<List<WODict>> GetActDictionaries(int? personID, string? lang = null)
        {
            if (personID == null || personID <= 0)
            {
                Console.WriteLine($"[{_userState.UserName}] = = = = = = Invalid PersonID: {personID}");
                Debug.WriteLine($"[{_userState.UserName}] = = = = = = Invalid PersonID: {personID}");
                return new List<WODict>();
            }
            var qp = new List<string>();
            if (string.IsNullOrWhiteSpace(lang))
            {
                lang = _userState.LangCode;
            }

            qp.Add($"PersonID={personID}");
            qp.Add($"Lang={lang}");
            var url = "activity/getdict?" + string.Join("&", qp);
            try
            {
                var wrapper = await _http.GetFromJsonAsync<ApiResponse<WODict>>(url);
                Console.WriteLine($"[{_userState.UserName}] = = = = = = = response Activity Dict.Count: " + wrapper?.Data.Count.ToString());
                Debug.WriteLine($"[{_userState.UserName}] = = = = = = = response Activity Dict.Count: " + wrapper?.Data.Count.ToString());
                if (wrapper != null && wrapper.Data != null && wrapper.IsValid)
                {
                    if (wrapper.Errors.Count == 0)
                        _actDictCache = wrapper.Data;
                    else
                    {
                        Console.WriteLine($"[{_userState.UserName}] = = = = = = Errors in GetActDictionaries: " + string.Join(", ", wrapper.Errors));
                        Debug.WriteLine($"[{_userState.UserName}] = = = = = = Errors in GetActDictionaries: " + string.Join(", ", wrapper.Errors));
                    }
                }

                return wrapper?.Data ?? new List<WODict>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"ApiServiceClient: HTTP Request error during GET to {url}: {ex.Message}");
                Debug.WriteLine($"ApiServiceClient: HTTP Request error during GET to {url}: {ex.Message}");
                return new List<WODict>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ApiServiceClient: Unexpected error during GET to {url}: {ex.Message}");
                return new List<WODict>();
            }
        }

        public List<WODict> GetActDictionariesCached()
        {
            return _actDictCache;
        }
        public List<WODict> GetActCategoriesCached()
        {
            if (_actDictCache == null || _actDictCache.Count == 0)
            {
                return new List<WODict>();
            }
            return (GetActDictionariesCached()).Where(d => d.ListType == (int)WOListTypeEnum.Category)
                .Distinct()
                .ToList();
        }

        public int GetActCategoryIDCached(string? name)
        {
            if (_actDictCache == null || _actDictCache.Count == 0)
            {
                return 0;
            }
            return (GetActDictionariesCached()).FirstOrDefault(d => d.ListType == (int)WOListTypeEnum.Category && d.Name == name)?.Id ?? 0;
        }
        public async Task<List<WODict>> GetActCategories()
        {
            if (_actDictCache == null || _actDictCache.Count == 0)
            {
                _actDictCache = await GetActDictionaries(_userState.PersonID, _userState.LangCode);
            }
            return (GetActDictionariesCached()).Where(d => d.ListType == (int)WOListTypeEnum.Category)
                .Distinct()
                .ToList();
        }

        public async Task<List<WODict>> GetActCategoriesByDeviceCategory(int? deviceCategoryId)
        {
            if (_actDictCache == null || _actDictCache.Count == 0)
            {
                _actDictCache = await GetActDictionaries(_userState.PersonID, _userState.LangCode);
            }
            return (GetActDictionariesCached()).Where(d => d.ListType == (int)WOListTypeEnum.Category &&
            (d.MachineCategoryID == deviceCategoryId) || d.MachineCategoryID == null)
                .Distinct()
                .ToList();
        }

        #endregion
    }
}
