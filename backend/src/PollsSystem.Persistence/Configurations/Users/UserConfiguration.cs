using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PollsSystem.Domain.Entities.Users;
using PollsSystem.Domain.ValueObjects.Users;

namespace PollsSystem.Persistence.Configurations.Users;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Gid);

        builder.HasIndex(x => x.UserName);

        builder.Property(x => x.FirstName)
               .HasConversion(x => x!.Value, x => InputName.Init(x))
               .HasMaxLength(50);

        builder.Property(x => x.LastName)
               .HasConversion(x => x!.Value, x => InputName.Init(x))
               .HasMaxLength(50);

        builder.Property(x => x.UserName)
               .HasConversion(x => x!.Value, x => InputName.Init(x))
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(x => x.Password)
               .HasMaxLength(150)
               .IsRequired();

        builder.HasOne(x => x.Role)
               .WithMany(x => x.Users)
               .HasForeignKey(x => x.RoleGid);
    }
}