using System.Net.Http.Json;
using SessionAssistant.Shared.DTOs.Combat;
using SessionAssistant.Shared.DTOs.Combat.Requests;

namespace Blazor.WebApp.Client.Combat;

public class EncounterClient(HttpClient httpClient)
{
    public async Task<EncounterDTO> GetEncounterAsync(int id)
    {
        return await httpClient.GetFromJsonAsync<EncounterDTO>($"api/Encounters/{id}");
    }

    public async Task<CombatantDTO> CreateEncounterCombatant(int encounterId, CombatantDTO combatant, int userId)
    {
        var requestBody = new CreateCombatantRequest(combatant.Name, combatant.Initiative, combatant.Attacks, userId);
        var response = await httpClient.PostAsJsonAsync($"api/encounters/{encounterId}/combatants", requestBody);
        return await response.Content.ReadFromJsonAsync<CombatantDTO>();
    }
    
    public async Task<CombatantDTO?> GetCombatantForUser(int encounterId, int userId)
    {
        var response =  await httpClient.GetAsync($"api/encounters/{encounterId}/combatants?userId={userId}");
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<CombatantDTO>();
        return null;
    }
}