export class ChangeResultScore{
  pollGid: string;
  lastName: string;
  score: number;

  constructor(
    pollGid: string,
    lastName: string,
    score: number
  ) {
    this.pollGid = pollGid;
    this.lastName = lastName;
    this.score = score;
  }
}
