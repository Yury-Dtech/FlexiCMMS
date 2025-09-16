using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using BlazorTool.Client.Resources;

namespace BlazorTool.Client.Models
{
    public class AddActivity
    {
            [JsonProperty("workOrderID")]
            public int WorkOrderID { get; set; }

            [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName = "Person_Required")]
            [Range(1, int.MaxValue, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName = "Person_MustBeSelected")]
            [JsonProperty("personID")]
            public int PersonID { get; set; }

            [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName = "Category_Required")]
            [Range(1,int.MaxValue, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName = "Category_MustBeSelected")]
            [JsonProperty("categoryID")]
            public int CategoryID { get; set; }

            [Range(0, 24, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName = "Workload_Positive")]
            [JsonProperty("workLoad")]
            public decimal WorkLoad { get; set; } = 0;

            [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName = "Description_Required")]
            [JsonProperty("description")]
            public string Description { get; set; } = string.Empty;

            [Range(0, double.MaxValue, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName = "Cost_Positive")]
            [JsonProperty("cost")]
            public decimal? Cost { get; set; }

            [JsonProperty("actDate")]
            public DateTime? ActDate { get; set; }

    }

    public class JoinToActivity
    {
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName = "Person_Required")]
        [Range(1, int.MaxValue, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName = "Person_MustBeSelected")]
        [JsonProperty("personID")]
        public int PersonID { get; set; }

        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName = "Activity_Required")]
        [Range(1, int.MaxValue, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName = "Activity_MustBeSelected")]
        [JsonProperty("activityID")]
        public int ActivityID { get; set; }

        [Range(0, double.MaxValue, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName = "Workload_Positive")]
        [JsonProperty("work_Load")]
        public decimal WorkLoad { get; set; } = 0;
    }
}
