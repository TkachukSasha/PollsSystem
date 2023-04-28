export interface IQuestionsWithAnswers{
  gid: string;
  questionName: string;
  answers: IAnswer[];
}

export interface IAnswer{
  gid: string;
  answerText: string;
}

export interface IQuestionsWithAnswersAndScores{
  gid: string;
  questionName: string;
  answers: IAnswerWithScores[];
}

export interface IAnswerWithScores{
  gid: string;
  answerText: string;
  scoreGid: string;
}
