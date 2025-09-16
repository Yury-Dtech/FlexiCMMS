using System.Text.Json.Serialization;

namespace BlazorTool.Client.Models
{
    public class RightMatrix
    {
        [JsonPropertyName("personID")]
        public int PersonID { get; set; }

        [JsonPropertyName("wO_Add")]
        public bool WO_Add { get; set; }

        [JsonPropertyName("wO_Edit")]
        public bool WO_Edit { get; set; }

        [JsonPropertyName("wO_Edit_Description")]
        public bool WO_Edit_Description { get; set; }

        [JsonPropertyName("wO_Del")]
        public bool WO_Del { get; set; }

        [JsonPropertyName("wO_Close")]
        public bool WO_Close { get; set; }

        [JsonPropertyName("acT_Add")]
        public bool AcT_Add { get; set; }

        [JsonPropertyName("acT_Del")]
        public bool AcT_Del { get; set; }

        [JsonPropertyName("acT_Edit_Description")]
        public bool AcT_Edit_Description { get; set; }

        [JsonPropertyName("parT_WO_take")]
        public bool ParT_WO_take { get; set; }

        [JsonPropertyName("parT_WO_Order")]
        public bool ParT_WO_Order { get; set; }

        [JsonPropertyName("parT_Edit_State")]
        public bool ParT_Edit_State { get; set; }

        [JsonPropertyName("wO_SET_AssignedPerson")]
        public bool WO_SET_AssignedPerson { get; set; }

        [JsonPropertyName("parT_Give")]
        public bool ParT_Give { get; set; }

        [JsonPropertyName("mD_Add")]
        public bool MD_Add { get; set; }

        [JsonPropertyName("mD_Edit")]
        public bool MD_Edit { get; set; }

        [JsonPropertyName("mD_Edit_Warranty")]
        public bool MD_Edit_Warranty { get; set; }

        [JsonPropertyName("parT_Add")]
        public bool ParT_Add { get; set; }

        [JsonPropertyName("mD_Add_ForceCycle")]
        public bool MD_Add_ForceCycle { get; set; }
    }
}
