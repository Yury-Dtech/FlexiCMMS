using System;
using System.Text.Json.Serialization;

namespace BlazorTool.Client.Models
{
    public class WorkOrderCreateRequest
    {
        [JsonPropertyName("machineID")]
        public int MachineID { get; set; }

        [JsonPropertyName("personID")]
        public int PersonID { get; set; }

        [JsonPropertyName("levelID")]
        public int LevelID { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("start_Date")]
        public string? StartDate { get; set; }

        [JsonPropertyName("end_Date")]
        public string? EndDate { get; set; }

        [JsonPropertyName("reasonID")]
        public int? ReasonID { get; set; }

        [JsonPropertyName("categoryID")]
        public int? CategoryID { get; set; }

        [JsonPropertyName("departmentID")]
        public int? DepartmentID { get; set; }

        [JsonPropertyName("assignedPersonID")]
        public int? AssignedPersonID { get; set; }
    }
}
