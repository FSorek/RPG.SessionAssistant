using Blazor.WebApp.Client.Combat;
using SessionAssistant.Shared.Users;

namespace Blazor.WebApp.Client.ApiClients;

public class SessionAssistantClient(HttpClient httpClient)
{
    public EncounterClient Encounters { get; } = new(httpClient);
    public UsersClient Users { get; } = new(httpClient);
    private readonly HttpClient _httpClient = httpClient;
}

public class UsersClient(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;
    
    public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
    {
        return new[]
        {
            new UserDTO()
            {
                Id = 1,
                DisplayName = "Sima Bols",
            },
            new UserDTO()
            {
                Id = 2,
                DisplayName = "Cedees Nawts",
            }
        };
        //return await httpClient.GetFromJsonAsync<EncounterDTO>($"api/Encounters/{id}");
    }
}