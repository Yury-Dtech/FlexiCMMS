using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace BlazorTool.Client.Models
{
    public class FullDeviceInfo : Device
    {
        public List<DeviceDetailProperty>? Details { get; set; }
        public DeviceState? State { get; set; }
        public List<DeviceStatus>? Statuses { get; set; }
        public List<DeviceImage>? Images { get; set; } = new List<DeviceImage>();
        public List<WorkOrderFileItem> DirectoryFiles { get; set; } = new List<WorkOrderFileItem>();
    }
}
