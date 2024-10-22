using Microsoft.EntityFrameworkCore;
using SessionAssistant.Shared.DTOs.Combat;

namespace SessionAssistant.API.Persistence;

public class SessionAssistantWriteDbContext(DbContextOptions<SessionAssistantWriteDbContext> options) 
    : DbContext(options)
{
    public DbSet<Encounter> Encounters { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Encounter>()
            .ToTable("Encounters")
            .OwnsMany(e => e.Combatants, c =>
            {
                c.ToTable("Combatants");
                c.HasKey(cbt => cbt.Id);
            });
        base.OnModelCreating(modelBuilder);
    }
}