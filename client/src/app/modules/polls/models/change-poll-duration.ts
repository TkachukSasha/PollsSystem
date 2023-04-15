export class ChangePollDuration{
  pollGid: string;
  duration: number;

  constructor(
    pollGid: string,
    duration: number
  ) {
    this.pollGid = pollGid;
    this.duration = duration;
  }
}
