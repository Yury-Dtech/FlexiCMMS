using System.Text.Json.Serialization;

namespace BlazorTool.Client.Models
{
    //"workOrderID": 229,
    //    "addPersonID": 3,
    //    "addDate": "2025-07-09T11:14:05.787",
    //    "stateID": 5,
    //    "levelID": 3,
    //    "categoryID": 3,
    //    "reasonID": 8,
    //    "departmentID": 2,
    //    "assignedPersonID": null
    public class WorkOrderInfo
    {
        [JsonPropertyName("workOrderID")]
        public int WorkOrderID { get; set; }
        [JsonPropertyName("addPersonID")]
        public int? AddPersonID { get; set; }
        [JsonPropertyName("addDate")]
        public DateTime AddDate { get; set; }
        [JsonPropertyName("stateID")]
        public int? StateID { get; set; }
        [JsonPropertyName("levelID")]
        public int? LevelID { get; set; }
        [JsonPropertyName("categoryID")]
        public int? CategoryID { get; set; }
        [JsonPropertyName("reasonID")]
        public int? ReasonID { get; set; }
        [JsonPropertyName("departmentID")]
        public int? DepartmentID { get; set; }
        [JsonPropertyName("assignedPersonID")]
        public int? AssignedPersonID { get; set; }
    }
}
