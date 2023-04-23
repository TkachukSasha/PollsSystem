using Mediator;
using PollsSystem.Application.Common.Enums;
using PollsSystem.Application.Common.Services;
using PollsSystem.Application.Responses.Statistics;
using PollsSystem.Domain.Entities.Statistics;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;

namespace PollsSystem.Application.Queries.Statistics.Results;

public record GetCalculations(StatisticType Type, string PollGid) : IQuery<Calculations>;

public class GetCalculationsHandler : IQueryHandler<GetCalculations, Calculations?>
{
    private readonly IBaseRepository _baseRepository;
    private readonly IStatisticCalculationService _calculationService;

    public GetCalculationsHandler(
        IBaseRepository baseRepository,
        IStatisticCalculationService calculationService)
    {
        _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));
        _calculationService = calculationService ?? throw new ArgumentNullException(nameof(calculationService));
    }

    public async ValueTask<Calculations?> Handle(GetCalculations query, CancellationToken cancellationToken)
    {
        List<double> _scores = new();

        var results = await _baseRepository.GetEntitiesByConditionAsync<Result>(x => x.PollGid == Guid.Parse(query.PollGid));

        if (!results.Any() || results is null)
            throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
                $"Results with: {query.PollGid} is null!");

        foreach (var item in results)
            _scores.Add(item.Score);

        var pollScores = _scores.ToArray();

        if (results.ToArray().Length <= 1)
            return null;

        var calculations = _calculationService.Calculate(query.Type, pollScores);

        return calculations;
    }
}