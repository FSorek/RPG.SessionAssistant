using Microsoft.EntityFrameworkCore;
using SessionAssistant.Shared.DTOs.Combat;

namespace SessionAssistant.API.Persistence;

public class SessionAssistantDbContext(DbContextOptions<SessionAssistantDbContext> options) 
    : DbContext(options)
{
    public DbSet<Character> Characters { get; set; }
    public DbSet<EncounterDTO> Encounters { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Character>().HasData(
            new {Id=1, Name="Gerlach Bauer", CanParry = true, CanDodge = true},
            new {Id=2, Name="Roborbor", CanParry = true, CanDodge = false},
            new {Id=3, Name="Pan Robak", CanParry = false, CanDodge = false},
            new {Id=4, Name="Chad Poggington", CanParry = true, CanDodge = true}
            );
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlite("Data Source=SessionAssistant.db");
        base.OnConfiguring(optionsBuilder);
    }
}

public class Character
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool CanParry { get; set; }
    public bool CanDodge { get; set; }
}

public class CombatLog
{
    public int Id { get; set; }
    
}