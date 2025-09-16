using System.Text.Json.Serialization;

namespace BlazorTool.Client.Models
{
    public class ApiResponse<T>
    {
        [JsonPropertyName("data")]
        public List<T> Data { get; set; }

        [JsonPropertyName("isValid")]
        public bool IsValid { get; set; }

        [JsonPropertyName("errors")]
        public List<string> Errors { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}
