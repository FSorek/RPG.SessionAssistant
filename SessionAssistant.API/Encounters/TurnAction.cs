namespace SessionAssistant.API.Persistence;

public record TurnAction
{
    public string Name { get; init; }
    public static readonly TurnAction End = new(nameof(End));
    public static readonly TurnAction MultiAttack = new(nameof(MultiAttack));
    public static readonly TurnAction Delay = new(nameof(Delay));

    private TurnAction(string name)
    {
        Name = name;
    }
}