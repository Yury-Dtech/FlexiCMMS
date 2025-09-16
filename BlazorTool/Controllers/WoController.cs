using BlazorTool.Client.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json; 

namespace BlazorTool.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WoController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WoController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList([FromQuery] WorkOrderQueryParameters q)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ExternalApiBearerAuthClient");

                var qp = new List<string>();
                if (q.DeviceID.HasValue) qp.Add($"DeviceID={q.DeviceID.Value}");
                if (q.WorkOrderID.HasValue) qp.Add($"WorkOrderID={q.WorkOrderID.Value}");
                if (!string.IsNullOrWhiteSpace(q.DeviceName)) qp.Add($"DeviceName={Uri.EscapeDataString(q.DeviceName)}");
                if (q.IsDep.HasValue) qp.Add($"IsDep={q.IsDep.Value}");
                if (q.IsTakenPerson.HasValue) qp.Add($"IsTakenPerson={q.IsTakenPerson.Value}");
                if (q.Active.HasValue) qp.Add($"Active={q.Active.Value}");
                if (!string.IsNullOrWhiteSpace(q.Lang)) qp.Add($"Lang={Uri.EscapeDataString(q.Lang)}");
                if (q.PersonID.HasValue) qp.Add($"PersonID={q.PersonID.Value}");
                if (q.IsPlan.HasValue) qp.Add($"IsPlan={q.IsPlan.Value}");
                if (q.IsWithPerson.HasValue) qp.Add($"IsWithPerson={q.IsWithPerson.Value}");

                var url = "wo/getlist?" + string.Join("&", qp);

                var response = await client.GetAsync(url); 
                response.EnsureSuccessStatusCode();
                var wrapper = await response.Content.ReadFromJsonAsync<ApiResponse<WorkOrder>>();

                var data = wrapper?.Data ?? new List<WorkOrder>();
                Console.WriteLine($"WOController --> GetList returned {data.Count} workorders");
                return Ok(new
                {
                    data,
                    isValid = true,
                    errors = Array.Empty<string>()
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WOController --> Error in GetList: {ex.Message}");
                return StatusCode(500, new
                {
                    data = Array.Empty<object>(),
                    isValid = false,
                    errors = new[] { ex.Message }
                });
            }
        }

        [HttpGet("getdict")]
        public async Task<IActionResult> GetWODicts([FromQuery] WOCategoriesParameters q)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ExternalApiBearerAuthClient"); // Get named client

                var qp = new List<string>();
                qp.Add($"PersonID={q.PersonID}");
                qp.Add($"Lang={q.Lang}");
                var url = "wo/getdict?" + string.Join("&", qp);

                var response = await client.GetAsync(url); // Use the named client
                response.EnsureSuccessStatusCode();

                var wrapper = await response.Content.ReadFromJsonAsync<ApiResponse<WODict>>();

                var data = wrapper?.Data ?? new List<WODict>();
                Console.WriteLine($"WOController --> GetWODicts returned {data.Count} items");
                return Ok(new
                {
                    data,
                    isValid = true,
                    errors = Array.Empty<string>()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    data = Array.Empty<object>(),
                    isValid = false,
                    errors = new[] { ex.Message }
                });
            }
        }



        // GET api/<WoController>/get?WorkOrderID=2
        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] int WorkOrderID)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ExternalApiBearerAuthClient");
                string url = $"wo/get?WorkOrderID={WorkOrderID}";
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var apiResponse = await response.Content.ReadFromJsonAsync<SingleResponse<WorkOrderInfo>>();
                if (apiResponse == null || !apiResponse.IsValid)
                {
                    var errors = apiResponse?.Errors ?? new List<string> { "Unknown error occurred." };
                    return NotFound(new SingleResponse<WorkOrderInfo>
                    {
                        IsValid = false,
                        Errors = errors
                    });
                }
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getwotaken")]
        public async Task<IActionResult> GetWOTaken([FromQuery] int PersonID)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ExternalApiBearerAuthClient");
                string url = $"wo/getwotaken?PersonID={PersonID}";
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<WorkOrderIdentifier>>();
                if (apiResponse == null || !apiResponse.IsValid)
                {
                    var errors = apiResponse?.Errors ?? new List<string> { "Unknown error occurred." };
                    return NotFound(new ApiResponse<WorkOrderIdentifier>
                    {
                        Data = new List<WorkOrderIdentifier>(),
                        IsValid = false,
                        Errors = errors
                    });
                }
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getfiles")]
        public async Task<IActionResult> GetFiles([FromQuery] int WorkOrderID)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ExternalApiBearerAuthClient");
                string url = $"wo/getfiles?WorkOrderID={WorkOrderID}";
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<WorkOrderFile>>();
                if (apiResponse == null || !apiResponse.IsValid)
                {
                    var errors = apiResponse?.Errors ?? new List<string> { "Unknown error occurred." };
                    return NotFound(new ApiResponse<WorkOrderFile>
                    {
                        Data = new List<WorkOrderFile>(),
                        IsValid = false,
                        Errors = errors
                    });
                }
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WOController --> Error in GetFiles: {ex.Message}");
                return StatusCode(500, new ApiResponse<WorkOrderFile>
                {
                    Data = new List<WorkOrderFile>(),
                    IsValid = false,
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        [HttpGet("getfile")]
        public async Task<IActionResult> GetFile([FromQuery] int WorkOrderDataID, [FromQuery] int Width, [FromQuery] int Height)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ExternalApiBearerAuthClient");
                string url = $"wo/getfile?WorkOrderDataID={WorkOrderDataID}&Width={Width}&Height={Height}";
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var apiResponse = await response.Content.ReadFromJsonAsync<SingleResponse<WorkOrderFileData>>();
                if (apiResponse == null || !apiResponse.IsValid)
                {
                    var errors = apiResponse?.Errors ?? new List<string> { "Unknown error occurred." };
                    return NotFound(new SingleResponse<WorkOrderFileData>
                    {
                        IsValid = false,
                        Errors = errors
                    });
                }
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WOController --> Error in GetFile: {ex.Message}");
                return StatusCode(500, new SingleResponse<WorkOrderFileData>
                {
                    Data = null,
                    IsValid = false,
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        // POST api/<WoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] WorkOrderCreateRequest createRequest)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ExternalApiBearerAuthClient");
                var url = "wo/create";

                var response = await client.PostAsJsonAsync(url, createRequest);
                response.EnsureSuccessStatusCode();

                var apiResponse = await response.Content.ReadFromJsonAsync<SingleResponse<WorkOrder>>();
                Console.WriteLine($"requested URL: {client.BaseAddress}/{url}");
                Console.WriteLine($"WOController --> Create returned WorkOrderID: {apiResponse?.Data?.WorkOrderID}");
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WOController --> Error in Create: {ex.Message}");
                return StatusCode(500, new SingleResponse<WorkOrder>
                {
                    IsValid = false,
                    Errors = new List<string> { ex.Message }
                });
            }
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

        [HttpPut("close")]
        public async Task<IActionResult> CloseWorkOrder([FromBody] CloseWorkOrderRequest request)
        {
            if (request == null || request.WorkOrderID <= 0)
            {
                return BadRequest(new SingleResponse<bool>
                {
                    IsValid = false,
                    Errors = new List<string> { "WorkOrderID is required." }
                });
            }

            try
            {
                var client = _httpClientFactory.CreateClient("ExternalApiBearerAuthClient");
                var url = "wo/close";

                var response = await client.PutAsJsonAsync(url, request);

                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = System.Text.Json.JsonSerializer.Deserialize<SingleResponse<bool>>(responseContent);
                    Console.WriteLine($"WOController --> CloseWorkOrder returned: {apiResponse?.IsValid}");
                    return Ok(apiResponse);
                }
                else
                {
                    try
                    {
                        var errorResponse = System.Text.Json.JsonSerializer.Deserialize<SingleResponse<bool>>(responseContent);
                        if (errorResponse != null)
                        {
                            Console.WriteLine($"WOController --> CloseWorkOrder error: {string.Join(", ", errorResponse.Errors)}");
                            return StatusCode((int)response.StatusCode, errorResponse);
                        }
                    }
                    catch { }
                    Console.WriteLine($"WOController --> CloseWorkOrder API Error: {response.ReasonPhrase}, Content: {responseContent}");
                    return StatusCode((int)response.StatusCode, new SingleResponse<bool>
                    {
                        IsValid = false,
                        Errors = new List<string> { $"API Error: {response.ReasonPhrase}", responseContent }
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WOController --> Error in CloseWorkOrder: {ex.Message}");
                return StatusCode(500, new SingleResponse<bool>
                {
                    IsValid = false,
                    Errors = new List<string> { ex.Message }
                });
            }
        }
    

    

    [HttpPut("update")]
        public async Task<IActionResult> UpdateWorkOrder([FromBody] UpdateWorkOrderRequest request)
        {
            if (request == null || request.WorkOrderID <= 0)
            {
                return BadRequest(new SingleResponse<bool>
                {
                    IsValid = false,
                    Errors = new List<string> { "A valid WorkOrderID is required." }
                });
            }

            try
            {
                var client = _httpClientFactory.CreateClient("ExternalApiBearerAuthClient");
                var url = "wo/update";

                var response = await client.PutAsJsonAsync(url, request);

                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = System.Text.Json.JsonSerializer.Deserialize<SingleResponse<bool>>(responseContent);
                    Console.WriteLine($"WOController --> WorkOrderID={request.WorkOrderID} updated: {apiResponse?.IsValid}");
                    return Ok(apiResponse);
                }
                else
                {
                    try
                    {
                        var errorResponse = System.Text.Json.JsonSerializer.Deserialize<SingleResponse<bool>>(responseContent);
                        if (errorResponse != null && errorResponse.Errors.Any())
                        {
                            Console.WriteLine($"WOController --> UpdateWorkOrder error: {string.Join(", ", errorResponse.Errors)}");
                            return StatusCode((int)response.StatusCode, errorResponse);
                        }
                    }
                    catch {  }
                    Console.WriteLine($"WOController --> UpdateWorkOrder API Error: {response.ReasonPhrase}, Content: {responseContent}");
                    return StatusCode((int)response.StatusCode, new SingleResponse<bool>
                    {
                        IsValid = false,
                        Errors = new List<string> { $"API Error: {response.ReasonPhrase}", responseContent }
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WOController --> Error in UpdateWorkOrder: {ex.Message}");  
                return StatusCode(500, new SingleResponse<bool>
                {
                    IsValid = false,
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        [HttpPut("take")]
        public async Task<IActionResult> TakeWorkOrder([FromBody] WorkOrderTakeRequest request)
        {
            if (request == null || request.WorkOrderID <= 0)
            {
                return BadRequest(new SingleResponse<bool>
                {
                    IsValid = false,
                    Errors = new List<string> { "WorkOrderID is required." }
                });
            }

            try
            {
                var client = _httpClientFactory.CreateClient("ExternalApiBearerAuthClient");
                var url = "wo/take";

                var response = await client.PutAsJsonAsync(url, request);

                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = System.Text.Json.JsonSerializer.Deserialize<SingleResponse<bool>>(responseContent);
                    Console.WriteLine($"WOController --> TakeWorkOrder returned: {apiResponse?.IsValid}");
                    return Ok(apiResponse);
                }
                else
                {
                    try
                    {
                        var errorResponse = System.Text.Json.JsonSerializer.Deserialize<SingleResponse<bool>>(responseContent);
                        if (errorResponse != null)
                        {
                            Console.WriteLine($"WOController --> TakeWorkOrder error: {string.Join(", ", errorResponse.Errors)}");
                            return StatusCode((int)response.StatusCode, errorResponse);
                        }
                    }
                    catch { }
                    Console.WriteLine($"WOController --> TakeWorkOrder API Error: {response.ReasonPhrase}, Content: {responseContent}");
                    return StatusCode((int)response.StatusCode, new SingleResponse<bool>
                    {
                        IsValid = false,
                        Errors = new List<string> { $"API Error: {response.ReasonPhrase}", responseContent }
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WOController --> Error in TakeWorkOrder: {ex.Message}");
                return StatusCode(500, new SingleResponse<bool>
                {
                    IsValid = false,
                    Errors = new List<string> { ex.Message }
                });
            }
        }
    }
}
