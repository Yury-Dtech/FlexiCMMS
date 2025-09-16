namespace BlazorTool.Client.Models
{

    public class UpdateWorkOrderRequest
    {
        public int WorkOrderID { get; set; }
        public int PersonID { get; set; }
        public int? CategoryID { get; set; }
        public int? LevelID { get; set; }
        public int? ReasonID { get; set; }
        public string? Description { get; set; }
        public int? DepartmentID { get; set; }
        public int? AssignedPersonID { get; set; }
        public string? Location { get; set; }
        public string? Start_Date { get; set; }
        public string? End_Date { get; set; }
        public int? StateID { get; set; }
    }

    public class CloseWorkOrderRequest
    {
        public int WorkOrderID { get; set; }
        public int PersonID { get; set; }
        public int CategoryID { get; set; }
        public int ReasonID { get; set; }
        public int LevelID { get; set; }
    }

    public class WorkOrderQueryParameters
    {
        public int? DeviceID { get; set; }
        public int? WorkOrderID { get; set; }
        public string? DeviceName { get; set; }
        public bool? IsDep { get; set; }
        public bool? IsTakenPerson { get; set; }
        public bool? Active { get; set; }
        public string Lang { get; set; } = "pl-PL";
        public int? PersonID { get; set; }
        public bool? IsPlan { get; set; }
        public bool? IsWithPerson { get; set; }
    }

    public class WOCategoriesParameters
    {
        public int PersonID { get; set; }
        public string Lang { get; set; } = "pl-PL";
    }

    public class WorkOrderTakeRequest
    {
        public int WorkOrderID { get; set; }
        public int PersonID { get; set; }
        public int LevelID { get; set; }
        public int ReasonID { get; set; }
        public int CategoryID { get; set; }
    }
}
