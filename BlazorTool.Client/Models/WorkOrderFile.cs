using System.Text.Json.Serialization;

namespace BlazorTool.Client.Models
{
    public class WorkOrderFile
    {
        [JsonPropertyName("workOrderDataID")]
        public int WorkOrderDataID { get; set; }
        [JsonPropertyName("extension")]
        public string Extension { get; set; }
        [JsonPropertyName("file_Name")]
        public string File_Name { get; set; }
        [JsonPropertyName("syncId")]
        public string? SyncId { get; set; }
    }
}