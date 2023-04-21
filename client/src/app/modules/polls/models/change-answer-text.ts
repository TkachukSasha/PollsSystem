export class ChangeAnswerText{
  questionGid: string;
  answerGid: string;
  answerName: string;

  constructor(
    questionGid: string,
    answerGid: string,
    answerName: string
  ) {
    this.questionGid = questionGid;
    this.answerGid = answerGid;
    this.answerName = answerName;
  }
}
