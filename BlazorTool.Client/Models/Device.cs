using System.Text.Json.Serialization;

namespace BlazorTool.Client.Models
{
    public class Device
    {
        [JsonPropertyName("machineID")]
        public int MachineID { get; set; }

        [JsonPropertyName("assetNo")]
        public string? AssetNo { get; set; }

        [JsonPropertyName("assetNoShort")]
        public string? AssetNoShort { get; set; }

        [JsonPropertyName("deviceCategory")]
        public string? DeviceCategory { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("serialNo")]
        public string? SerialNo { get; set; }

        [JsonPropertyName("stateID")]
        public int? StateID { get; set; }

        [JsonPropertyName("categoryID")]
        public int? CategoryID { get; set; }

        [JsonPropertyName("documentationPath")]
        public string? DocumentationPath { get; set; }

        [JsonPropertyName("location")]
        public string? Location { get; set; }

        [JsonPropertyName("locationRequired")]
        public bool? LocationRequired { get; set; }

        [JsonPropertyName("locationName")]
        public string? LocationName { get; set; }

        [JsonPropertyName("place")]
        public string? Place { get; set; }

        [JsonPropertyName("isCritical")]
        public bool? IsCritical { get; set; }

        [JsonPropertyName("setName")]
        public string? SetName { get; set; }

        [JsonPropertyName("setID")]
        public int? SetID { get; set; }

        [JsonPropertyName("active")]
        public bool? Active { get; set; }

        [JsonPropertyName("cycle")]
        public object? Cycle { get; set; }

        [JsonPropertyName("owner")]
        public string? Owner { get; set; }
    }
}
