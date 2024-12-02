namespace SessionAssistant.API.Persistence;

public class Combatant(string name, int initiative, int attacks, int? playerId)
{
    public int Id { get; private set; }
    public string Name { get; private set; } = name;
    public int Initiative { get; private set; } = initiative;
    public int Attacks { get; private set; } = attacks;
    public bool HasCompletedRound { get; private set; } = false;
    public int ActPriority { get; private set; } = 0;
    public TurnAction? UsedAction { get; private set; }
    public int? PlayerId { get; private set; } = playerId;
    public IReadOnlyCollection<Status> ActiveStatuses => _activeStatuses;

    private List<Status> _activeStatuses = [];
    
    public void BeginRound()
    {
        ActPriority = 0;
        HasCompletedRound = false;
    }

    public void BeginTurn()
    {
        for (int i = 0; i < _activeStatuses.Count; i++)
        {
            var status = _activeStatuses[i];
            status.ReduceDuration();
            if (status.Duration <= 0)
                _activeStatuses.Remove(status);
        }
        UsedAction = null;
    }
    public void EndTurn(TurnAction action)
    {
        if(HasCompletedRound)
            throw new InvalidOperationException("This combatant can not perform any actions.");
        if(UsedAction != null 
           && action != TurnAction.MultiAttack)
            throw new InvalidOperationException($"Cannot perform {action.Name}");
        if (action == TurnAction.MultiAttack
            && ActPriority + 1 < Attacks)
        {
            ActPriority++;
        }
        else if (action == TurnAction.Delay
                 && ActPriority == 0)
        {
            ActPriority = 1;
        }
        else
        {
            HasCompletedRound = true;
        }

        UsedAction = action;
    }

    public void Apply(Status status)
    {
        var existingStatus = _activeStatuses.FirstOrDefault(s => s.AbilityId == status.AbilityId);
        if(existingStatus is null)
            _activeStatuses.Add(status);
        else existingStatus.ExtendDuration(status.Duration);
    }

    public void Remove(Status status)
    {
        var existingStatus = _activeStatuses.Single(s => s.AbilityId == status.AbilityId);
        _activeStatuses.Remove(existingStatus);
    }
    public void Knockdown(int rounds)
    {
        
    }

    public void CancelTurn()
    {
        if (UsedAction == null)
            return;
        if (UsedAction == TurnAction.MultiAttack)
        {
            ActPriority--;
        }
        else if (UsedAction == TurnAction.Delay)
        {
            ActPriority = 0;
        }
        else
        {
            HasCompletedRound = false;
        }
        UsedAction = null;
        HasCompletedRound = false;
    }
}

// Combatants need to remove their active abilities on start of their first turn
// It should only happen once within a round, 