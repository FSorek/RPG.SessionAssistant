namespace SessionAssistant.API.Persistence;

public class Encounter(string name)
{
    public int Id { get; private set; }
    public string Name { get; private set; } = name;
    public IReadOnlyCollection<Player> Players => _players;
    private readonly List<Player> _players = new List<Player>();
    public Combat Combat { get; private set; } = new Combat();
    public void Join(Player player)
    {
        if(!_players.Contains(player))
            _players.Add(player);
    }
}