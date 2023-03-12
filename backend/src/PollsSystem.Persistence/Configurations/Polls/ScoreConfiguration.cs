using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Domain.ValueObjects.Polls;

namespace PollsSystem.Persistence.Configurations.Polls;

internal class ScoreConfiguration : IEntityTypeConfiguration<Score>
{
    public void Configure(EntityTypeBuilder<Score> builder)
    {
        builder.HasKey(x => x.Gid);

        builder.Property(x => x.ScoreValue)
               .HasConversion(x => x.Value, x => ScoreValue.Init(x))
               .IsRequired();
    }
}