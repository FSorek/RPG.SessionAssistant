using System.Net.Http.Json;
using SessionAssistant.Shared.DTOs.Combat;

namespace Blazor.WebApp.Client.Combat;

public class EncounterClient(HttpClient httpClient)
{
    public async Task<EncounterDTO> GetEncounterAsync(int id)
    {
        return await httpClient.GetFromJsonAsync<EncounterDTO>($"api/Encounters/{id}");
    }

    public async Task<CombatantDTO> JoinCombat(int id, CombatantDTO combatant)
    {
        var response = await httpClient.PutAsJsonAsync($"api/encounters/{id}/join", combatant);
        return await response.Content.ReadFromJsonAsync<CombatantDTO>();
    }
}