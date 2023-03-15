namespace PollsSystem.Utils.Algorithms;

public static class AlgorithmsEngine
{
    public static List<TData> ShuffleQuizQuestionsV1<TData>(this List<TData> array)
    {
        Random random = new Random();
        List<TData> retArray = new List<TData>(array);

        int maxIndex = array.Count - 1;

        for (int i = 0; i <= maxIndex; i++)
        {
            int swapIndex = random.Next(i, maxIndex);

            if (swapIndex != i)
            {
                TData temp = retArray[i];
                retArray[i] = retArray[swapIndex];
                retArray[swapIndex] = temp;
            }
        }
        return retArray;
    }

    public static List<TData> ShuffleQuizQuestionsV2<TData>(List<TData> questions)
    {
        Random random = new Random();

        for (int i = questions.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            TData temp = questions[i];
            questions[i] = questions[j];
            questions[j] = temp;
        }

        return questions;
    }

    public static List<TData> ShuffleQuizQuestionsV3<TData>(List<TData> questions)
    {
        List<TData> shuffled = new List<TData>();
        List<int> indices = Enumerable.Range(0, questions.Count).ToList();
        Random random = new Random();

        for (int i = 0; i < questions.Count; i++)
        {
            int index = random.Next(0, indices.Count);
            TData question = questions[indices[index]];
            indices.RemoveAt(index);
            shuffled.Add(question);
        }

        return shuffled;
    }
}
