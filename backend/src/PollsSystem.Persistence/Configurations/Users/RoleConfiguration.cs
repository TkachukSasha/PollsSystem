using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PollsSystem.Domain.Entities.Users;

namespace PollsSystem.Persistence.Configurations.Users;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Gid);

        builder.Property(x => x.Name).IsRequired();
    }
}