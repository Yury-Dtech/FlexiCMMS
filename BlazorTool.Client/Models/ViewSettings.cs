using System.Text.Json.Serialization;
using Telerik.Blazor.Components;

namespace BlazorTool.Client.Models
{
    /// <summary>
    /// Represents the settings for a specific view, including grid state and custom filters.
    /// </summary>
    /// <typeparam name="T">The model type for the Telerik Grid.</typeparam>
    public class ViewSettings<T>
    {
        /// <summary>
        /// Stores the complete state of the Telerik Grid, including filters, sorting, paging, etc.
        /// </summary>
        public GridState<T> GridState { get; set; }

        /// <summary>
        /// A flexible dictionary to store values from custom filter controls (e.g., text boxes, dropdowns).
        /// The key is the filter identifier, and the value is the filter value.
        /// </summary>
        public Dictionary<string, object> CustomFilters { get; set; } = new();

        /// <summary>
        /// An additional dictionary for any other miscellaneous view-specific data a user might want to save.
        /// </summary>
        public Dictionary<string, object> AdditionalData { get; set; } = new();

        public ViewSettings()
        {
            GridState = new GridState<T>();
        }
    }
    
    public class SectionCollapseStates
    {
        public bool AssignedCollapsed { get; set; } = false;
        public bool TakenCollapsed { get; set; } = false;
        public bool DeptCollapsed { get; set; } = false;
        public bool WorkOrdersWithPersonCollapsed { get; set; } = false;
        
        // Размеры панелей для TelerikSplitter
        public string AssignedPaneSize { get; set; } = "450px";
        public string TakenPaneSize { get; set; } = "250px";
        public string DeptPaneSize { get; set; } = "550px";
        public string WorkOrdersWithPersonPaneSize { get; set; } = "500px";
    }
}