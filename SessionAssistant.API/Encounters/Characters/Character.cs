namespace SessionAssistant.API.Persistence;

public class Character(string name, int attacks)
{
    public int Id { get; private set; }
    public string Name { get; private set; } = name;
    public int Attacks { get; private set; } = attacks;
    public IReadOnlyCollection<Ability> KnownAbilities => _knownAbilities;
    private readonly List<Ability> _knownAbilities = new List<Ability>();
    public void Learn(Ability ability)
    {
        if (_knownAbilities.Contains(ability))
            return;
        _knownAbilities.Add(ability);
    }
}