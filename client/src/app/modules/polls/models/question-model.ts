export interface IQuestionsWithAnswers{
  gid: string;
  questionName: string;
  answers: IAnswer[];
}

export interface IAnswer{
  gid: string;
  answerName: string;
}

export interface IQuestionsWithAnswersAndScores{
  gid: string;
  questionName: string;
  answers: IAnswerWithScores[];
}

export interface IAnswerWithScores{
  gid: string;
  answerName: string;
  scoreGid: string;
}
