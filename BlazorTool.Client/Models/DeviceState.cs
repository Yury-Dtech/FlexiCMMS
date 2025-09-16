using System;
using System.Text.Json.Serialization;

namespace BlazorTool.Client.Models
{
    public class DeviceState
    {
        [JsonPropertyName("state_Name")]
        public string? StateName { get; set; }

        [JsonPropertyName("change_Time")]
        public DateTime? ChangeTime { get; set; }

        [JsonPropertyName("state_Time")]
        public int StateTime { get; set; }

        [JsonPropertyName("stateID")]
        public int StateID { get; set; }

        public override string ToString()
        {
            return StateName ?? string.Empty;
        }
    }
}
