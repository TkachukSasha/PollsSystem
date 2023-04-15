﻿using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Base;
using PollsSystem.Application.Commands.Validation;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;
using System.Diagnostics.CodeAnalysis;

namespace PollsSystem.Application.Commands.Polls.Holder;

public class DeleteQuestionAnswerValidator : AbstractValidator<DeleteQuestionAnswer>
{
    public DeleteQuestionAnswerValidator()
    {
        RuleFor(x => x.QuestionGid)
            .NotNull();

        RuleFor(x => x.AnswerGid)
            .NotNull();
    }
}

public record DeleteQuestionAnswer(string QuestionGid, string AnswerGid) : ICommand<bool>, IValidate
{
    public bool IsValid([NotNullWhen(false)] out ValidationError? error)
    {
        var validator = new DeleteQuestionAnswerValidator();

        var result = validator.Validate(this);

        if (result.IsValid) error = null;
        else error = new ValidationError(result.Errors.Select(x => x.ErrorMessage).ToArray());

        return result.IsValid;
    }
}

public class DeleteQuestionAnswerHandler : BaseCommandHandler<DeleteQuestionAnswer, bool>
{
    public DeleteQuestionAnswerHandler(
        IUnitOfWork unitOfWork,
        IBaseRepository baseRepository
    ) : base(unitOfWork, baseRepository) { }

    public override async ValueTask<bool> Handle(DeleteQuestionAnswer command, CancellationToken cancellationToken)
    {
        var existingAnswer = await _baseRepository.GetByConditionAsync<Answer>(x => x.Gid == Guid.Parse(command.AnswerGid) && x.QuestionGid == Guid.Parse(command.QuestionGid));

        if (existingAnswer is null)
            throw new BaseException(ExceptionCodes.ValueMissmatch,
                $"Answer with: {command.AnswerGid} is null or empty");

        _baseRepository.Delete<Answer>(existingAnswer.Gid);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
