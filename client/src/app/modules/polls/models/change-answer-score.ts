export class ChangeAnswerScore{
  questionGid: string;
  answerGid: string;
  scoreGid: string;

  constructor(
    questionGid: string,
    answerGid: string,
    scoreGid: string
  ) {
    this.questionGid = questionGid;
    this.answerGid = answerGid;
    this.scoreGid = scoreGid;
  }
}
