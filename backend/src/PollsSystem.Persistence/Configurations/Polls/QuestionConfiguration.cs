using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Domain.ValueObjects.Polls;

namespace PollsSystem.Persistence.Configurations.Polls;

internal class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasKey(x => x.Gid);

        builder.Property(x => x.QuestionName)
               .HasConversion(x => x.Value, x => Text.Init(x))
               .HasMaxLength(250)
               .IsRequired();

        builder.HasOne(x => x.Poll)
               .WithMany(x => x.Questions)
               .HasForeignKey(x => x.PollGid)
               .OnDelete(DeleteBehavior.Cascade);
    }
}