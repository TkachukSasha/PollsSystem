export class DeleteQuestionAnswer{
  questionGid: string;
  answerGid: string;

  constructor(
    questionGid: string,
    answerGid: string
  ) {
    this.questionGid = questionGid;
    this.answerGid = answerGid;
  }
}
