using BlazorTool.Client.Models;
using BlazorTool.Client.Services;
using BlazorTool.Components;
using BlazorTool.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);

// Store data protection keys in a relative folder named 'keys'
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("keys"))
    .SetApplicationName("BlazorTool");

builder.Services.AddMemoryCache();
var ApiAddress = new Uri(builder.Configuration["ExternalApi:BaseUrl"]!);
Console.WriteLine("External Api BaseAddress: " + ApiAddress);
builder.Services.AddHttpClient("ExternalApiBasicAuthClient", client =>
{
    // BaseAddress EXTERNAL API, where get token
    client.BaseAddress = ApiAddress;
});

builder.Services.AddHttpClient("ExternalApiBearerAuthClient", client =>
{
    client.BaseAddress = ApiAddress;
})
.AddHttpMessageHandler<ServerAuthHeaderHandler>();

builder.Services.AddHttpContextAccessor(); 
// inject IHttpClientFactory & IMemoryCache
builder.Services.AddScoped<ServerAuthTokenService>(sp =>
{
    return new ServerAuthTokenService(
        sp.GetRequiredService<IMemoryCache>(),
        sp.GetRequiredService<IHttpContextAccessor>()
    );
});

builder.Services.AddScoped<ServerAuthHeaderHandler>();

builder.Services.AddScoped<UserState>();

string? internalApiBaseUrl = null;

internalApiBaseUrl = getLocalServerAddress(builder);
//Console.WriteLine("Internal Api BaseUrl: " + internalApiBaseUrl);
//internal controllers Blazor Host
builder.Services.AddHttpClient("InternalApiClient", client =>
{
    client.BaseAddress = new Uri(internalApiBaseUrl);
});

builder.Services.AddScoped<ApiServiceClient>(sp =>
    new ApiServiceClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("InternalApiClient"),
    sp.GetRequiredService<UserState>(),
    sp.GetRequiredService<ILogger<ApiServiceClient>>()));


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddControllers();
builder.Services.AddTelerikBlazor();
builder.Services.AddScoped<AppointmentService>();
builder.Logging.AddConsole();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    Console.WriteLine("======= APPLICATION (Server) STARTING in Development mode...");
}
else
{
    Console.WriteLine("======= APPLICATION (Server) STARTING in Production mode...");
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorTool.Client._Imports).Assembly);

app.MapControllers();
Console.WriteLine("======= APPLICATION (Server) app.Run();");
app.Run();

static string getLocalServerAddress(WebApplicationBuilder builder)
{
    string? internalApiBaseUrl;
    if (builder.Environment.IsDevelopment())
    {
        // ASPNETCORE_URLS "https://localhost:7282;http://localhost:5168")
        var urls = builder.Configuration["ASPNETCORE_URLS"]?.Split(';', StringSplitOptions.RemoveEmptyEntries);
        // 1st URL, "http://"
        internalApiBaseUrl = urls?.FirstOrDefault(url => url.StartsWith("http://"));

        if (string.IsNullOrEmpty(internalApiBaseUrl))
        {
            internalApiBaseUrl = urls?.FirstOrDefault();
        }

        if (string.IsNullOrEmpty(internalApiBaseUrl))
        {
            internalApiBaseUrl = "http://localhost:5168"; //default fallback URL
            Console.WriteLine("======= SERVER: ASPNETCORE_URLS is empty or invalid, falling back to default http://localhost:5168");
        }

    }
    else //prodaction or other environment
    {
        internalApiBaseUrl = "http://localhost/api/v1/"; //TODO: set your production URL here
        //internalApiBaseUrl = builder.Configuration["HostAddress"] ?? throw new InvalidOperationException("HostAddress not configured for non-development environment.");
    }
    //builder.Configuration["InternalApiBaseUrl"] = internalApiBaseUrl; // Store for later use if needed
    return internalApiBaseUrl;
}