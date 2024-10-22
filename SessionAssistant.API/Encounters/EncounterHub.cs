using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR;
using SessionAssistant.Shared.DTOs.Combat;

namespace Blazor.WebApp.Hubs;

public class EncounterHub : Hub<IEncounterClient>
{
    private static List<(string user, string message)> _messageHistory = [];
    
    public async Task EnterCombat(int combatantId)
    {
        await Clients.All.UpdateEncounter();
        await Clients.All.ReceiveMessage($"{combatantId} joined the combat");
    }

    public override async Task OnConnectedAsync()
    {
        var feature = Context.Features.Get<IHttpConnectionFeature>();
        string user = feature?.RemoteIpAddress?.ToString() ?? "IP Error";
        var messages = _messageHistory.Select(m => $"{m.user}: {m.message}").ToArray();
        await Clients.Client(Context.ConnectionId).LoadMessages(messages);
        await SendMessageToAllAsync(user, "Connected");
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var feature = Context.Features.Get<IHttpConnectionFeature>();
        string user = feature?.RemoteIpAddress?.ToString() ?? "IP Error";
        if(exception == null)
            await SendMessageToAllAsync(user, "Disconnected (Connection closed)");
        else await SendMessageToAllAsync(user, $"Disconnected ({exception.Message})");
    }

    private async Task SendMessageToAllAsync(string user, string message)
    {
        _messageHistory.Add((user, message));
        await Clients.All.ReceiveMessage($"({user}) {message}");
    }
}

public interface IEncounterClient
{
    Task UpdateEncounter();
    Task ReceiveMessage(string message);
    Task LoadMessages(string[] messages);
}