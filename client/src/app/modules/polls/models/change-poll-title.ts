export class ChangePollTitle{
  pollGid: string;
  title: string;

  constructor(
    pollGid: string,
    title: string
  ) {
    this.pollGid = pollGid;
    this.title = title;
  }
}
