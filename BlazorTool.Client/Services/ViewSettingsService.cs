using Blazored.LocalStorage;
using BlazorTool.Client.Models;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Text.Json;


namespace BlazorTool.Client.Services
{
    public class ViewSettingsService
    {
        private readonly ILocalStorageService _localStorage;

        public ViewSettingsService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task SaveSettingsAsync<T>(string viewKey, ViewSettings<T> settings)
        {
            try
            {
                await _localStorage.SetItemAsync(viewKey, settings);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving settings for '{viewKey}': {ex.Message}");
            }
        }

        public async Task<ViewSettings<T>> LoadSettingsAsync<T>(string viewKey)
        {
            try
            {
                var settings = await _localStorage.GetItemAsync<ViewSettings<T>>(viewKey);
                return settings ?? new ViewSettings<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading settings for '{viewKey}': {ex.Message}");
                return new ViewSettings<T>();
            }
        }

        public static Dictionary<string, string> SettingsKeys = new Dictionary<string, string>()
        {
            {"OrdersPage", "orders-grid-settings" },
            {"SchedulerPage", "scheduler-filter-settings" },
            {"Home-Assigned", "home-assigned-orders-grid" },
            {"Home-Taken", "home-taken-orders-grid" },
            {"Home-Department", "home-department-orders-grid" },
            {"Home-SectionStates", "home-section-collapse-states" },
            {"Home-WorkOrdersWithPerson", "home-work-orders-with-person-grid" },
            {"DevicesPage", "devices-grid-settings" },
            {"ActivitiesPage", "activities-settings" }
        };

        public async Task ClearAsync()
        {
            foreach (var key in SettingsKeys.Values)
            {
                await _localStorage.RemoveItemAsync(key);
            }
        }
    }


    public static class ViewSettingsExtensions
    {
        /// <summary>
        /// Safely gets a value from the CustomFilters dictionary, handling deserialization from JsonElement.
        /// </summary>
        public static TValue GetCustomFilter<T, TValue>(this ViewSettings<T> settings, string key, TValue defaultValue = default)
        {
            if (!settings.CustomFilters.TryGetValue(key, out var value))
            {
                return defaultValue;
            }

            if (value is JsonElement jsonElement)
            {
                try
                {
                    return JsonSerializer.Deserialize<TValue>(jsonElement.GetRawText()) ?? defaultValue;
                }
                catch
                {
                    return defaultValue;
                }
            }

            try
            {
                return (TValue)Convert.ChangeType(value, typeof(TValue));
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Sets a value in the CustomFilters dictionary.
        /// </summary>
        public static void SetCustomFilter<T, TValue>(this ViewSettings<T> settings, string key, TValue value)
        {
            settings.CustomFilters[key] = value;
        }
    }
}