namespace SessionAssistant.Shared.DTOs.Combat;

public class CombatantDTO
{
    public int Id { get; set; }
    public int EncounterId { get; set; }
    public string Name { get; set; }
    public int Initiative { get; set; }
    public int Attacks { get; set; }
    public bool HasCompletedRound { get; set; }
    public int ActPriority { get; set; }
    public IList<SkillDTO> Skills { get; set; } = new List<SkillDTO>();
}