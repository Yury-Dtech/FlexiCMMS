using Blazored.LocalStorage;
using BlazorTool.Client.Models;
using BlazorTool.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Globalization;
using Telerik.Blazor.Services;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddTelerikBlazor();
builder.Services.AddSingleton<ITelerikStringLocalizer, CustomTelerikLocalizer>();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddLocalization();

builder.Services.AddScoped<AuthHeaderHandler>(sp =>
{
    var userState = sp.GetRequiredService<UserState>();
    return new AuthHeaderHandler(userState);
});


var serverBaseUrl = builder.HostEnvironment.BaseAddress;

builder.Services.AddHttpClient("ServerHost", client =>
{
    // BaseAddress for the server, where the Blazor app is hosted
        var uriBuilder = new UriBuilder(serverBaseUrl);
        uriBuilder.Path = string.Empty; // Remove the path part
        uriBuilder.Query = string.Empty; // Remove the query part

    client.BaseAddress = uriBuilder.Uri;
})
.AddHttpMessageHandler<AuthHeaderHandler>();
//TODO get AbsolutePath from ExternalApiBaseUrl
if (!serverBaseUrl.Contains("/api"))
{
    serverBaseUrl += "api/v1/";
}

builder.Services.AddHttpClient("ServerApi", client =>
{    
    client.BaseAddress = new Uri(serverBaseUrl);
})
.AddHttpMessageHandler<AuthHeaderHandler>();

// Weather API client (no auth header handler needed)
builder.Services.AddHttpClient("WeatherApi", client =>
{
    client.BaseAddress = new Uri("https://api.weatherapi.com/v1/");
});

builder.Services.AddScoped<ApiServiceClient>(sp =>
    new ApiServiceClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("ServerApi"), 
    sp.GetRequiredService<UserState>(),
    sp.GetRequiredService<ILogger<ApiServiceClient>>()));

builder.Services.AddScoped<AppointmentService>();

builder.Services.AddScoped(sp => {
    return new HttpClient { BaseAddress = new Uri(serverBaseUrl) };
});

builder.Services.AddScoped<UserState>();
builder.Services.AddScoped<ViewSettingsService>();

builder.Services.AddSingleton<AppStateService>();

Console.WriteLine("======= APPLICATION (Client) STARTING...");

var host = builder.Build();

var userState = host.Services.GetRequiredService<UserState>();
await userState.InitializationTask;

var currentCulture = new CultureInfo(userState.LangCode ?? "en-US");
CultureInfo.DefaultThreadCurrentCulture = currentCulture;
CultureInfo.DefaultThreadCurrentUICulture = currentCulture;

await host.RunAsync();

public static class AppInfo
{
    public static string Version { get; } = ThisAssembly.AssemblyInformationalVersion;
    public static string Name = "flexiCMMS";
    public static string BuildDate { get; } = ThisAssembly.GitCommitDate.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) + " UTC";
}
