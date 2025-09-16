using System.Text.Json.Serialization;

namespace BlazorTool.Client.Models
{
    public class DeviceStatus
    {
        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("deviceID")]
        public int DeviceID { get; set; }

        [JsonPropertyName("device_Name")]
        public string? DeviceName { get; set; }

        [JsonPropertyName("icon")]
        public string? Icon { get; set; }

        [JsonPropertyName("color")]
        public string? Color { get; set; }

        [JsonPropertyName("detail")]
        public string? Detail { get; set; }
    }
}
