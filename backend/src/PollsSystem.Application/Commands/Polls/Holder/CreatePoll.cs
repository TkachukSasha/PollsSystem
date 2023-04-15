using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Base;
using PollsSystem.Application.Commands.Validation;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;
using System.Diagnostics.CodeAnalysis;

namespace PollsSystem.Application.Commands.Polls.Holder;

public record PollCreatingResponse(bool Status, string PollGid);

public class CreatePollValidator : AbstractValidator<CreatePoll>
{
    public CreatePollValidator()
    {
        RuleFor(x => x.Title)
            .MinimumLength(8)
            .MaximumLength(256)
            .NotNull();

        RuleFor(x => x.Description)
            .MinimumLength(10)
            .MaximumLength(548)
            .NotNull();

        RuleFor(x => x.NumberOfQuestions)
            .NotEqual(0)
            .NotNull();

        RuleFor(x => x.Duration)
            .NotEqual(0)
            .NotNull();

        RuleFor(x => x.AuthorGid)
            .NotNull();
    }
}

public sealed record CreatePoll(string Title, string Description, int NumberOfQuestions, int Duration, string AuthorGid) : ICommand<PollCreatingResponse>, IValidate
{
    public bool IsValid([NotNullWhen(false)] out ValidationError? error)
    {
        var validator = new CreatePollValidator();

        var result = validator.Validate(this);

        if (result.IsValid) error = null;
        else error = new ValidationError(result.Errors.Select(x => x.ErrorMessage).ToArray());

        return result.IsValid;
    }
}

public class CreatePollHandler : BaseCommandHandler<CreatePoll, PollCreatingResponse>
{
    public CreatePollHandler(
         IUnitOfWork unitOfWork,
         IBaseRepository baseRepository
    ) : base(unitOfWork, baseRepository) { }

    public override async ValueTask<PollCreatingResponse> Handle(CreatePoll command, CancellationToken cancellationToken)
    {
        var isTitleUnique = await _baseRepository.IsFieldUniqueAsync<Poll>(x => x.Title == command.Title);

        var key = Guid.NewGuid().ToString("N");

        var poll = Poll.Init(
             command.Title,
             command.Description,
             command.NumberOfQuestions,
             command.Duration,
             key,
             Guid.Parse(command.AuthorGid),
             isTitleUnique.GetValueOrDefault()
        );

        _baseRepository.Add(poll);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return !string.IsNullOrWhiteSpace(poll.Key) ? new PollCreatingResponse(true, poll.Gid.ToString()) : new PollCreatingResponse(false, "");
    }
}
