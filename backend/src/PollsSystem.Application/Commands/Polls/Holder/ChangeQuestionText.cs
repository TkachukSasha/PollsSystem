using FluentValidation;
using Mediator;
using PollsSystem.Application.Commands.Validation;
using PollsSystem.Domain.Entities.Polls;
using PollsSystem.Shared.Api.Exceptions;
using PollsSystem.Shared.Dal.Repositories;
using PollsSystem.Shared.Dal.Utils;
using System.Diagnostics.CodeAnalysis;

namespace PollsSystem.Application.Commands.Polls.Holder;

public class ChangeQuestionTextValidator : AbstractValidator<ChangeQuestionText>
{
	public ChangeQuestionTextValidator()
	{
		RuleFor(x => x.CurrentQuestion)
			.NotNull();

		RuleFor(x => x.Question)
			.MinimumLength(6)
			.MaximumLength(50)
			.NotNull();
	}
}

public sealed record ChangeQuestionText(string CurrentQuestion, string Question) : ICommand<Guid>, IValidate
{
	public bool IsValid([NotNullWhen(false)] out ValidationError? error)
	{
		var validator = new ChangeQuestionTextValidator();

		var result = validator.Validate(this);

		if (result.IsValid) error = null;
		else error = new ValidationError(result.Errors.Select(x => x.ErrorMessage).ToArray());

		return result.IsValid;
	}
}

public class ChangeQuestionTextHandler : ICommandHandler<ChangeQuestionText, Guid>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IBaseRepository _baseRepository;

	public ChangeQuestionTextHandler(
		IUnitOfWork unitOfWork,
		IBaseRepository baseRepository)
	{
		_unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		_baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));
	}

	public async ValueTask<Guid> Handle(ChangeQuestionText command, CancellationToken cancellationToken)
	{
		var existingQuestion = await _baseRepository.GetByConditionAsync<Question>(x => x.QuestionName == command.CurrentQuestion);

		if (existingQuestion is null)
			throw new BaseException(ExceptionCodes.ValueIsNullOrEmpty,
				$"Question with: {command.CurrentQuestion} is null!");

		if (existingQuestion.QuestionName == command.Question)
			throw new BaseException(ExceptionCodes.ValueAlreadyExist,
				$"Answer with: {command.Question} is already exist!");

		bool? isQuestionNameUnique = await _baseRepository.IsFieldUniqueAsync<Question>(x => x.QuestionName == command.Question);

		var answer = Question.ChangeQuestionName(
			existingQuestion,
			command.Question,
			isQuestionNameUnique.GetValueOrDefault()
		);

		_baseRepository.Update(answer);

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return answer.Gid;
	}
}
