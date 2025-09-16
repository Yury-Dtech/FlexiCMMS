namespace BlazorTool.Client.Models
{
    public class ContextMenuItem
    {
        public string Text { get; set; }
        public string CommandName { get; set; }
        public object? Icon { get; set; }
        public double CursorX { get; set; }
        public double CursorY { get; set; }
    }
}
