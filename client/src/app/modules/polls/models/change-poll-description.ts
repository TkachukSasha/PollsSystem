export class ChangePollDescription{
  pollGid: string;
  description: string;

  constructor(
    pollGid: string,
    description: string
  ) {
    this.pollGid = pollGid;
    this.description = description;
  }
}
