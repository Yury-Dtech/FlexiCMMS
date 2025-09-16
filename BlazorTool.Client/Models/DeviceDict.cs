using System.Text.Json.Serialization;

namespace BlazorTool.Client.Models
{
    public class DeviceDict
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("listType")]
        public int ListType { get; set; }
    }
}
