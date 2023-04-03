export class CheckPollKey{
  pollGid: string;
  key: string;

  constructor(
    pollGid: string,
    key: string
  ) {
    this.pollGid = pollGid;
    this.key = key;
  }
}
