using BlazorTool.Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;

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
