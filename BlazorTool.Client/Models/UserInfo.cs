using System.Text.Json.Serialization;

namespace BlazorTool.Client.Models
{
    public class UserInfo
    {
        [JsonPropertyName("personID")]
        public int PersonID { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("departmentID")]
        public int DepartmentID { get; set; }

        [JsonPropertyName("departmentShortName")]
        public string? DepartmentShortName { get; set; }

        [JsonPropertyName("active")]
        public bool Active { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }
    }
}
