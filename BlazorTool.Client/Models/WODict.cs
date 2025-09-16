using System.Text.Json.Serialization;
namespace BlazorTool.Client.Models
{
    public class WODict
    {
        //"machineCategoryID": null,
        //    "is_Default": false,
        //    "id": 1,
        //    "name": "Błąd technika",
        //    "listType": 3
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
