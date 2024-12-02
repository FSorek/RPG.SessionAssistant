using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SessionAssistant.API.Persistence;

namespace SessionAssistant.API.Encounters.Combats;

public class CombatantEFConfiguration : IEntityTypeConfiguration<Combatant>
{
    public void Configure(EntityTypeBuilder<Combatant> builder)
    {
        builder.HasMany<Status>()
            .WithOne();
        builder.ComplexProperty(c => c.UsedAction);
    }
}