namespace SessionAssistant.API.Persistence;

public class Ability
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Category { get; private set; }

    public Status CreateStatus(int duration)
    {
        return new Status(Id, duration);
    }
}