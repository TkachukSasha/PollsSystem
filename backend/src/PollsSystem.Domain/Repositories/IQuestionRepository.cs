using PollsSystem.Domain.Entities.Polls;

namespace PollsSystem.Domain.Repositories;

public interface IQuestionRepository
{
    ValueTask<IEnumerable<Question?>> GetPollQuestionsWithAnswersAsync(Guid pollGid);
}