export class DeletePollQuestion{
  pollGid: string;
  questionGid: string;

  constructor(
    pollGid: string,
    questionGid: string
  ) {
    this.pollGid = pollGid;
    this.questionGid = questionGid;
  }
}
