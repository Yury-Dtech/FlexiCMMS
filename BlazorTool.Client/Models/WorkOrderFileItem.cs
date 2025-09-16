using System.Text.Json.Serialization;

namespace BlazorTool.Client.Models
{
    public class WorkOrderFileItem
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("fullName")]
        public string FullName { get; set; }
        [JsonPropertyName("isDir")]
        public bool IsDir { get; set; }
    }
}