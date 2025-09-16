using System;

namespace BlazorTool.Client.Models
{
    public class ContextMenuItemClickEventArgs<TItem> : EventArgs
    {
        public ContextMenuItem? MenuItem { get; set; }
        public TItem? Item { get; set; }
    }
}
