export interface IQuestionsWithAnswers{
  gid: string;
  questionName: string;
  answers: IAnswer[];
}

export interface IAnswer{
  gid: string;
  answerName: string;
}
