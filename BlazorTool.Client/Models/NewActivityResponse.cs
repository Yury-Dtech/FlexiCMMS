using System.Text.Json.Serialization;
namespace BlazorTool.Client.Models
{
    public class NewActivityResponse
    {
    //"activityID": 220,
    //"workOrderID": 217,
    //"description": "Test",
    //"categoryID": 1,
    //"work_Load": 1,
    //"cost": 0.00,
    //"workers": 1
    [JsonPropertyName("activityID")]
    public int ActivityID { get; set; }

    [JsonPropertyName("workOrderID")]
    public int WorkOrderID { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("categoryID")]
    public int CategoryID { get; set; }

    [JsonPropertyName("work_Load")]
    public decimal WorkLoad { get; set; }

    [JsonPropertyName("cost")]
    public decimal Cost { get; set; }

    [JsonPropertyName("workers")]
    public int Workers { get; set; }

    }

    public class AddToActivityResponse
    {
        [JsonPropertyName("activityID")]
        public int ActivityID { get; set; }
       
        [JsonPropertyName("work_Load")]
        public decimal WorkLoad { get; set; }

        [JsonPropertyName("personID")]
        public int PersonID { get; set; }

        [JsonPropertyName("activityPersonID")]
        public int ActivityPersonID { get; set; }
        
        [JsonPropertyName("add_Date")]
        public DateTime AddDate { get; set; }
    }
}
