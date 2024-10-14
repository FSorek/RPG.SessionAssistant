namespace CombatAssistant;

public class CombatProcessor
{
    private readonly List<Combatant> _combatants = new List<Combatant>(); 
    public Combatant EnterCombat(string name, int initiative)
    {
        var combatant = new Combatant(name, initiative);
        _combatants.Add(combatant);
        return combatant;
    }

    public Combatant GetActivePlayer()
    {
        var combatant = _combatants
            .Where(p => p.CanAct)
            .MaxBy(p => p.Initiative);
        return combatant;
    }
}

public class Combatant
{
    public string Name { get; private set; }
    public int Attacks { get; private set; }
    public bool CanDodge { get; private set; }
    public bool CanParry { get; private set; }
    public int Initiative { get; private set; }
    public bool CanAct { get; private set; }

    public Combatant(string name, int initiative)
    {
        Name = name;
        Initiative = initiative;
        CanAct = true;
    }

    public void EndTurn()
    {
        CanAct = false;
    }
}