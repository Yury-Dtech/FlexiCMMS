using System.Text.Json.Serialization;

namespace BlazorTool.Client.Models
{
    public class SingleResponse<T>
    {
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;

        [JsonPropertyName("isValid")]
        public bool IsValid { get; set; }

        [JsonPropertyName("errors")]
        public List<string> Errors { get; set; } = new();
    }

    public class SimpleResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
