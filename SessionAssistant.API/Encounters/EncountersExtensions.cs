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
            Name = encounter.Name,
            ActingInitiative = encounter.Combat.ActingInitiative,
            ActingPriority = encounter.Combat.ActingPriority,
            CurrentRound = encounter.Combat.CurrentRound,
            Combatants = encounter.Combat.Combatants.Select(c => c.ToDTO()).ToArray(),
        };
    public static CharacterDTO ToDTO(this Character character) =>
        new()
        {
            Id = character.Id,
            Name = character.Name,
            Attacks = character.Attacks,
            Abilities = character.KnownAbilities.Select(a => a.ToDTO())
        };
    public static AbilityDTO ToDTO(this Ability ability) =>
        new()
        {
            Id = ability.Id,
            Name = ability.Name,
            Description = ability.Description
        };
}