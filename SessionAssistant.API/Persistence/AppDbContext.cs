using Microsoft.EntityFrameworkCore;
using SessionAssistant.Shared.DTOs.Combat;

namespace SessionAssistant.API.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) 
    : DbContext(options)
{
    public DbSet<Encounter> Encounters { get; set; }
    public DbSet<Combatant> Combatants { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Encounter>()
            .HasMany(e => e.Combatants)
            .WithOne()
            .HasForeignKey("EncounterId")
            .IsRequired();
        modelBuilder.Entity<Encounter>()
            .ToTable("Encounters")
            .HasData(
                new EncounterDTO(){Id = 1, CurrentRound = 1, ActingInitiative = 100, ActingPriority = 0}, 
                new EncounterDTO(){Id = 2, CurrentRound = 1, ActingInitiative = 100, ActingPriority = 0});
        modelBuilder.Entity<Combatant>()
            .ToTable("Combatants")
            .HasKey(c => c.Id);
        // modelBuilder.Entity<Combatant>()
        //     .HasMany(c => c.Skills)
        //     .WithMany("Combatants");
        // modelBuilder.Entity<Skill>()
        //     .ToTable("Skills")
        //     .HasKey(c => c.Id);

        base.OnModelCreating(modelBuilder);
    }
}