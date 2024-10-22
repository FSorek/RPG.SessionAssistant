namespace SessionAssistant.API.Persistence;

public class Encounter
{
    public int Id { get; private set; }
    public int CurrentRound { get; private set; } = 1;
    public int ActingInitiative { get; private set; } = 100;
    public int ActingPriority { get; private set; } = 0;
    public IReadOnlyCollection<Combatant> Combatants => _combatants;
    private readonly List<Combatant> _combatants = [];

    public Combatant EnterCombat(string name, int initiative, int attacks)
    {
        var newCombatant = new Combatant(name, initiative, attacks);
        _combatants.Add(newCombatant);
        CalculateActingValues();
        return newCombatant;
    }

    private void CalculateActingValues()
    {
        ActingInitiative = _combatants
            .Where(c => !c.HasCompletedRound)
            .Max(c => c.Initiative);
        ActingPriority = _combatants
            .Where(c => !c.HasCompletedRound)
            .Min(c => c.ActPriority);
    }
    public void EndTurn(int combatantId, bool usedMultiattack = false)
    {
        var combatant = _combatants.SingleOrDefault(c => c.Id == combatantId);
        if (combatant == null)
            throw new ArgumentException("Invalid combatant id");
        if (combatant.HasCompletedRound 
            || combatant.ActPriority != ActingPriority 
            || combatant.Initiative != ActingInitiative)
            throw new InvalidOperationException("This combatant cannot act right now");
        combatant.EndTurn(usedMultiattack);
        if (_combatants.All(c => c.HasCompletedRound))
        {
            NextRound();
        }
        CalculateActingValues();
    }

    private void NextRound()
    {
        CurrentRound++;
        foreach (var combatant in Combatants)
        {
            combatant.BeginRound();
        }
    }
}