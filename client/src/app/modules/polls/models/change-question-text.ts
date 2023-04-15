export class ChangeQuestionText{
  questionGid: string;
  question: string;

  constructor(
    questionGid: string,
    question: string
  ) {
    this.questionGid = questionGid;
    this.question = question;
  }
}
