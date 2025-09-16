using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlazorTool.Client.Models
{
    public class OrderStatus
    {
        [JsonPropertyName("machineCategoryID")]
        public int? MachineCategoryID { get; set; }

        [JsonPropertyName("is_Default")]
        public bool IsDefault { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("listType")]
        public int ListType { get; set; }
    }
}
