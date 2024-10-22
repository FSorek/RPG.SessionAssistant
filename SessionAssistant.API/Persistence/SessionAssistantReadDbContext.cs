using Microsoft.EntityFrameworkCore;
using SessionAssistant.Shared.DTOs.Combat;

namespace SessionAssistant.API.Persistence;

public class SessionAssistantReadDbContext(DbContextOptions<SessionAssistantReadDbContext> options) 
    : DbContext(options)
{
    public DbSet<EncounterDTO> Encounters { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EncounterDTO>()
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