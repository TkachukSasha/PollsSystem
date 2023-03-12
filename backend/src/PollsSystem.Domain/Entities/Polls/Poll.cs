using PollsSystem.Domain.Entities.Users;
using PollsSystem.Domain.ValueObjects.Polls;
using PollsSystem.Shared.Abstractions.Primitives;
using PollsSystem.Shared.Api.Exceptions;

namespace PollsSystem.Domain.Entities.Polls;

public class Poll : Entity
{
    private List<Question> _questions = new();

    public Poll(Text title,
                Text description,
                NumberOfQuestions numberOfQuestions,
                Duration duration,
                Key key,
                Guid authorGid) : base()
    {
        Title = title;
        Description = description;
        NumberOfQuestions = numberOfQuestions;
        Duration = duration;
        Key = key;
        AuthorGid = authorGid;
    }

    public Text Title { get; private set; }
    public Text Description { get; private set; }
    public NumberOfQuestions NumberOfQuestions { get; private set; }
    public Key Key { get; private set; }
    public Duration Duration { get; private set; }
    public Guid AuthorGid { get; private set; }

    public User User { get; set; }
    public ICollection<Question> Questions => _questions;

    public static Poll Init(
        string titleRequest,
        string descriptionRequest,
        int numberOfQuestionsRequest,
        int durationRequest,
        string keyRequest,
        Guid authorGid,
        bool? isTitleUnique)
    {
        if (!isTitleUnique.GetValueOrDefault())
            throw new BaseException(ExceptionCodes.ValueAlreadyExist,
                $"Title: {titleRequest} already exist!");

        var title = Text.Init(titleRequest);
        var description = Text.Init(descriptionRequest);
        var numberOfQuestions = NumberOfQuestions.Init(numberOfQuestionsRequest);
        var duration = Duration.Init(durationRequest);
        var key = Key.Init(keyRequest);

        var poll = new Poll(title,
                            description,
                            numberOfQuestions,
                            duration,
                            key,
                            authorGid);

        return poll;
    }

    public static Poll ChangePollTitle(
        Poll poll,
        string titleRequest,
        bool? isTitleUnique)
    {
        if (!isTitleUnique.GetValueOrDefault())
            throw new BaseException(ExceptionCodes.ValueAlreadyExist,
                $"Title: {titleRequest} already exist!");

        var title = Text.Init(titleRequest);

        poll.Title = title;

        return poll;
    }

    public static Poll ChangePollDescription(
        Poll poll,
        string descriptionRequest,
        bool? isDescriptionUnique)
    {
        if (!isDescriptionUnique.GetValueOrDefault())
            throw new BaseException(ExceptionCodes.ValueAlreadyExist,
                $"Description: {descriptionRequest} already exist!");

        var description = Text.Init(descriptionRequest);

        poll.Description = description;

        return poll;
    }

    public static Poll ChangePollDuration(
        Poll poll,
        int durationRequest)
    {
        if (poll.Duration == durationRequest)
            throw new BaseException(ExceptionCodes.ValueMissmatch,
                $"Duration: {durationRequest} missmatch!");

        var duration = Duration.Init(durationRequest);

        poll.Duration = duration;

        return poll;
    }

    public static Poll RegenerateKey(
        Poll poll,
        string keyRequest)
    {
        if (poll.Key.Equals(keyRequest))
            return poll;

        var key = Key.Init(keyRequest);

        poll.Key = key;

        return poll;
    }
}