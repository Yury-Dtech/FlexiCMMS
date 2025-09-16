namespace BlazorTool.Client.Models
{
    public class PersonWorkOrderSummary
    {
        public string PersonName { get; set; } = string.Empty;
        public int TotalCount { get; set; }
        public Dictionary<string, int> StatusCounts { get; set; } = new Dictionary<string, int>();
    }
}
