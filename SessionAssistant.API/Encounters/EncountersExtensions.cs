using SessionAssistant.API.Persistence;
using SessionAssistant.Shared.DTOs.Combat;

namespace SessionAssistant.API.Encounters;

public static class EncountersExtensions
{
    public static CombatantDTO ToDTO(this Combatant combatant) =>
        new()
        {
            Id = combatant.Id,
            Attacks = combatant.Attacks,
            Initiative = combatant.Initiative,
            Name = combatant.Name,
            ActPriority = combatant.ActPriority,
            HasCompletedRound = combatant.HasCompletedRound,
        };
    public static EncounterDTO ToDTO(this Encounter encounter) =>
        new()
        {
            Id = encounter.Id,
            CurrentRound = encounter.CurrentRound,
            ActingInitiative = encounter.ActingInitiative,
            ActingPriority = encounter.ActingPriority,
            Combatants = encounter.Combatants.Select(c => c.ToDTO())
        };
}