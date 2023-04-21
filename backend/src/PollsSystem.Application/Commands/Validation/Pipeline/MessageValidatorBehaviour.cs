using FluentValidation;
using Mediator;
using PollsSystem.Shared.Api.Exceptions.Validation;
using ValidationException = PollsSystem.Shared.Api.Exceptions.Validation.ValidationException;

namespace PollsSystem.Application.Commands.Validation.Pipeline;

public class MessageValidatorBehaviour<TMessage, TResponse> : IPipelineBehavior<TMessage, TResponse> where TMessage : class, ICommand<TResponse>
{
    private readonly IEnumerable<IValidator<TMessage>>? _validators = null;

    public MessageValidatorBehaviour(IEnumerable<IValidator<TMessage>> validators) => _validators = validators;

    public async ValueTask<TResponse> Handle(TMessage request, CancellationToken cancellationToken, MessageHandlerDelegate<TMessage, TResponse> next)
    {
        if (!_validators.Any())
        {
            return await next(request, cancellationToken);
        }

        var context = new ValidationContext<TMessage>(request);

        var validationErrors = _validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .GroupBy(
                x => x.PropertyName,
                x => $"Error with code: {x.ErrorCode} and message: {x.ErrorMessage}",
                (propertyName, errorMessages) => new
                {
                    Key = propertyName,
                    Values = errorMessages.Distinct().ToArray()
                })
            .ToDictionary(x => x.Key, x => x.Values);

        if (validationErrors.Any())
            throw new ValidationException(new ValidationError(validationErrors));

        return await next(request, cancellationToken);
    }
}
