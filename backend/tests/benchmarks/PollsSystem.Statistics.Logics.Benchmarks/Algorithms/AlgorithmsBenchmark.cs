using BenchmarkDotNet.Attributes;
using PollsSystem.Utils.Algorithms;

namespace PollsSystem.Statistics.Logics.Benchmarks.Algorithms;

public class AlgorithmsBenchmark
{
    private readonly List<Question> Questions = new List<Question>()
    {
        new Question(Guid.NewGuid(), "FirstQuestion", new List<Answer>
        {
            new Answer(Guid.NewGuid(), "FirstAnswer"),
            new Answer(Guid.NewGuid(), "SecondAnswer"),
            new Answer(Guid.NewGuid(), "ThirdAnswer"),
        }),
        new Question(Guid.NewGuid(), "SecondQuestion", new List<Answer>
        {
            new Answer(Guid.NewGuid(), "FirstAnswer"),
            new Answer(Guid.NewGuid(), "SecondAnswer"),
            new Answer(Guid.NewGuid(), "ThirdAnswer"),
        }),
        new Question(Guid.NewGuid(), "ThirdQuestion", new List<Answer>
        {
            new Answer(Guid.NewGuid(), "FirstAnswer"),
            new Answer(Guid.NewGuid(), "SecondAnswer"),
            new Answer(Guid.NewGuid(), "ThirdAnswer"),
        }),
        new Question(Guid.NewGuid(), "FourthQuestion", new List<Answer>
        {
            new Answer(Guid.NewGuid(), "FirstAnswer"),
            new Answer(Guid.NewGuid(), "SecondAnswer"),
            new Answer(Guid.NewGuid(), "ThirdAnswer"),
        }),
        new Question(Guid.NewGuid(), "FifthQuestion", new List<Answer>
        {
            new Answer(Guid.NewGuid(), "FirstAnswer"),
            new Answer(Guid.NewGuid(), "SecondAnswer"),
            new Answer(Guid.NewGuid(), "ThirdAnswer"),
        }),
        new Question(Guid.NewGuid(), "FifthQuestion", new List<Answer>
        {
            new Answer(Guid.NewGuid(), "FirstAnswer"),
            new Answer(Guid.NewGuid(), "SecondAnswer"),
            new Answer(Guid.NewGuid(), "ThirdAnswer"),
        }),
        new Question(Guid.NewGuid(), "FifthQuestion", new List<Answer>
        {
            new Answer(Guid.NewGuid(), "FirstAnswer"),
            new Answer(Guid.NewGuid(), "SecondAnswer"),
            new Answer(Guid.NewGuid(), "ThirdAnswer"),
        }),
        new Question(Guid.NewGuid(), "FifthQuestion", new List<Answer>
        {
            new Answer(Guid.NewGuid(), "FirstAnswer"),
            new Answer(Guid.NewGuid(), "SecondAnswer"),
            new Answer(Guid.NewGuid(), "ThirdAnswer"),
        }),
        new Question(Guid.NewGuid(), "FifthQuestion", new List<Answer>
        {
            new Answer(Guid.NewGuid(), "FirstAnswer"),
            new Answer(Guid.NewGuid(), "SecondAnswer"),
            new Answer(Guid.NewGuid(), "ThirdAnswer"),
        }),
        new Question(Guid.NewGuid(), "FifthQuestion", new List<Answer>
        {
            new Answer(Guid.NewGuid(), "FirstAnswer"),
            new Answer(Guid.NewGuid(), "SecondAnswer"),
            new Answer(Guid.NewGuid(), "ThirdAnswer"),
        }),
        new Question(Guid.NewGuid(), "FifthQuestion", new List<Answer>
        {
            new Answer(Guid.NewGuid(), "FirstAnswer"),
            new Answer(Guid.NewGuid(), "SecondAnswer"),
            new Answer(Guid.NewGuid(), "ThirdAnswer"),
        }),
        new Question(Guid.NewGuid(), "FifthQuestion", new List<Answer>
        {
            new Answer(Guid.NewGuid(), "FirstAnswer"),
            new Answer(Guid.NewGuid(), "SecondAnswer"),
            new Answer(Guid.NewGuid(), "ThirdAnswer"),
        }),
        new Question(Guid.NewGuid(), "FifthQuestion", new List<Answer>
        {
            new Answer(Guid.NewGuid(), "FirstAnswer"),
            new Answer(Guid.NewGuid(), "SecondAnswer"),
            new Answer(Guid.NewGuid(), "ThirdAnswer"),
        }),
        new Question(Guid.NewGuid(), "FifthQuestion", new List<Answer>
        {
            new Answer(Guid.NewGuid(), "FirstAnswer"),
            new Answer(Guid.NewGuid(), "SecondAnswer"),
            new Answer(Guid.NewGuid(), "ThirdAnswer"),
        }),
        new Question(Guid.NewGuid(), "FifthQuestion", new List<Answer>
        {
            new Answer(Guid.NewGuid(), "FirstAnswer"),
            new Answer(Guid.NewGuid(), "SecondAnswer"),
            new Answer(Guid.NewGuid(), "ThirdAnswer"),
        }),
        new Question(Guid.NewGuid(), "FifthQuestion", new List<Answer>
        {
            new Answer(Guid.NewGuid(), "FirstAnswer"),
            new Answer(Guid.NewGuid(), "SecondAnswer"),
            new Answer(Guid.NewGuid(), "ThirdAnswer"),
        }),
        new Question(Guid.NewGuid(), "FifthQuestion", new List<Answer>
        {
            new Answer(Guid.NewGuid(), "FirstAnswer"),
            new Answer(Guid.NewGuid(), "SecondAnswer"),
            new Answer(Guid.NewGuid(), "ThirdAnswer"),
        }),
        new Question(Guid.NewGuid(), "FifthQuestion", new List<Answer>
        {
            new Answer(Guid.NewGuid(), "FirstAnswer"),
            new Answer(Guid.NewGuid(), "SecondAnswer"),
            new Answer(Guid.NewGuid(), "ThirdAnswer"),
        }),
        new Question(Guid.NewGuid(), "FifthQuestion", new List<Answer>
        {
            new Answer(Guid.NewGuid(), "FirstAnswer"),
            new Answer(Guid.NewGuid(), "SecondAnswer"),
            new Answer(Guid.NewGuid(), "ThirdAnswer"),
        }),
        new Question(Guid.NewGuid(), "FifthQuestion", new List<Answer>
        {
            new Answer(Guid.NewGuid(), "FirstAnswer"),
            new Answer(Guid.NewGuid(), "SecondAnswer"),
            new Answer(Guid.NewGuid(), "ThirdAnswer"),
        })
    };

    [Benchmark]
    public void DefaultAlgotihmV1()
    {
        _ = AlgorithmsEngine.ShuffleQuizQuestionsV1<Question>(Questions);
    }

    [Benchmark]
    public void BetterAlgotihmV2()
    {
        _ = AlgorithmsEngine.ShuffleQuizQuestionsV2<Question>(Questions);
    }

    [Benchmark]
    public void AlgotihmGrayerV3()
    {
        _ = AlgorithmsEngine.ShuffleQuizQuestionsV3<Question>(Questions);
    }

    [Benchmark]
    public void AlgotihmShuffledV4()
    {
        _ = AlgorithmsEngine.ShuffleQuizQuestionsV4<Question>(Questions);
    }
}

public sealed record Question(Guid Gid, string Name, List<Answer> Answers);

public sealed record Answer(Guid Gid, string Name);
