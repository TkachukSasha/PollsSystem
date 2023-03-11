using System.Net;

namespace PollsSystem.Shared.Api.Exceptions;

internal sealed record ExceptionResponse(object Response, HttpStatusCode StatusCode);