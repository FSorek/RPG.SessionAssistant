namespace SessionAssistant.Shared.DTOs.Combat.Requests;
public record CreateCombatantRequest(string Name, int Initiative, int Attacks, int UserId);