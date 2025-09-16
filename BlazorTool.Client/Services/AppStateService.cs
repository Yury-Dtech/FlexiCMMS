using BlazorTool.Client.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BlazorTool.Client.Services
{
    public class AppStateService
    {
        private readonly IServiceProvider _serviceProvider;
        
        public ApplicationSettings Settings { get; private set; }
        public bool IsInitialized { get; private set; } = false;

        public event Action OnChange;

        public AppStateService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task InitializeAsync()
        {
            if (IsInitialized)
            {
                return;
            }

            try
            {
                // Создаем временный scope для получения Scoped сервисов
                await using var scope = _serviceProvider.CreateAsyncScope();
                var apiClient = scope.ServiceProvider.GetRequiredService<ApiServiceClient>();

                // Запускаем все запросы на получение настроек параллельно
                var nameTask = apiClient.LoadSettingAsync("AppName", string.Empty);
                var apiUrlTask = apiClient.LoadSettingAsync("ExternalApi:BaseUrl", string.Empty);

                // Ожидаем завершения всех запросов
                await Task.WhenAll(nameTask, apiUrlTask);
                var version = ThisAssembly.AssemblyInformationalVersion.Split('+').FirstOrDefault() ?? "";
                // Собираем результаты в один объект
                Settings = new ApplicationSettings
                {
                    ApplicationName = await nameTask,
                    ApiBaseUrl = await apiUrlTask,
                    Version = version
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Critical error: Failed to load application settings. {ex.Message}");
                // В случае ошибки, устанавливаем значения по умолчанию, чтобы не блокировать UI
                Settings = new ApplicationSettings
                {
                    ApplicationName = "BlazorTool (Offline)",
                    ApiBaseUrl = "",
                };
            }
            finally
            {
                IsInitialized = true;
            }
        }
    }
}
