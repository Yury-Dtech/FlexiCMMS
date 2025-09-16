using Newtonsoft.Json;

namespace BlazorTool.Client.Models
{
    public class Activity
    {
        [JsonProperty("workOrderID")]
        public int WorkOrderID { get; set; } = 0;

        [JsonProperty("activityID")]
        public int ActivityID { get; set; } = 0;

        [JsonProperty("work_Load")]
        public decimal WorkLoad { get; set; } = 0;

        [JsonProperty("workers")]
        public int Workers { get; set; } = 0;

        [JsonProperty("act_Category")]
        public string ActCategory { get; set; } = string.Empty;

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty("act_Persons")]
        public string ActPersons { get; set; } = string.Empty;

        [JsonProperty("add_Date")]
        public DateTime AddDate { get; set; } = DateTime.Now;

        [JsonProperty("act_PersonsName")]
        public string ActPersonsName { get; set; } = string.Empty;

        [JsonProperty("cost")]
        public decimal? Cost { get; set; } = 0;
    }    
}