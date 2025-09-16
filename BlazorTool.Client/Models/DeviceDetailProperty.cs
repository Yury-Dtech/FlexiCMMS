using System.Text.Json.Serialization;

namespace BlazorTool.Client.Models
{
    public class DeviceDetailProperty
    {
        [JsonPropertyName("display_Index")]
        public int DisplayIndex { get; set; }

        [JsonPropertyName("property_Name")]
        public string? PropertyName { get; set; }

        [JsonPropertyName("property_Value")]
        public string? PropertyValue { get; set; }

        [JsonPropertyName("property_Type")]
        public string? PropertyType { get; set; }
    }
}
