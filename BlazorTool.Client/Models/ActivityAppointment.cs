namespace BlazorTool.Client.Models
{
    public class ActivityAppointment : Activity
    {
        public int AppointmentId { get => WorkOrderID; set => WorkOrderID = value; }
        public string Title
        {
            get
            {
                if (IsWorkOrder && WorkOrder != null)
                {
                    return ($"[{WorkOrder.WorkOrderID}] {WorkOrder.AssetNo}") ?? string.Empty;
                }

                if (string.IsNullOrEmpty(Description))
                {
                    return string.Empty;
                }

                int length = Description.Length > trimTitleSize ? trimTitleSize : Description.Length;
                return Description.Remove(length);
            }
        }
        public DateTime? Start { get => AddDate; set => AddDate = value ?? DateTime.Now; }
        public DateTime? End { get => AddDate.AddHours((double)WorkLoad); set => WorkLoad = (decimal)(value?.Subtract(AddDate).TotalHours ?? 0); }
        public bool IsAllDay { get; set; } = false;
        public WorkOrder? WorkOrder { get; set; }
        public bool IsWorkOrder { get; set; } = false;
        /// <summary>
        /// Count of characters to show in the title
        /// </summary>
        public int trimTitleSize = 30;
        public string WOState { get { 
                if(IsWorkOrder && WorkOrder != null)
                    return WorkOrder.WOState ?? WorkOrder.WorkOrderID.ToString() ?? "default";
               return WorkOrderID.ToString();
            }
            set {  }
        }

        public ActivityAppointment()
        {
            AppointmentId = 0;
            Start = DateTime.Now;
            End = DateTime.Now.AddHours(8);
            Description = string.Empty;
            IsWorkOrder = false;
        }

        public ActivityAppointment(Activity act)
        {
            CopyFromActivity(act);
        }

        public ActivityAppointment(Activity act, WorkOrder wo) : this(act)
        {
            this.WorkOrder = wo;
            this.IsWorkOrder = true;
        }

        public ActivityAppointment(WorkOrder wo)
        {
            this.WorkOrder = wo;
            this.IsWorkOrder = true;
            this.AppointmentId = wo.WorkOrderID;
            this.Start = wo.StartDate ?? wo.AddDate ?? DateTime.Now;
            this.End = wo.EndDate ?? wo.CloseDate ?? wo.AddDate?.AddHours(8) ?? DateTime.Now.AddHours(8);
            this.Description = wo.WODesc ?? string.Empty;
        }

        public ActivityAppointment ShallowCopy()
        {
            return (ActivityAppointment)this.MemberwiseClone();
        }

        public SchedulerAppointment ToSchedulerAppointment()
        {
            if (WorkOrder != null)
            {
                return new SchedulerAppointment(WorkOrder);
            }
            return new SchedulerAppointment
            {
                AppointmentId = this.AppointmentId,
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
