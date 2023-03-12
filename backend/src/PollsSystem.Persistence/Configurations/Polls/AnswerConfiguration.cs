using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Domain.ValueObjects.Polls;

namespace PollsSystem.Persistence.Configurations.Polls;

internal class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.HasKey(x => x.Gid);

        builder.Property(x => x.AnswerText)
               .HasConversion(x => x.Value, x => Text.Init(x))
               .HasMaxLength(250)
               .IsRequired();

        builder.HasOne(x => x.Question)
               .WithMany(x => x.Answers)
               .HasForeignKey(x => x.QuestionGid);
    }
}