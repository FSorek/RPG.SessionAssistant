using System.Net.Http.Json;
using SessionAssistant.Shared.DTOs.Combat;

namespace Blazor.WebApp.Client.Combat;

public class EncounterClient(HttpClient httpClient)
{
    public async Task<EncounterDTO> GetEncounterAsync(int id)
    {
        return await httpClient.GetFromJsonAsync<EncounterDTO>($"api/Encounters/{id}");
    }

    public async Task<CombatantDTO> JoinCombat(int id, CombatantDTO combatant, int userId)
    {
        var response = await httpClient.PutAsJsonAsync($"api/encounters/{id}/join",
            new
            {
                Name = combatant.Name,
                Initiative = combatant.Initiative, 
                Attacks = combatant.Attacks,
                UserId = userId
            });
        return await response.Content.ReadFromJsonAsync<CombatantDTO>();
    }
    
    public async Task<CombatantDTO?> GetCombatantForUser(int userId)
    {
        var response =  await httpClient.GetAsync($"api/combatants/{userId}");
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<CombatantDTO>();
        return null;
    }
}