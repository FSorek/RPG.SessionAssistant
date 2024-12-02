namespace SessionAssistant.Shared.DTOs.Combat.Requests;

public record CreateCharacterRequest(string Name, int Attacks);
public record LearnAbilityRequest(int AbilityId);