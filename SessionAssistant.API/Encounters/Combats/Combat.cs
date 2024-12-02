namespace SessionAssistant.API.Persistence;

public class Combat
{
    public int Id { get; private set; }
    public int EncounterId { get; private set; }
    public int CurrentRound { get; private set; } = 1;
    public int ActingInitiative { get; private set; } = 100;
    public int ActingPriority { get; private set; } = 0;
    public IReadOnlyCollection<Combatant> Combatants => _combatants;

    private List<Combatant> _combatants = [];

    public Combatant Enter(int playerId, Character character, int initiative)
    {
        var combatant = new Combatant(character.Name, initiative, character.Attacks, playerId);
        _combatants.Add(combatant);

        if (combatant.ActPriority < ActingPriority)
        {
            ActingPriority = combatant.ActPriority;
            ActingInitiative = combatant.Initiative;
        }
        else if (combatant.ActPriority == ActingPriority
                 && combatant.Initiative >= ActingInitiative)
        {
            if (combatant.Initiative > ActingInitiative)
            {
                foreach (var activeCombatant in GetActiveCombatants())
                {
                    activeCombatant.CancelTurn();
                }

                ActingInitiative = combatant.Initiative;
            }

            combatant.BeginTurn();
        }

        return combatant;
    }

    public void EndTurn(int combatantId, TurnAction action)
    {
        var combatant = _combatants.SingleOrDefault(c => c.Id == combatantId);
        if (combatant == null)
            throw new ArgumentException("Invalid combatant id");
        if (combatant.HasCompletedRound
            || combatant.ActPriority != ActingPriority
            || combatant.Initiative != ActingInitiative)
            throw new InvalidOperationException("This combatant cannot act right now");

        combatant.EndTurn(action);
        if (_combatants.All(c => c.HasCompletedRound))
        {
            NextRound();
        }
        else
        {
            ActingPriority = _combatants
                .Where(c => !c.HasCompletedRound)
                .Min(c => c.ActPriority);
            ActingInitiative = _combatants
                .Where(c => !c.HasCompletedRound
                            && c.ActPriority == ActingPriority)
                .Max(c => c.Initiative);
            foreach (var activeCombatant in GetActiveCombatants())
            {
                activeCombatant.BeginTurn();
            }
        }
    }

    private void NextRound()
    {
        CurrentRound++;
        foreach (var combatant in Combatants)
        {
            combatant.BeginRound();
        }
        foreach (var activeCombatant in GetActiveCombatants())
        {
            activeCombatant.BeginTurn();
        }
        ActingPriority = 0;
        ActingInitiative = _combatants.Max(c => c.Initiative);
    }

    private IEnumerable<Combatant> GetActiveCombatants() => _combatants
        .Where(c => c.Initiative == ActingInitiative
                    && c.ActPriority == ActingPriority)
        .ToArray();
}