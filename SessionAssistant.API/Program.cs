using System.Text.Json.Serialization;
using Blazor.WebApp.Hubs;
using Microsoft.EntityFrameworkCore;
using SessionAssistant.API.Persistence;
using SessionAssistant.Shared.DTOs.Combat;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SessionAssistantReadDbContext>(opt =>
{
    opt.UseSqlite(string.Format("Filename={0}/SessionAssistant.db", AppDomain.CurrentDomain.BaseDirectory));
});
builder.Services.AddDbContext<SessionAssistantWriteDbContext>(opt =>
{
    opt.UseSqlite(string.Format("Filename={0}/SessionAssistant.db", AppDomain.CurrentDomain.BaseDirectory));
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors(
    options => options.AddDefaultPolicy(
        policy => policy.WithOrigins(["http://localhost:5039", 
                "http://localhost:5255"])
            .AllowAnyMethod()
            .AllowAnyHeader()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapHub<EncounterHub>("/encounterhub");
app.MapControllers();
app.Run();