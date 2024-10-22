using Blazor.WebApp.Client.Combat;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddMudServices();
builder.Services.AddHttpClient<EncounterClient>(client => client.BaseAddress = new Uri("http://localhost:5039"));

await builder.Build().RunAsync();