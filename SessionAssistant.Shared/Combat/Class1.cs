namespace SessionAssistant.Shared.DTOs.Combat;

public class Class1
{
}

public class CombatantDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Initiative { get; set; }
    public int Attacks { get; set; }
    public bool CanAct { get; set; }
    public IList<SkillDTO> Skills { get; set; } = new List<SkillDTO>();
}

public class SkillDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Cooldown { get; set; }
    public string Icon { get; set; }
}

public class EncounterDTO
{
    public int Id { get; set; }
    public int CurrentRound { get; set; }
    public IList<CombatantDTO> Combatants { get; set; } = [];
}