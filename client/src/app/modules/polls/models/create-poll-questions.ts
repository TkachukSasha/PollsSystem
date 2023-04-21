export class CreatePollQuestions{
  pollGid: string;
  questions: Question[];

  constructor(
    pollGid: string,
    questions: Question[]
  ) {
    this.pollGid = pollGid;
    this.questions = questions;
  }
}

export class Question{
  questionText: string;
  answers: Answer[];

  constructor(
    questionText: string,
    answers: Answer[]
  ) {
    this.questionText = questionText;
    this.answers = answers;
  }
}

export class Answer{
  answerText: string;
  scoreGid: string;

  constructor(
    answerText: string,
    scoreGid: string
  ) {
    this.answerText = answerText;
    this.scoreGid = scoreGid;
  }
}
