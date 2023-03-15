using PollsSystem.Application.Responses.Statistics;
using PollsSystem.Domain.Entities.Statistics;

namespace PollsSystem.Presentation.Statistics.Results;

public static class StatisticsQueryMapper
{
    public static ResultResponse ToResultResponse(this Result result)
        => new ResultResponse(
            result.Gid,
            result.Score.Value,
            result.Percents.Value,
            result.FirstName.Value,
            result.LastName.Value,
            result.PollGid
        );
}
