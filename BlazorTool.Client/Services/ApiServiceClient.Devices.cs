using BlazorTool.Client.Models;
using System.Diagnostics;
using System.Net.Http.Json;

namespace BlazorTool.Client.Services
{
    public partial class ApiServiceClient
    {
        #region Devices
        public async Task<List<Device>> GetAllDevicesAsync()
        {
            if (!_devicesCache.Any())
            {
                _devicesCache = await GetDevicesAsync();
            }

            return _devicesCache;
        }

        private async Task<List<Device>> GetDevicesAsync(
                                        string? lang = null,
                                        string? name = null,
                                        int? categoryID = null,
                                        bool? isSet = null,
                                        IEnumerable<int>? machineIDs = null)
        {
            var qp = new List<string>();
            if (string.IsNullOrWhiteSpace(lang))
            {
                lang = _userState.LangCode;
            }
            if (!string.IsNullOrWhiteSpace(lang)) qp.Add($"Lang={Uri.EscapeDataString(lang)}");
            if (!string.IsNullOrWhiteSpace(name)) qp.Add($"Name={Uri.EscapeDataString(name)}");
            if (categoryID.HasValue) qp.Add($"CategoryID={categoryID.Value}");
            if (isSet.HasValue) qp.Add($"IsSet={isSet.Value}");
            if (machineIDs != null)
                foreach (var id in machineIDs)
                    qp.Add($"MachineIDs={id}");
            Console.WriteLine($" ====    name:{name} MachineIDs.count={machineIDs?.Count()}");
            var url = "device/getlist" + (qp.Count > 0 ? "?" + string.Join("&", qp) : "");
            try
            {
                var response = await _http.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"\n[{_userState.UserName}] = = = = = = Devices response error: " + response.ReasonPhrase + "\n");
                    Debug.WriteLine($"\n[{_userState.UserName}] = = = = = Devices response error: " + response.ReasonPhrase + "\n");
                    return new List<Device>();
                }
                var wrapper = await response.Content.ReadFromJsonAsync<ApiResponse<Device>>();

                Console.WriteLine($"\n[{_userState.UserName}] = = = = = = response Devices: " + wrapper?.Data.Count.ToString() + "\n");
                Debug.WriteLine($"\n[{_userState.UserName}] = = = = = response Devices: " + wrapper?.Data.Count.ToString() + "\n");
                return wrapper?.Data ?? new List<Device>();
            }
            catch (HttpRequestException ex)
            {
                //await _userState.ClearAsync();
                Console.WriteLine($"ApiServiceClient: HTTP Request error during GET to {url}: {ex.Message}");
                Debug.WriteLine($"ApiServiceClient: HTTP Request error during GET to {url}: {ex.Message}");
                return new List<Device>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ApiServiceClient: Unexpected error during GET to {url}: {ex.Message}");
                return new List<Device>();
            }
        }

        public string GetDeviceNameByOrder(WorkOrder order)
        {
            return _devicesCache.FirstOrDefault(d => d.MachineID == order.MachineID)?.AssetNo ?? string.Empty;
        }

        public async Task<ApiResponse<DeviceDetailProperty>> GetDeviceDetailAsync(int deviceID, string? lang = null)
        {
            var qp = new List<string>();
            if (string.IsNullOrWhiteSpace(lang))
            {
                lang = _userState.LangCode;
            }
            qp.Add($"DeviceID={deviceID}");
            qp.Add($"Lang={Uri.EscapeDataString(lang)}");

            var url = "device/getdetail?" + string.Join("&", qp);

            try
            {
                var wrapper = await _http.GetFromJsonAsync<ApiResponse<DeviceDetailProperty>>(url);
                Console.WriteLine($"[{_userState.UserName}] = = = = = = = API response-> DeviceDetailProperty.Count: " + wrapper?.Data.Count.ToString());
                return wrapper ?? new ApiResponse<DeviceDetailProperty> { IsValid = false, Errors = new List<string> { "Empty response from API." } };
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"[{_userState.UserName}] ApiServiceClient: HTTP Request error during GET to {url}: {ex.Message}");
                Debug.WriteLine($"[{_userState.UserName}] ApiServiceClient: HTTP Request error during GET to {url}: {ex.Message}");
                return new ApiResponse<DeviceDetailProperty> { IsValid = false, Errors = new List<string> { $"Network error: {ex.Message}" } };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{_userState.UserName}] ApiServiceClient: Unexpected error during GET to {url}: {ex.Message}");
                Debug.WriteLine($"[{_userState.UserName}] ApiServiceClient: Unexpected error during GET to {url}: {ex.Message}");
                return new ApiResponse<DeviceDetailProperty> { IsValid = false, Errors = new List<string> { $"An unexpected error occurred: {ex.Message}" } };
            }
        }

        public async Task<ApiResponse<DeviceState>> GetDeviceStateAsync(int machineID, string? lang = null)
        {
            var qp = new List<string>();
            if (string.IsNullOrWhiteSpace(lang))
            {
                lang = _userState.LangCode;
            }
            qp.Add($"MachineID={machineID}");
            qp.Add($"Lang={Uri.EscapeDataString(lang)}");

            var url = "device/getstate?" + string.Join("&", qp);

            try
            {
                var wrapper = await _http.GetFromJsonAsync<ApiResponse<DeviceState>>(url);
                Console.WriteLine($"[{_userState.UserName}] = = = = = = = API response-> DeviceState.Count: " + wrapper?.Data.Count.ToString());
                return wrapper ?? new ApiResponse<DeviceState> { IsValid = false, Errors = new List<string> { "Empty response from API." } };
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"[{_userState.UserName}] ApiServiceClient: HTTP Request error during GET to {url}: {ex.Message}");
                Debug.WriteLine($"[{_userState.UserName}] ApiServiceClient: HTTP Request error during GET to {url}: {ex.Message}");
                return new ApiResponse<DeviceState> { IsValid = false, Errors = new List<string> { $"Network error: {ex.Message}" } };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{_userState.UserName}] ApiServiceClient: Unexpected error during GET to {url}: {ex.Message}");
                Debug.WriteLine($"[{_userState.UserName}] ApiServiceClient: Unexpected error during GET to {url}: {ex.Message}");
                return new ApiResponse<DeviceState> { IsValid = false, Errors = new List<string> { $"An unexpected error occurred: {ex.Message}" } };
            }
        }

        public async Task<ApiResponse<DeviceStatus>> GetDeviceStatusAsync(string? lang = null)
        {
            if (string.IsNullOrWhiteSpace(lang))
            {
                lang = _userState.LangCode;
            }

            if (_deviceStatusCache.TryGetValue(lang, out var cachedResponse))
            {
                Console.WriteLine($"[{_userState.UserName}] Device status for lang {lang} found in cache.");
                Debug.WriteLine($"[{_userState.UserName}] Device status for lang {lang} found in cache.");
                return cachedResponse;
            }

            var url = $"device/getstatus?Lang={Uri.EscapeDataString(lang)}";

            try
            {
                var wrapper = await _http.GetFromJsonAsync<ApiResponse<DeviceStatus>>(url);
                Console.WriteLine($"[{_userState.UserName}] = = = = = = = API response-> DeviceStatus.Count: " + wrapper?.Data.Count.ToString());
                if (wrapper != null && wrapper.IsValid)
                {
                    _deviceStatusCache[lang] = wrapper;
                }
                return wrapper ?? new ApiResponse<DeviceStatus> { IsValid = false, Errors = new List<string> { "Empty response from API." } };
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"[{_userState.UserName}] ApiServiceClient: HTTP Request error during GET to {url}: {ex.Message}");
                Debug.WriteLine($"[{_userState.UserName}] ApiServiceClient: HTTP Request error during GET to {url}: {ex.Message}");
                return new ApiResponse<DeviceStatus> { IsValid = false, Errors = new List<string> { $"Network error: {ex.Message}" } };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{_userState.UserName}] ApiServiceClient: Unexpected error during GET to {url}: {ex.Message}");
                Debug.WriteLine($"[{_userState.UserName}] ApiServiceClient: Unexpected error during GET to {url}: {ex.Message}");
                return new ApiResponse<DeviceStatus> { IsValid = false, Errors = new List<string> { $"An unexpected error occurred: {ex.Message}" } };
            }
        }

        public async Task<SingleResponse<Device>> GetSingleDeviceAsync(int machineID, string? lang = null)
        {
            var qp = new List<string>();
            if (string.IsNullOrWhiteSpace(lang))
            {
                lang = _userState.LangCode;
            }
            qp.Add($"MachineID={machineID}");
            qp.Add($"Lang={Uri.EscapeDataString(lang)}");

            var url = "device/get?" + string.Join("&", qp);

            try
            {
                var wrapper = await _http.GetFromJsonAsync<SingleResponse<Device>>(url);
                Console.WriteLine($"[{_userState.UserName}] = = = = = = = API response-> Single Device: " + wrapper?.Data?.AssetNo);
                return wrapper ?? new SingleResponse<Device> { IsValid = false, Errors = new List<string> { "Empty response from API." } };
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"[{_userState.UserName}] ApiServiceClient: HTTP Request error during GET to {url}: {ex.Message}");
                Debug.WriteLine($"[{_userState.UserName}] ApiServiceClient: HTTP Request error during GET to {url}: {ex.Message}");
                return new SingleResponse<Device> { IsValid = false, Errors = new List<string> { $"Network error: {ex.Message}" } };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{_userState.UserName}] ApiServiceClient: Unexpected error during GET to {url}: {ex.Message}");
                Debug.WriteLine($"[{_userState.UserName}] ApiServiceClient: Unexpected error during GET to {url}: {ex.Message}");
                return new SingleResponse<Device> { IsValid = false, Errors = new List<string> { $"An unexpected error occurred: {ex.Message}" } };
            }
        }

        public async Task<SingleResponse<DeviceImage>> GetDeviceImageAsync(int deviceID, int? width = null, int? height = null)
        {
            var cacheKey = $"{deviceID}-{width}-{height}";

            if (_deviceImageCache.TryGetValue(cacheKey, out var cachedResponse))
            {
                Console.WriteLine($"[{_userState.UserName}] Device image for {cacheKey} found in cache.");
                Debug.WriteLine($"[{_userState.UserName}] Device image for {cacheKey} found in cache.");
                return cachedResponse;
            }

            var qp = new List<string>();
            qp.Add($"DeviceID={deviceID}");
            if (width.HasValue) qp.Add($"Width={width.Value}");
            if (height.HasValue) qp.Add($"Height={height.Value}");

            var url = "device/GetImage?" + string.Join("&", qp);

            try
            {
                var wrapper = await _http.GetFromJsonAsync<SingleResponse<DeviceImage>>(url);
                Console.WriteLine($"[{_userState.UserName}] = = = = = = = API response-> DeviceImage for {deviceID}");
                if (wrapper != null && wrapper.IsValid)
                {
                    _deviceImageCache[cacheKey] = wrapper;
                }
                return wrapper ?? new SingleResponse<DeviceImage> { IsValid = false, Errors = new List<string> { "Empty response from API." } };
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"[{_userState.UserName}] ApiServiceClient: HTTP Request error during GET to {url}: {ex.Message}");
                Debug.WriteLine($"[{_userState.UserName}] ApiServiceClient: HTTP Request error during GET to {url}: {ex.Message}");
                return new SingleResponse<DeviceImage> { IsValid = false, Errors = new List<string> { $"Network error: {ex.Message}" } };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{_userState.UserName}] ApiServiceClient: Unexpected error during GET to {url}: {ex.Message}");
                Debug.WriteLine($"[{_userState.UserName}] ApiServiceClient: Unexpected error during GET to {url}: {ex.Message}");
                return new SingleResponse<DeviceImage> { IsValid = false, Errors = new List<string> { $"An unexpected error occurred: {ex.Message}" } };
            }
        }

        public async Task<ApiResponse<DeviceDict>> GetDeviceDictionariesAsync(int personID, string? lang = null)
        {
            if (personID <= 0)
            {
                Console.WriteLine($"[{_userState.UserName}] = = = = = = Invalid PersonID: {personID}");
                Debug.WriteLine($"[{_userState.UserName}] = = = = = = Invalid PersonID: {personID}");
                return new ApiResponse<DeviceDict> { IsValid = false, Errors = new List<string> { "Invalid PersonID." } };
            }

            if (string.IsNullOrWhiteSpace(lang))
            {
                lang = _userState.LangCode;
            }

            var cacheKey = $"{personID}-{lang}";

            if (_deviceDictCache.TryGetValue(cacheKey, out var cachedResponse))
            {
                Console.WriteLine($"[{_userState.UserName}] Device dictionaries for {cacheKey} found in cache.");
                Debug.WriteLine($"[{_userState.UserName}] Device dictionaries for {cacheKey} found in cache.");
                return cachedResponse;
            }

            var qp = new List<string>();
            qp.Add($"PersonID={personID}");
            qp.Add($"Lang={Uri.EscapeDataString(lang)}");

            var url = "device/getdict?" + string.Join("&", qp);

            try
            {
                var wrapper = await _http.GetFromJsonAsync<ApiResponse<DeviceDict>>(url);
                Console.WriteLine($"[{_userState.UserName}] = = = = = = = API response-> DeviceDict.Count: " + wrapper?.Data.Count.ToString());
                if (wrapper != null && wrapper.IsValid)
                {
                    _deviceDictCache[cacheKey] = wrapper;
                }
                return wrapper ?? new ApiResponse<DeviceDict> { IsValid = false, Errors = new List<string> { "Empty response from API." } };
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"[{_userState.UserName}] ApiServiceClient: HTTP Request error during GET to {url}: {ex.Message}");
                Debug.WriteLine($"[{_userState.UserName}] ApiServiceClient: HTTP Request error during GET to {url}: {ex.Message}");
                return new ApiResponse<DeviceDict> { IsValid = false, Errors = new List<string> { $"Network error: {ex.Message}" } };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{_userState.UserName}] ApiServiceClient: Unexpected error during GET to {url}: {ex.Message}");
                Debug.WriteLine($"[{_userState.UserName}] ApiServiceClient: Unexpected error during GET to {url}: {ex.Message}");
                return new ApiResponse<DeviceDict> { IsValid = false, Errors = new List<string> { $"An unexpected error occurred: {ex.Message}" } };
            }
        }

        /// <summary>
        /// Full device info except List Details
        /// </summary>
        /// <param name="device"></param>
        /// <param name="lang"></param>
        /// <param name="skipImageLoad"></param>
        /// <returns></returns>
        public async Task<FullDeviceInfo> GetDeviceInfoForGridAsync(Device device, string? lang = null, bool skipImageLoad = true)
        {
            if (string.IsNullOrWhiteSpace(lang))
            {
                lang = _userState.LangCode;
            }

            var fullDeviceInfo = new FullDeviceInfo
            {
                MachineID = device.MachineID,
                AssetNo = device.AssetNo,
                AssetNoShort = device.AssetNoShort,
                DeviceCategory = device.DeviceCategory,
                Type = device.Type,
                SerialNo = device.SerialNo,
                StateID = device.StateID,
                CategoryID = device.CategoryID,
                DocumentationPath = device.DocumentationPath,
                Location = device.Location,
                LocationRequired = device.LocationRequired,
                LocationName = device.LocationName,
                Place = device.Place,
                IsCritical = device.IsCritical,
                SetName = device.SetName,
                SetID = device.SetID,
                Active = device.Active,
                Cycle = device.Cycle,
                Owner = device.Owner
            };

            // Get state
            var stateResponse = await GetDeviceStateAsync(device.MachineID, lang);
            if (stateResponse.IsValid && stateResponse.Data != null && stateResponse.Data.Any())
            {
                fullDeviceInfo.StateHistory = stateResponse.Data;
            }

            // Get status
            var statusResponse = await GetDeviceStatusAsync(lang);
            if (statusResponse.IsValid && statusResponse.Data != null)
            {
                fullDeviceInfo.Statuses = statusResponse.Data.Where(s => s.DeviceID == device.MachineID).ToList();
            }

            // Get image
            if (!skipImageLoad)
            {
                var imageResponse = await GetDeviceImageAsync(device.MachineID);
                if (imageResponse.IsValid && imageResponse.Data != null)
                {
                    fullDeviceInfo.Images.Add(imageResponse.Data);
                }
            }

            // Get directory files if DocumentationPath is available
            if (!string.IsNullOrWhiteSpace(fullDeviceInfo.DocumentationPath))
            {
                var directoryFilesResponse = await GetWorkOrderDirectoryFiles(fullDeviceInfo.DocumentationPath);
                if (directoryFilesResponse.IsValid && directoryFilesResponse.Data != null)
                {
                    fullDeviceInfo.DirectoryFiles = directoryFilesResponse.Data;
                }
            }
            return fullDeviceInfo;
        }
        public async Task<List<FullDeviceInfo>> GetListDeviceInfoForGrid(IEnumerable<Device> devices, string? lang = null, bool skipImageLoad = true)
        {
            var listDeviceInfo = new List<FullDeviceInfo>();
            foreach (var device in devices)
            {
                listDeviceInfo.Add(await GetDeviceInfoForGridAsync(device, lang, skipImageLoad));
            }

            return listDeviceInfo;
        }
        public async Task<FullDeviceInfo?> GetFullDeviceInfoAsync(int deviceID, string? lang = null, bool skipImageLoad = true)
        {
            if (string.IsNullOrWhiteSpace(lang))
            {
                lang = _userState.LangCode;
            }

            var deviceResponse = await GetSingleDeviceAsync(deviceID, lang);
            if (!deviceResponse.IsValid || deviceResponse.Data == null)
            {
                Console.WriteLine($"[{_userState.UserName}] Failed to get basic device info for DeviceID: {deviceID}");
                Debug.WriteLine($"[{_userState.UserName}] Failed to get basic device info for DeviceID: {deviceID}");
                return null;
            }

            var fullDeviceInfo = await GetDeviceInfoForGridAsync(deviceResponse.Data, lang, skipImageLoad);

            // Get details
            var detailsResponse = await GetDeviceDetailAsync(deviceID, lang);
            if (detailsResponse.IsValid && detailsResponse.Data != null)
            {
                fullDeviceInfo.Details = detailsResponse.Data;
            }

            return fullDeviceInfo;
        }


        public async Task<ApiResponse<WorkOrderFileItem>> GetWorkOrderDirectoryFiles(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                Console.WriteLine($"[{_userState.UserName}] ApiServiceClient.GetWorkOrderDirectoryFiles: DirectoryPath cannot be empty.");
                Debug.WriteLine($"[{_userState.UserName}] ApiServiceClient.GetWorkOrderDirectoryFiles: DirectoryPath cannot be empty.");
                return new ApiResponse<WorkOrderFileItem> { IsValid = false, Errors = new List<string> { "DirectoryPath cannot be empty." } };
            }

            var url = $"device/getdirfiles?DirectoryPath={Uri.EscapeDataString(directoryPath)}";

            try
            {
                var response = await _http.GetFromJsonAsync<ApiResponse<WorkOrderFileItem>>(url);
                if (response == null)
                {
                    Console.WriteLine($"[{_userState.UserName}] ApiServiceClient.GetWorkOrderDirectoryFiles: Empty response from API for {url}");
                    Debug.WriteLine($"[{_userState.UserName}] ApiServiceClient.GetWorkOrderDirectoryFiles: Empty response from API for {url}");
                    return new ApiResponse<WorkOrderFileItem> { IsValid = false, Errors = new List<string> { "Empty response from API." } };
                }
                return response;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"[{_userState.UserName}] ApiServiceClient: HTTP Request error during GET to {url}: {ex.Message}");
                Debug.WriteLine($"[{_userState.UserName}] ApiServiceClient: HTTP Request error during GET to {url}: {ex.Message}");
                return new ApiResponse<WorkOrderFileItem> { IsValid = false, Errors = new List<string> { $"Network error: {ex.Message}" } };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{_userState.UserName}] ApiServiceClient: Unexpected error during GET to {url}: {ex.Message}");
                Debug.WriteLine($"[{_userState.UserName}] ApiServiceClient: Unexpected error during GET to {url}: {ex.Message}");
                return new ApiResponse<WorkOrderFileItem> { IsValid = false, Errors = new List<string> { $"An unexpected error occurred: {ex.Message}" } };
            }
        }

        public async Task<SingleResponse<WorkOrderFileData>> GetWorkOrderFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                Console.WriteLine($"[{_userState.UserName}] ApiServiceClient.GetWorkOrderFile: FileName cannot be empty.");
                Debug.WriteLine($"[{_userState.UserName}] ApiServiceClient.GetWorkOrderFile: FileName cannot be empty.");
                return new SingleResponse<WorkOrderFileData> { IsValid = false, Errors = new List<string> { "FileName cannot be empty." } };
            }

            // Normalize the file path: replace double backslashes with single ones
            string normalizedFileName = fileName.Replace("\\\\", "\\");

            // Use the normalized file name as the cache key
            var cacheKey = normalizedFileName;

            // Check cache first
            if (_workOrderFileCache.TryGetValue(cacheKey, out var cachedResponse))
            {
                Console.WriteLine($"[{_userState.UserName}] Work order file for {cacheKey} found in cache.");
                Debug.WriteLine($"[{_userState.UserName}] Work order file for {cacheKey} found in cache.");
                return cachedResponse;
            }

            var url = $"device/getfile?FileName={Uri.EscapeDataString(normalizedFileName)}";

            try
            {
                var response = await _http.GetFromJsonAsync<SingleResponse<WorkOrderFileData>>(url);
                if (response == null)
                {
                    Console.WriteLine($"[{_userState.UserName}] ApiServiceClient.GetWorkOrderFile: Empty response from API for {url}");
                    Debug.WriteLine($"[{_userState.UserName}] ApiServiceClient.GetWorkOrderFile: Empty response from API for {url}");
                    return new SingleResponse<WorkOrderFileData> { IsValid = false, Errors = new List<string> { "Empty response from API." } };
                }
                // Cache the result
                _workOrderFileCache[cacheKey] = response;
                return response;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"[{_userState.UserName}] ApiServiceClient: HTTP Request error during GET to {url}: {ex.Message}");
                Debug.WriteLine($"[{_userState.UserName}] ApiServiceClient: HTTP Request error during GET to {url}: {ex.Message}");
                return new SingleResponse<WorkOrderFileData> { IsValid = false, Errors = new List<string> { $"Network error: {ex.Message}" } };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{_userState.UserName}] ApiServiceClient: Unexpected error during GET to {url}: {ex.Message}");
                Debug.WriteLine($"[{_userState.UserName}] ApiServiceClient: Unexpected error during GET to {url}: {ex.Message}");
                return new SingleResponse<WorkOrderFileData> { IsValid = false, Errors = new List<string> { $"An unexpected error occurred: {ex.Message}" } };
            }
        }

        /// <summary>
        /// Constructs a URL for downloading a file, including SMB credentials in the query string.
        /// WARNING: Passing credentials in the URL is not recommended for sensitive information.
        /// Consider using a POST request with credentials in the body for better security.
        /// </summary>
        /// <param name="filePath">The path to the file to download.</param>
        /// <param name="credentials">The SMB credentials for accessing the file.</param>
        /// <returns>A URL for downloading the file.</returns>
        public string GetFileDownloadUrl(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine($"[{_userState.UserName}] DeviceFileService.GetFileDownloadUrl: FilePath cannot be empty.");
                Debug.WriteLine($"[{_userState.UserName}] DeviceFileService.GetFileDownloadUrl: FilePath cannot be empty.");
                return string.Empty;
            }

            // Normalize the file path: replace double backslashes with single ones
            string normalizedFilePath = filePath.Replace("\\", "\\");

            // Encode the file path to be safe for URL
            string encodedFilePath = Uri.EscapeDataString(normalizedFilePath);

            // Construct the full URL for the download endpoint
            // Include SMB credentials in the query string
            var queryParams = new List<string>
            {
                $"filePath={encodedFilePath}"
            };

            if (!string.IsNullOrWhiteSpace(_userState.NetworkShareUsername))
                queryParams.Add($"smbUsername={Uri.EscapeDataString(_userState.NetworkShareUsername)}");
            if (!string.IsNullOrWhiteSpace(_userState.NetworkSharePassword))
                queryParams.Add($"smbPassword={Uri.EscapeDataString(_userState.NetworkSharePassword)}");

            return $"{_http.BaseAddress}device/downloadfile?{string.Join("&", queryParams)}";
        }
        #endregion
    }
}
