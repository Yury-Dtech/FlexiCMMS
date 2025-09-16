using Newtonsoft.Json;

namespace BlazorTool.Client.Models
{
    public class UpdateActivity
    {
        //        {
        //  "activityID": 347,
        //  "personID": {{personID}},
        //  "categoryID": { { actCatID} },
        //  "description": "Aktywność Opis: ##TEST długi opis Interfejs Aktywności / Komponenty: Dodano komponent ActivityCard z obsługą lokalizacji oraz zmiennymi CSS dla lepszego themingu. Zaktualizowano ActivitySchedulerItem oraz style ActivityDisplay, aby poprawić renderowanie terminów i wspierać różne widoki harmonogramu",
        //  "workLoad": 2,
        //  "cost": 10,
        //  "actDate" : "2025-09-04T12:00:36.813"
        //}

        [JsonProperty("activityID")]
        public int ActivityID { get; set; } = 0;
        [JsonProperty("personID")]
        public int PersonID { get; set; } = 0;
        [JsonProperty("categoryID")]
        public int CategoryID { get; set; } = 0;
        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;
        [JsonProperty("workLoad")]
        public decimal WorkLoad { get; set; } = 0;
        [JsonProperty("cost")]
        public decimal? Cost { get; set; } = 0;
        [JsonProperty("actDate")]
        public DateTime ActDate { get; set; } = DateTime.Now;
    }
}
