namespace CombatAssistant;

public class Player
{
    public string Name { get; private set; }
    public Player(string name)
    {
        Name = name;
    }
}

public class CombatProcessor
{
    private readonly List<Combatant> _combatants = new List<Combatant>(); 
    public Combatant EnterCombat(Player player, int initiative)
    {
        var combatant = new Combatant(player, initiative);
        _combatants.Add(combatant);
        return combatant;
    }

    public Player GetActivePlayer()
    {
        var combatant = _combatants
            .Where(p => p.CanAct)
            .MaxBy(p => p.Initiative);
        return combatant.Player;
    }
}

public class Combatant
{
    public Player Player { get; private set; }
    public int Initiative { get; private set; }
    public bool CanAct { get; private set; }

    public Combatant(Player player, int initiative)
    {
        Player = player;
        Initiative = initiative;
        CanAct = true;
    }

    public void EndTurn()
    {
        CanAct = false;
    }
}