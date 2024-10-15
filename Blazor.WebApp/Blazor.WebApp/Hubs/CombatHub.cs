using Microsoft.AspNetCore.SignalR;

namespace Blazor.WebApp.Hubs;

public class CombatHub : Hub
{
    private static List<(string user, string message)> _messageHistory = [];
    
    public async Task EnterCombat(int initiative)
    {
        await SendMessageToAllAsync(Context.ConnectionId, $"Joined combat with {initiative} initiative");
    }

    public override async Task OnConnectedAsync()
    {
        var messages = _messageHistory.Select(m => $"{m.user}: {m.message}").ToArray();
        await Clients.Client(Context.ConnectionId).SendAsync("LoadMessages", messages);
        await SendMessageToAllAsync(Context.ConnectionId, "Connected");
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if(exception == null)
            await SendMessageToAllAsync(Context.ConnectionId, "Disconnected (Connection closed)");
        else await SendMessageToAllAsync(Context.ConnectionId, $"Disconnected ({exception.Message})");
    }

    private async Task SendMessageToAllAsync(string user, string message)
    {
        _messageHistory.Add((user, message));
        await Clients.All.SendAsync("ReceiveMessage", Context.ConnectionId, message);
    }
}