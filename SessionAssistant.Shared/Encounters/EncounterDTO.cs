namespace SessionAssistant.Shared.DTOs.Combat;

public class EncounterDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CurrentRound { get; set; }
    public int ActingInitiative { get; set; }
    public int ActingPriority { get; set; }
    public ICollection<CombatantDTO> Combatants { get; set; }
}

public class CharacterDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Attacks { get; set; }
    public IEnumerable<AbilityDTO> Abilities { get; set; }
}

public class AbilityDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

public class CombatDTO
{
    public int Id { get; set; }
    public int CurrentRound { get; set; }
    public int ActingInitiative { get; set; }
    public int ActingPriority { get; set; }
    public IReadOnlyCollection<CombatantDTO> Combatants { get; set; }
}