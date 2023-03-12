using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PollsSystem.Domain.Entities.Statistics;
using PollsSystem.Domain.ValueObjects.Polls;
using PollsSystem.Domain.ValueObjects.Statistics;
using PollsSystem.Domain.ValueObjects.Users;

namespace PollsSystem.Persistence.Configurations.Statistics;

internal sealed class ResultConfiguration : IEntityTypeConfiguration<Result>
{
    public void Configure(EntityTypeBuilder<Result> builder)
    {
        builder.HasKey(x => x.Gid);

        builder.Property(x => x.Score)
               .HasConversion(x => x!.Value, x => ScoreValue.Init(x))
               .IsRequired();

        builder.Property(x => x.Percents)
               .HasConversion(x => x!.Value, x => Percents.Init(x))
               .IsRequired();

        builder.Property(x => x.FirstName)
               .HasConversion(x => x!.Value, x => InputName.Init(x))
               .IsRequired();

        builder.Property(x => x.LastName)
               .HasConversion(x => x!.Value, x => InputName.Init(x))
               .IsRequired();
    }
}