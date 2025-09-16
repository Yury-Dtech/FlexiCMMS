using Microsoft.Extensions.Localization;
using Telerik.Blazor.Services;
using BlazorTool.Client.Resources;

namespace BlazorTool.Client.Services
{
    public class CustomTelerikLocalizer : ITelerikStringLocalizer
    {
        private readonly IStringLocalizer<TelerikMessages> _localizer;

        public CustomTelerikLocalizer(IStringLocalizer<TelerikMessages> localizer)
        {
            _localizer = localizer;
        }

        public string this[string name] => _localizer[name];
    }
}