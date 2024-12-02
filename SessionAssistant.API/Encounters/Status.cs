namespace SessionAssistant.API.Persistence;

public class Status(int abilityId, int duration)
{
    public int Id { get; private set; }
    public int AbilityId { get; private set; } = abilityId;
    public int Duration { get; private set; } = duration;

    public void ExtendDuration(int duration)
    {
        Duration += duration;
    }

    public void ReduceDuration()
    {
        Duration--;
    }

    public Ability Ability { get; private set; }
}