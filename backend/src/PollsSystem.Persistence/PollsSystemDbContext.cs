using Microsoft.EntityFrameworkCore;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Domain.Entities.Statistics;
using PollsSystem.Domain.Entities.Users;

namespace PollsSystem.Persistence;

public class PollsSystemDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Poll> Polls { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Score> Scores { get; set; }
    public DbSet<Result> Results { get; set; }

    public PollsSystemDbContext(DbContextOptions<PollsSystemDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
        => builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
}