using Microsoft.EntityFrameworkCore;
using SessionAssistant.Shared.DTOs.Combat;

namespace SessionAssistant.API.Persistence;

public class SessionAssistantReadDbContext(DbContextOptions<SessionAssistantReadDbContext> options) 
    : DbContext(options)
{
    public DbSet<EncounterDTO> Encounters { get; set; }
    public DbSet<CombatantDTO> Combatants { get; set; }
    public DbSet<SkillDTO> Skills { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EncounterDTO>()
            .HasMany(e => e.Combatants)
            .WithOne()
            .HasForeignKey("EncounterId")
            .IsRequired();
        modelBuilder.Entity<EncounterDTO>()
            .ToTable("Encounters")
            .HasData(
                new EncounterDTO(){Id = 1, CurrentRound = 1, ActingInitiative = 100, ActingPriority = 0}, 
                new EncounterDTO(){Id = 2, CurrentRound = 1, ActingInitiative = 100, ActingPriority = 0});
        modelBuilder.Entity<CombatantDTO>()
            .ToTable("Combatants")
            .HasKey(c => c.Id);
        modelBuilder.Entity<CombatantDTO>()
            .HasMany(c => c.Skills)
            .WithMany("Combatants");
        modelBuilder.Entity<SkillDTO>()
            .ToTable("Skills")
            .HasKey(c => c.Id);

        base.OnModelCreating(modelBuilder);
    }
}