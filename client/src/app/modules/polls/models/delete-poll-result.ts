export class DeletePollResult{
  pollGid: string;
  resultGid: string;

  constructor(
    pollGid: string,
    resultGid: string
  ) {
    this.pollGid = pollGid;
    this.resultGid = resultGid;
  }
}
