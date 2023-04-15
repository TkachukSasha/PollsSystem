export class CreatePoll{
  title: string;
  description: string;
  numberOfQuestions: number;
  duration: number;
  authorGid: string;

  constructor(
    title: string,
    description: string,
    numberOfQuestions: number,
    duration: number,
    authorGid: string
  ) {
    this.title = title;
    this.description = description;
    this.numberOfQuestions = numberOfQuestions;
    this.duration = duration;
    this.authorGid = authorGid;
  }
}
