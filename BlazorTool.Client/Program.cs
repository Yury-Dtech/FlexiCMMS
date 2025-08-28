using Blazored.LocalStorage;
using BlazorTool.Client.Models;
using BlazorTool.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddTelerikBlazor();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddLocalization();

builder.Services.AddScoped<AuthHeaderHandler>(sp =>
{
    var userState = sp.GetRequiredService<UserState>();
    return new AuthHeaderHandler(userState);
});


var serverBaseUrl = builder.Configuration["InternalApiBaseUrl"] ?? builder.HostEnvironment.BaseAddress;

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

builder.Services.AddScoped<ApiServiceClient>(sp =>
    new ApiServiceClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("ServerApi"), 
    sp.GetRequiredService<UserState>()));

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

await host.RunAsync();

public static class AppInfo
{
    public static string Version { get; } = ThisAssembly.AssemblyInformationalVersion;
    public static string Name = "flexiCMMS";
    public static string BuildDate { get; } = ThisAssembly.GitCommitDate.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) + " UTC";
}
