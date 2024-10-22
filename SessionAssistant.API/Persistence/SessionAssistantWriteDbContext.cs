using Microsoft.EntityFrameworkCore;
using SessionAssistant.Shared.DTOs.Combat;

namespace SessionAssistant.API.Persistence;

public class SessionAssistantWriteDbContext(DbContextOptions<SessionAssistantWriteDbContext> options) 
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Encounter>()
            .OwnsMany(e => e.Combatants, c =>
            {
                c.ToTable("Combatants");
                c.HasKey(cbt => cbt.Id);
                c.OwnsMany(cbt => cbt.Skills, s =>
                {
                    s.ToTable("Skills");
                    s.HasKey(sk => sk.Id);
                });
            })
            .HasData(
                new EncounterDTO(){Id=1, Combatants = new List<CombatantDTO>(), CurrentRound = 1},
                new EncounterDTO(){Id=2, Combatants = new List<CombatantDTO>(), CurrentRound = 1}
            );
        base.OnModelCreating(modelBuilder);
    }
}

public class Encounter
{
    public int Id { get; private set; }
    public int CurrentRound { get; private set; } = 1;
    public IReadOnlyCollection<Combatant> Combatants => _combatants;

    private List<Combatant> _combatants = new List<Combatant>();
    private int _currentPriority;
    private int _currentInitiative;

    public Combatant EnterCombat(string name, int initiative, int attacks)
    {
        var newCombatant = new Combatant(Id, name, initiative, attacks);
        _combatants.Add(newCombatant);
        if (newCombatant.Initiative == _currentInitiative 
            && newCombatant.ActPriority == _currentPriority)
        {
            newCombatant.BeginTurn();
        } else if (newCombatant.Initiative > _currentInitiative || newCombatant.ActPriority < _currentPriority)
        {
            var actingCombatants = _combatants.FindAll(c => c.CanAct);
            foreach (var actingCombatant in actingCombatants)
            {
                actingCombatant.EndTurn(false);
            }
            
            _currentInitiative = newCombatant.Initiative;
            _currentPriority = newCombatant.ActPriority;
            newCombatant.BeginTurn();
        }
        return newCombatant;
    }

    public void LeaveCombat()
    {
        
    }

    public void EndTurn(int combatantId, bool usedMultiattack = false)
    {
        var combatant = _combatants.Find(c => c.Id == combatantId);
        if (combatant == null)
            throw new Exception("Combatant not found");
        combatant.EndTurn(usedMultiattack);
        if(_combatants.Any(c => c.CanAct))
            return;
        var remainingCombatants = _combatants
            .Where(c => c.Initiative < _currentInitiative && c.ActPriority == _currentPriority)
            .ToArray();
        if (remainingCombatants.Length == 0)
        {
            _currentPriority++;
            var nextPriorityCombatants = _combatants
                .Where(c => c.ActPriority >= _currentPriority)
                .ToArray();
            if (nextPriorityCombatants.Length == 0)
            {
                CurrentRound++;
                _currentPriority = 0;
                _currentInitiative = _combatants.MaxBy(c => c.Initiative)!.Initiative;
            }
            else
            {
                _currentPriority = nextPriorityCombatants.Min(c => c.ActPriority);
                _currentInitiative = nextPriorityCombatants.Max(c => c.Initiative);
                nextPriorityCombatants = nextPriorityCombatants.Where(c =>
                    c.Initiative == _currentInitiative && _currentPriority == c.ActPriority)
                    .ToArray();
                foreach (var nextCombatant in nextPriorityCombatants)
                {
                    nextCombatant.BeginTurn();
                }
            }
        }
        else
        {
            _currentInitiative = remainingCombatants.Max(c => c.Initiative);
            foreach (var nextCombatant in remainingCombatants.Where(c => c.Initiative == _currentInitiative))
            {
                nextCombatant.BeginTurn();
            }
        }
    }
}

public class Combatant(int encounterId, string name, int initiative, int attacks)
{
    public int Id { get; private set; }
    public int EncounterId { get; private set; } = encounterId;
    public string Name { get; private set; } = name;
    public int Initiative { get; private set; } = initiative;
    public int Attacks { get; private set; } = attacks;
    public bool CanAct { get; private set; }
    public int ActPriority { get; private set; }
    public IReadOnlyCollection<SkillDTO> Skills { get; set; }

    public void EndTurn(bool usedMultiattack)
    {
        if(!CanAct)
            throw new Exception($"{Name} cannot act right now");
        if (usedMultiattack && ActPriority + 1 < Attacks)
            ActPriority++;
        CanAct = false;
    }

    public void BeginTurn()
    {
        //Handle statuses
        CanAct = true;
    }

    public void RevertTurn(int currentInitiative, int currentPriority)
    {
        if (currentInitiative > Initiative)
            return; // My turn not happened yet, return
        if (currentInitiative == Initiative && currentPriority == ActPriority)
        {
            //my turn is ongoing
            //revert applied statuses
            CanAct = false;
        }
    }
}