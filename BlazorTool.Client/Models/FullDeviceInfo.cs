using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace BlazorTool.Client.Models
{
    public class FullDeviceInfo : Device
    {
        public List<DeviceDetailProperty>? Details { get; set; }
        public DeviceState? LastState { get { return StateHistory?.Where(ds => ds.ChangeTime.HasValue).OrderByDescending(ds => ds.ChangeTime).FirstOrDefault(); } }
        public List<DeviceState>? StateHistory { get; set; }
        public List<DeviceStatus>? Statuses { get; set; }
        public DeviceStatus? LastStatus { get { return Statuses?.Where(st => st.Type == LastState?.StateID).FirstOrDefault(); } }
        public List<DeviceImage>? Images { get; set; } = new List<DeviceImage>();
        public List<WorkOrderFileItem> DirectoryFiles { get; set; } = new List<WorkOrderFileItem>();
        public bool HaveFiles { get { return !string.IsNullOrWhiteSpace(this.DocumentationPath) && DirectoryFiles.Any(); } }

        public string StateName { get { return LastState?.StateName ?? "-";} }
    }
}
