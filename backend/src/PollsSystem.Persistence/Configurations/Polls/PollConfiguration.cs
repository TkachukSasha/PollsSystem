using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Domain.ValueObjects.Polls;

namespace PollsSystem.Persistence.Configurations.Polls;

internal class PollConfiguration : IEntityTypeConfiguration<Poll>
{
    public void Configure(EntityTypeBuilder<Poll> builder)
    {
        builder.HasKey(x => x.Gid);

        builder.Property(x => x.Title)
               .HasConversion(x => x.Value, x => Text.Init(x))
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(x => x.Description)
               .HasConversion(x => x.Value, x => Text.Init(x))
               .HasMaxLength(250)
               .IsRequired();

        builder.Property(x => x.NumberOfQuestions)
               .HasConversion(x => x.Value, x => NumberOfQuestions.Init(x))
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(x => x.Duration)
               .HasConversion(x => x.Value, x => Duration.Init(x))
               .HasMaxLength(240)
               .IsRequired();

        builder.Property(x => x.Key)
               .HasConversion(x => x.Value, x => Key.Init(x))
               .IsRequired();

        builder.HasOne(x => x.User)
                .WithMany(x => x.Polls)
                .HasForeignKey(x => x.AuthorGid);
    }
}