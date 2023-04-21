namespace PollsSystem.Shared.Api.Exceptions.Validation;

public sealed class ValidationException : Exception
{
    public ValidationException(ValidationError validationError) : base("Validation error") =>
        ValidationError = validationError;

    public ValidationError ValidationError { get; }
}