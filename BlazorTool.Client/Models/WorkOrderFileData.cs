using System.Text.Json.Serialization;

namespace BlazorTool.Client.Models
{
    public class WorkOrderFileData
    {
        [JsonPropertyName("workOrderDataId")]
        public int WorkOrderDataId { get; set; }
        [JsonPropertyName("data")]
        public string Data { get; set; } = string.Empty;
        [JsonPropertyName("syncId")]
        public string? SyncId { get; set; }
    }
}