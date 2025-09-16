using System.Text.Json.Serialization;

namespace BlazorTool.Client.Models
{
    public class DeviceImage
    {
        [JsonPropertyName("image")]
        public string? Image { get; set; }

        [JsonPropertyName("syncId")]
        public string? SyncId { get; set; }
    }
}
