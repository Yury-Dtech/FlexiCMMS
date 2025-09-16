using System;
using System.Text.Json.Serialization;

namespace BlazorTool.Client.Models
{
    public class WorkOrderView : WorkOrder
    {
        public string DeviceName { get; set; } = string.Empty;

        public WorkOrderView()
        {
        }

        public WorkOrderView(WorkOrder order, string deviceName)
        {
            // Copy all properties from WorkOrder
            this.WorkOrderID = order.WorkOrderID;
            this.MachineID = order.MachineID;
            this.AssetNo = order.AssetNo;
            this.DeviceCategory = order.DeviceCategory;
            this.WODesc = order.WODesc;
            this.WOCategory = order.WOCategory;
            this.WOState = order.WOState;
            this.WOReason = order.WOReason;
            this.AddDate = order.AddDate;
            this.TakeDate = order.TakeDate;
            this.CloseDate = order.CloseDate;
            this.Cost = order.Cost;
            this.TakePersons = order.TakePersons;
            this.PlanID = order.PlanID;
            this.StateColor = order.StateColor;
            this.ModDate = order.ModDate;
            this.ModPerson = order.ModPerson;
            this.WOLevel = order.WOLevel;
            this.LevelID = order.LevelID;
            this.ActCount = order.ActCount;
            this.DepName = order.DepName;
            this.AssignedPerson = order.AssignedPerson;
            this.FileCount = order.FileCount;
            this.PartCount = order.PartCount;
            this.PlanActCount = order.PlanActCount;
            this.StartDate = order.StartDate;
            this.EndDate = order.EndDate;
            this.IsScheduledPlanned = order.IsScheduledPlanned;
            this.CategoryID = order.CategoryID;
            this.ReasonID = order.ReasonID;
            this.IneffectiveCount = order.IneffectiveCount;
            this.PersonTakeDate = order.PersonTakeDate;
            this.DeviceCategoryID = order.DeviceCategoryID;

            // Set the additional property
            this.DeviceName = deviceName;
        }

        public WorkOrderView(SchedulerAppointment appointment, string deviceName)
            : this((WorkOrder)appointment, deviceName)
        {
        }

        public new WorkOrderView ShallowCopy()
        {
            var copy = (WorkOrderView)this.MemberwiseClone();
            return copy;
        }
    }
}