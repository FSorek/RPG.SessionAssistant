namespace SessionAssistant.Shared.DTOs.Combat;

public class EncounterDTO
{
    public int Id { get; set; }
    public int CurrentRound { get; set; } 
    public int ActingInitiative { get; set; }
    public int ActingPriority { get; set; }
    public IList<CombatantDTO> Combatants { get; set; } = [];
}