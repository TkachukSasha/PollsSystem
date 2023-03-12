using Microsoft.EntityFrameworkCore;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Domain.Repositories;

namespace PollsSystem.Persistence.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly PollsSystemDbContext _dbContext;

    public QuestionRepository(PollsSystemDbContext dbContext)
        => _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async ValueTask<IEnumerable<Question?>> GetPollQuestionsWithAnswersAsync(Guid pollGid)
        => await _dbContext.Questions
              .AsNoTracking()
              .Where(x => x.PollGid == pollGid)
              .Include(x => x.Answers)
              .ToArrayAsync();
}