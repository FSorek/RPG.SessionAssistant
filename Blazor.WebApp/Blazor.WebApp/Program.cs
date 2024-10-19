using Blazor.WebApp.Client.Combat;
using Blazor.WebApp.Client.Pages;
using Blazor.WebApp.Components;
using Microsoft.AspNetCore.ResponseCompression;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddMudServices();
builder.Services.AddHttpClient<CombatClient>(client => client.BaseAddress = new Uri("http://localhost:5039"));
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddResponseCompression(options => 
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(["application/octet-stream"]));
builder.Services.AddCors(
    options => options.AddPolicy(
        "server",
        policy => policy.WithOrigins("http://localhost:5039")
            .AllowAnyMethod()
            .AllowAnyHeader()));
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseResponseCompression();
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseCors("server");
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Blazor.WebApp.Client._Imports).Assembly);

app.Run();