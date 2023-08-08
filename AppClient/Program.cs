global using Newtonsoft.Json.Converters;
global using Newtonsoft.Json;
global using System.Net;
global using Microsoft.AspNetCore.Components.Authorization;
global using MyAppClient.Services;
global using System.Security.Claims;
global using Shared.DataTransferModels.User;
global using System.Net.NetworkInformation;
global using System.Threading;
global using Shared;
global using Blazored.LocalStorage;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using Blazored.Toast;
global using Microsoft.AspNetCore.Components.Web;
global using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
global using MyAppClient.Helpers;
global using MyAppClient;
global using MyAppClient.Shared.Components.Modals;
global using Shared.DataTransferModels.Todo;
using MyAppClient.Services.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5130/api/") });
builder.Services.AddScoped<ApiCallService>();
builder.Services.AddScoped<AccountService>();

builder.Services.AddBlazoredLocalStorage(config =>
{
    config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
    config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
    config.JsonSerializerOptions.WriteIndented = false;
});

builder.Services.AddScoped<TestService>();
builder.Services.AddScoped<StorageService>();
builder.Services.AddBlazoredToast();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddSingleton<SidebarService>();
builder.Services.AddScoped<TodoService>();
builder.Services.AddScoped<ConfirmationModalService>();

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

await builder.Build().RunAsync();
