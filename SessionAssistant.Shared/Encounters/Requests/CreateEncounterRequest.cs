namespace SessionAssistant.Shared.DTOs.Combat.Requests;

public record CreateEncounterRequest(string Name);
public record JoinEncounterRequest(int PlayerID);