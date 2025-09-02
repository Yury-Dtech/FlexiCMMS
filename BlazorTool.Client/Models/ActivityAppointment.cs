namespace BlazorTool.Client.Models
{
    public class ActivityAppointment : Activity
    {
        public int AppointmentId { get => WorkOrderID; set => WorkOrderID = value; }
        public string Title { get => Description?.Remove(Description.Length > trimTitleSize ? trimTitleSize : Description.Length) ?? string.Empty; }
        public DateTime? Start { get => AddDate; set => AddDate = value ?? DateTime.Now; }
        public DateTime? End { get => AddDate.AddHours((double)WorkLoad); set => WorkLoad = (decimal)(value?.Subtract(AddDate).TotalHours ?? 0); }
        public bool IsAllDay { get; set; } = false;
        /// <summary>
        /// Count of characters to show in the title
        /// </summary>
        public int trimTitleSize = 30;

        public ActivityAppointment()
        {
            AppointmentId = 0;
            Start = DateTime.Now;
            End = DateTime.Now.AddHours(8);
            Description = string.Empty;
        }

        public ActivityAppointment(Activity act)
        {
            CopyFromActivity(act);
        }

        public ActivityAppointment ShallowCopy()
        {
            return (ActivityAppointment)this.MemberwiseClone();
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
