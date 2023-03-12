using Mediator;
using System.Diagnostics.CodeAnalysis;

namespace PollsSystem.Application.Commands.Validation;

public interface IValidate : IMessage
{
    bool IsValid([NotNullWhen(false)] out ValidationError? error);
}