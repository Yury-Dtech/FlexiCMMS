using System.Text.Json.Serialization;

namespace BlazorTool.Client.Models
{
    public class TokenResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
        
        [JsonPropertyName("langCode")]
        public string LangCode { get; set; }
        
        [JsonPropertyName("expires")]
        public DateTime Expires { get; set; } 
    }
}
