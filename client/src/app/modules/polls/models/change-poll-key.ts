export class ChangePollKey{
  pollGid: string;
  currentKey: string;

  constructor(
    pollGid: string,
    currentKey: string
  ) {
    this.pollGid = pollGid;
    this.currentKey = currentKey;
  }
}
