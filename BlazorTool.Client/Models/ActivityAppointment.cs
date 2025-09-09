using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BlazorTool.Client.Models
{
    public class ActivityAppointment : Activity
    {
        public int AppointmentId { get; private set; }
        public string Title { get; private set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public bool IsAllDay { get; set; } = false;
        public WorkOrder? WorkOrder { private get; set; }
        public bool IsWorkOrder { get; set; } = false;
        public int trimTitleSize = 30;
        public string WOState { get; private set; }

        public ActivityAppointment()
        {
            CopyFromActivity(new Activity());
            WorkOrder = new WorkOrder();
            IsWorkOrder = false;
            SetCalculatedFields();
        }

        public ActivityAppointment(Activity act)
        {
            CopyFromActivity(act);
            WorkOrder = null;
            IsWorkOrder = false;
            SetCalculatedFields();
        }
        public decimal GetWorkLoadHours()
        {
            return (decimal)(End?.Subtract(Start ?? DateTime.Now).TotalHours ?? 0);
        }

        public WorkOrder GetWorkOrder()
        {
            if (WorkOrder == null) 
                WorkOrder = new WorkOrder();

            WorkOrder.WorkOrderID = WorkOrderID;
            WorkOrder.StartDate = Start;
            WorkOrder.EndDate = End;
            WorkOrder.WODesc = Description;
            return WorkOrder;
        }
        public ActivityAppointment(Activity act, WorkOrder wo) : this(act)
        {
            WorkOrder = wo;
            IsWorkOrder = true;
            SetCalculatedFields();
        }

        public ActivityAppointment(WorkOrder? wo)
        { if (wo == null) 
                wo = new WorkOrder();
            this.WorkOrderID = wo.WorkOrderID;
            this.IsWorkOrder = true;
            this.WorkOrder = wo;
            this.Start = wo.StartDate ?? wo.AddDate ?? DateTime.Now;
            this.End = wo.EndDate ?? wo.CloseDate ?? wo.AddDate?.AddHours(8) ?? DateTime.Now.AddHours(8);
            this.Description = wo.WODesc ?? string.Empty;
            SetCalculatedFields();
        }

        private void SetCalculatedFields()
        {
            AppointmentId = IsWorkOrder ? WorkOrderID : 10000 * WorkOrderID + ActivityID;

            if (IsWorkOrder && WorkOrder != null)
            {
                Title = $"[{WorkOrder.WorkOrderID}] {WorkOrder.AssetNo}" ?? string.Empty;
            }
            else if (!string.IsNullOrEmpty(Description))
            {
                int length = Description.Length > trimTitleSize ? trimTitleSize : Description.Length;
                Title = Description.Remove(length);
            }
            else
            {
                Title = string.Empty;
            }

            if (IsWorkOrder && WorkOrder != null)
            {
                WOState = WorkOrder.WOState ?? WorkOrder.WorkOrderID.ToString() ?? "default";
            }
            else
            {
                WOState = WorkOrderID.ToString();
            }

            if (!IsWorkOrder)
            {
                Start ??= AddDate;
                End ??= AddDate.AddHours((double)WorkLoad);
            }
        }

        public ActivityAppointment ShallowCopy()
        {
            return (ActivityAppointment)this.MemberwiseClone();
        }

        public SchedulerAppointment ToSchedulerAppointment()
        {
            if (WorkOrder != null)
            {                
                return new SchedulerAppointment(GetWorkOrder());
            }
            return new SchedulerAppointment
            {
                AppointmentId = this.WorkOrderID,
                Title = this.Title,
                Start = this.Start,
                End = this.End,
                IsAllDay = this.IsAllDay,
                Description = this.Description,
            };
        }

        private void CopyFromActivity(Activity activity)
        {
            this.WorkOrderID = activity.WorkOrderID;
            this.ActivityID = activity.ActivityID;
            this.WorkLoad = activity.WorkLoad;
            this.Workers = activity.Workers;
            this.ActCategory = activity.ActCategory;
            this.Description = activity.Description;
            this.ActPersons = activity.ActPersons;
            this.AddDate = activity.AddDate;
            this.ActPersonsName = activity.ActPersonsName;
            this.Cost = activity.Cost;
        }
    }
}
