using Telerik.Blazor.Components.Scheduler.Models;

namespace BlazorTool.Client.Models
{
    public class SchedulerAppointment : WorkOrder
    {
        public int AppointmentId { get => WorkOrderID; set => WorkOrderID = value; }
        public string Title { get => $"[{WorkOrderID}] {AssetNo} - {WODesc}" ?? string.Empty; set => AssetNo = value; }
        public DateTime? Start { get => StartDate; set => StartDate = value; }
        public DateTime? End { get => EndDate; set => EndDate = value; }
        public bool IsAllDay { get; set; }
        public string Description { get => WODesc ?? string.Empty; set => WODesc = value; }

        public SchedulerAppointment()
        {
            AppointmentId = 0;
            Title = string.Empty;
            Start = DateTime.Now;
            End = DateTime.Now.AddHours(5);
            IsAllDay = false;
            Description = string.Empty;
        }

        public SchedulerAppointment(WorkOrder order)
        {
            CopyFromWorkOrder(order);
            if (string.IsNullOrEmpty(AssignedPerson))
            {
                AssignedPerson = Person.NotAssigned.Name;
            }
        }

        public SchedulerAppointment ShallowCopy()
        {
            return (SchedulerAppointment)this.MemberwiseClone();
        }

        private void CopyFromWorkOrder(WorkOrder order)
        {
            this.IsAllDay = order.EndDate == null; 

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
        }
    }
}
