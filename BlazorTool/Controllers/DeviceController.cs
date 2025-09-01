using BlazorTool.Client.Models;
using Microsoft.AspNetCore.Mvc;
using SMBLibrary;
using SMBLibrary.Client;
using SMBLibrary.Services;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using Telerik.SvgIcons;

namespace BlazorTool.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DeviceController(IHttpClientFactory httpClientFactory) 
        {
            _httpClientFactory = httpClientFactory; 
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList(
                                        [FromQuery] string lang = "pl-pl",
                                        [FromQuery] string? name = null,
                                        [FromQuery] int? categoryID = null,
                                        [FromQuery] bool? isSet = null,
                                        [FromQuery] IEnumerable<int>? machineIDs = null)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ExternalApiBearerAuthClient"); 

                var qp = new List<string>();
                if (!string.IsNullOrWhiteSpace(lang)) qp.Add($"Lang={Uri.EscapeDataString(lang)}");
                if (!string.IsNullOrWhiteSpace(name)) qp.Add($"Name={Uri.EscapeDataString(name)}");
                if (categoryID.HasValue) qp.Add($"CategoryID={categoryID.Value}");
                if (isSet.HasValue) qp.Add($"IsSet={isSet.Value}");
                if (machineIDs != null)
                    foreach (var id in machineIDs)
                        qp.Add($"MachineIDs={id}");

                var url = "device/getlist" + (qp.Count > 0 ? "?" + string.Join("&", qp) : "");
                
                var response = await client.GetAsync(url); 
                response.EnsureSuccessStatusCode();

                var wrapper = await response.Content.ReadFromJsonAsync<ApiResponse<Device>>();
                return Ok(new
                {
                    data = wrapper?.Data ?? new List<Device>(),
                    isValid = true,
                    errors = Array.Empty<string>()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    data = Array.Empty<Device>(),
                    isValid = false,
                    errors = new[] { ex.Message }
                });
            }
        }
        

        [HttpGet("getdetail")]
        public async Task<IActionResult> GetDetail(
                                        [FromQuery] int deviceID,
                                        [FromQuery] string lang = "pl-pl")
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ExternalApiBearerAuthClient");
                var url = $"device/getdetail?DeviceID={deviceID}&Lang={Uri.EscapeDataString(lang)}";

                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var wrapper = await response.Content.ReadFromJsonAsync<ApiResponse<DeviceDetailProperty>>();
                return Ok(new
                {
                    data = wrapper?.Data ?? new List<DeviceDetailProperty>(),
                    isValid = true,
                    errors = Array.Empty<string>()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    data = Array.Empty<DeviceDetailProperty>(),
                    isValid = false,
                    errors = new[] { ex.Message }
                });
            }
        }

        [HttpGet("getstate")]
        public async Task<IActionResult> GetState(
                                        [FromQuery] int machineID,
                                        [FromQuery] string lang = "pl-pl")
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ExternalApiBearerAuthClient");
                var url = $"device/getstate?MachineID={machineID}&Lang={Uri.EscapeDataString(lang)}";

                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var wrapper = await response.Content.ReadFromJsonAsync<ApiResponse<DeviceState>>();
                return Ok(new
                {
                    data = wrapper?.Data ?? new List<DeviceState>(),
                    isValid = true,
                    errors = Array.Empty<string>()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    data = Array.Empty<DeviceState>(),
                    isValid = false,
                    errors = new[] { ex.Message }
                });
            }
        }

        // GET api/<WoController>/5
        [HttpGet("getstatus")]
        public async Task<IActionResult> GetStatus(
                                        [FromQuery] string lang = "pl-pl")
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ExternalApiBearerAuthClient");
                var url = $"device/getstatus?Lang={Uri.EscapeDataString(lang)}";

                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var wrapper = await response.Content.ReadFromJsonAsync<ApiResponse<DeviceStatus>>();
                return Ok(new
                {
                    data = wrapper?.Data ?? new List<DeviceStatus>(),
                    isValid = true,
                    errors = Array.Empty<string>()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    data = Array.Empty<DeviceStatus>(),
                    isValid = false,
                    errors = new[] { ex.Message }
                });
            }
        }

        // GET api/<WoController>/5
        [HttpGet("get")]
        public async Task<IActionResult> Get(
                                        [FromQuery] int machineID,
                                        [FromQuery] string lang = "pl-pl")
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ExternalApiBearerAuthClient");
                var url = $"device/get?MachineID={machineID}&Lang={Uri.EscapeDataString(lang)}";

                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var wrapper = await response.Content.ReadFromJsonAsync<SingleResponse<Device>>();
                return Ok(new
                {
                    data = wrapper?.Data ?? new Device(),
                    isValid = true,
                    errors = Array.Empty<string>()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    data = new Device(),
                    isValid = false,
                    errors = new[] { ex.Message }
                });
            }
        }

        [HttpGet("GetImage")]
        public async Task<IActionResult> GetImage(
                                        [FromQuery] int deviceID,
                                        [FromQuery] int? width = null,
                                        [FromQuery] int? height = null)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ExternalApiBearerAuthClient");
                var qp = new List<string>();
                qp.Add($"DeviceID={deviceID}");
                if (width.HasValue) qp.Add($"Width={width.Value}");
                if (height.HasValue) qp.Add($"Height={height.Value}");

                var url = "device/GetImage?" + string.Join("&", qp);

                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var wrapper = await response.Content.ReadFromJsonAsync<SingleResponse<DeviceImage>>();
                return Ok(new
                {
                    data = wrapper?.Data ?? new DeviceImage(),
                    isValid = true,
                    errors = Array.Empty<string>()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    data = Array.Empty<DeviceImage>(),
                    isValid = false,
                    errors = new[] { ex.Message }
                });
            }
        }

        [HttpGet("getdict")]
        public async Task<IActionResult> GetDict(
                                        [FromQuery] int personID,
                                        [FromQuery] string lang = "pl-pl")
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ExternalApiBearerAuthClient");
                var url = $"device/getdict?PersonID={personID}&Lang={Uri.EscapeDataString(lang)}";

                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var wrapper = await response.Content.ReadFromJsonAsync<ApiResponse<DeviceDict>>();
                return Ok(new
                {
                    data = wrapper?.Data ?? new List<DeviceDict>(),
                    isValid = true,
                    errors = Array.Empty<string>()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    data = Array.Empty<DeviceDict>(),
                    isValid = false,
                    errors = new[] { ex.Message }
                });
            }
        }

        [HttpGet("getdirfiles")]
        public async Task<IActionResult> GetDirectoryFiles([FromQuery] string directoryPath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(directoryPath))
                {
                    return BadRequest(new ApiResponse<WorkOrderFileItem>
                    {
                        IsValid = false,
                        Errors = new List<string> { "DirectoryPath cannot be empty." }
                    });
                }

                var client = _httpClientFactory.CreateClient("ExternalApiBearerAuthClient");
                var url = $"device/getdirfiles?DirectoryPath={Uri.EscapeDataString(directoryPath)}";

                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var wrapper = await response.Content.ReadFromJsonAsync<ApiResponse<WorkOrderFileItem>>();
                return Ok(new
                {
                    data = wrapper?.Data ?? new List<WorkOrderFileItem>(),
                    isValid = true,
                    errors = Array.Empty<string>()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<WorkOrderFileItem>
                {
                    IsValid = false,
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        [HttpGet("getfile")]
        public async Task<IActionResult> GetFile([FromQuery] string fileName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    return BadRequest(new SingleResponse<WorkOrderFileData>
                    {
                        IsValid = false,
                        Errors = new List<string> { "FileName cannot be empty." }
                    });
                }

                // Normalize the file path: replace double backslashes with single ones
                string normalizedFileName = fileName.Replace("\\", "\\");

                var client = _httpClientFactory.CreateClient("ExternalApiBearerAuthClient");
                var url = $"device/getfile?FileName={Uri.EscapeDataString(normalizedFileName)}";

                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var wrapper = await response.Content.ReadFromJsonAsync<SingleResponse<WorkOrderFileData>>();
                return Ok(new
                {
                    data = wrapper?.Data ?? new WorkOrderFileData(),
                    isValid = true,
                    errors = Array.Empty<string>()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new SingleResponse<WorkOrderFileData>
                {
                    IsValid = false,
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        [HttpGet("downloadfile")]
        public IActionResult DownloadFile([FromQuery] string filePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filePath))
                {
                    return BadRequest("File path cannot be empty.");
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    Console.WriteLine("Running on Windows. Using direct FileStream access." + filePath);

                    if (!System.IO.File.Exists(filePath))
                    {
                        return NotFound("File not found at path: " + filePath);
                    }

                    var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    var contentType = GetContentType(filePath);
                    var fileName = Path.GetFileName(filePath);

                    return File(fileStream, contentType, fileName);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Console.WriteLine("Running on Linux. Using smblibrary for SMB access." + filePath);

                    var username = "flexicmms";
                    var password = "CMMS#flexi";
                    //var serverName = filePath.Split(new[] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries)[0];

                    var normalized = filePath.Replace('\\', '/').Trim('/');      // server/share/dir/file
                    var parts = normalized.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length < 3)
                        return BadRequest("Wrong path. Expected: \\\\server\\share\\path\\to\\file");

                    var server = parts[0];
                    var share = parts[1];
                    var relativePath = string.Join("\\", parts.Skip(2));

                    var smbClient = new SMB2Client();

                    bool isConnected = smbClient.Connect(server, SMBTransportType.DirectTCPTransport);
                    if (!isConnected)
                    {
                        Debug.WriteLine("Could not connect to the SMB server: " + server);
                        return StatusCode(500, "Could not connect to the SMB server." + server);
                    }

                    var status = smbClient.Login(server, username, password);
                    if (status != NTStatus.STATUS_SUCCESS)
                    {
                        Debug.WriteLine("SMB login failed with status: " + status);
                        return StatusCode(500, "SMB login failed with status: " + status);
                    }
                    #region sample code

                    //public class SmbExample
                    //        {
                    //            public void ReadFileFromShare()
                    //            {
                    //                var client = new SMB2Client(); // Можно использовать SMB1Client, если сервер поддерживает только SMB1
                    //                bool isConnected = client.Connect(IPAddress.Parse("192.168.1.11"), SMBTransportType.DirectTCPTransport);

                    //                if (isConnected)
                    //                {
                    //                    NTStatus status = client.Login("DOMAIN", "Username", "Password");
                    //                    if (status == NTStatus.STATUS_SUCCESS)
                    //                    {
                    //                        ISMBFileStore fileStore = client.TreeConnect("SharedFolder", out status);
                    //                        if (status == NTStatus.STATUS_SUCCESS)
                    //                        {
                    //                            string filePath = @"TestFolder\TestFile.txt";
                    //                            object fileHandle;
                    //                            FileStatus fileStatus;

                    //                            status = fileStore.CreateFile(
                    //                                out fileHandle,
                    //                                out fileStatus,
                    //                                filePath,
                    //                                AccessMask.GENERIC_READ,
                    //                                FileAttributes.Normal,
                    //                                ShareAccess.Read,
                    //                                CreateDisposition.FILE_OPEN,
                    //                                CreateOptions.FILE_NON_DIRECTORY_FILE,
                    //                                null);

                    //                            if (status == NTStatus.STATUS_SUCCESS)
                    //                            {
                    //                                MemoryStream stream = new MemoryStream();
                    //                                byte[] data;
                    //                                long bytesRead = 0;

                    //                                while (true)
                    //                                {
                    //                                    status = fileStore.ReadFile(out data, fileHandle, bytesRead, 4096);
                    //                                    if (status != NTStatus.STATUS_SUCCESS || data.Length == 0)
                    //                                        break;

                    //                                    bytesRead += data.Length;
                    //                                    stream.Write(data, 0, data.Length);
                    //                                }

                    //                                string fileContent = System.Text.Encoding.UTF8.GetString(stream.ToArray());
                    //                                Console.WriteLine("File content:");
                    //                                Console.WriteLine(fileContent);

                    //                                fileStore.CloseFile(fileHandle);
                    //                            }

                    //                            fileStore.Disconnect();
                    //                        }

                    //                        client.Logoff();
                    //                    }

                    //                    client.Disconnect();
                    //                }
                    //            }
                    //        }

                    #endregion sample code
                    ISMBFileStore fileStore = smbClient.TreeConnect(share, out status);
                    if (status != NTStatus.STATUS_SUCCESS)
                    {
                        smbClient.Logoff();
                        smbClient.Disconnect();
                        Debug.WriteLine($"Failed to connect to the shared folder ({share}) with status: " + status);
                        return StatusCode(500, $"Failed to connect to the shared folder ({share}) with status: " + status);
                    }

                    string smbFilePath = filePath.Substring(filePath.IndexOf('/') + 1); // Убираем имя сервера из пути - remove server name from path
                    object fileHandle;
                    FileStatus fileStatus;

                    status = fileStore.CreateFile(
                        out fileHandle,
                        out fileStatus,
                        relativePath,
                        AccessMask.GENERIC_READ,
                        SMBLibrary.FileAttributes.Normal,
                        ShareAccess.Read,
                        CreateDisposition.FILE_OPEN,
                        CreateOptions.FILE_NON_DIRECTORY_FILE,
                        null);

                    if (status != NTStatus.STATUS_SUCCESS)
                    {
                        Debug.WriteLine("Failed to open the file with status: " + status);
                        return NotFound("File not found on the network share.");
                    }

                    using (var memoryStream = new MemoryStream())
                    {
                        byte[] data;
                        long bytesRead = 0;

                        while (true)
                        {
                            status = fileStore.ReadFile(out data, fileHandle, bytesRead, 4096);
                            if (status != NTStatus.STATUS_SUCCESS || data.Length == 0)
                                break;

                            bytesRead += data.Length;
                            memoryStream.Write(data, 0, data.Length);
                        }

                        fileStore.CloseFile(fileHandle);

                        var contentType = GetContentType(filePath);
                        var fileName = Path.GetFileName(filePath).Split('\\').Last();

                        return File(memoryStream.ToArray(), contentType, fileName);
                    }
                }
                else
                {
                    return StatusCode(501, $"Operating system {RuntimeInformation.OSDescription} is not supported.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while trying to download the file: {ex.Message}");
            }
        }

        [HttpPost("checksmb")]
        public IActionResult CheckCredentials([FromBody] SmbCredentials credentials)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(credentials.Server) ||
                    string.IsNullOrWhiteSpace(credentials.Username) ||
                    string.IsNullOrWhiteSpace(credentials.Password))
                {
                    return BadRequest("All fields must be filled.");
                }

                var smbPath = credentials.Server;
                //var smbPath = $"smb://{credentials.Server}/";
                var smbClient = new SMB2Client();
                bool isConnected = smbClient.Connect(smbPath, SMBTransportType.DirectTCPTransport);
                if (isConnected)
                {
                    Debug.WriteLine($"CheckCredentials ==> Connection to {smbPath} successful");
                }
                else
                {
                    Debug.WriteLine($"CheckCredentials ==> Wrong server path: {smbPath}");
                    return Unauthorized($"Wrong server path. {smbPath}");
                }

                var status = smbClient.Login(string.Empty, credentials.Username, credentials.Password);
                if (status != NTStatus.STATUS_SUCCESS)
                {
                    Debug.WriteLine($"CheckCredentials ==> SMB login failed with status: " + status);
                    return StatusCode(500, "SMB login failed with status: " + status);
                }
                else
                {
                    Debug.WriteLine($"CheckCredentials ==> SMB login successful");
                    return Ok("SMB login successful");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error checking credentials: {ex.Message}");
            }
        }


        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types.ContainsKey(ext) ? types[ext] : "application/octet-stream";
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
                // Add more as needed
            };
        }

        // GET api/<WoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<WoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<WoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
