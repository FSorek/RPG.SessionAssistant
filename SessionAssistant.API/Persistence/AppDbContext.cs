using Microsoft.EntityFrameworkCore;
using SessionAssistant.API.Encounters.Combats;
using SessionAssistant.Shared.DTOs.Combat;

namespace SessionAssistant.API.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) 
    : DbContext(options)
{
    public DbSet<Encounter> Encounters { get; set; }
    public DbSet<Combatant> Combatants { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Ability> Abilities { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Encounter>()
            .HasMany<Player>(e => e.Players)
            .WithMany();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        modelBuilder.Entity<Status>()
            .HasOne(s => s.Ability)
            .WithMany();

        modelBuilder.Entity<Character>()
            .HasMany<Ability>(c => c.KnownAbilities)
            .WithMany();
        
        base.OnModelCreating(modelBuilder);
    }
}