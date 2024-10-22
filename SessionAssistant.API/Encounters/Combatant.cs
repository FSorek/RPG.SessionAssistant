namespace SessionAssistant.API.Persistence;

public class Combatant(string name, int initiative, int attacks)
{
    public int Id { get; private set; }
    public string Name { get; private set; } = name;
    public int Initiative { get; private set; } = initiative;
    public int Attacks { get; private set; } = attacks;
    public bool HasCompletedRound { get; private set; } = false;
    public int ActPriority { get; private set; } = 0;

    public void BeginRound()
    {
        ActPriority = 0;
        HasCompletedRound = false;
    }
    public void EndTurn(bool usedMultiattack)
    {
        if (usedMultiattack && ActPriority + 1 < Attacks)
        {
            ActPriority++;
            return;
        }
        HasCompletedRound = true;
    }
}