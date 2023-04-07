export class SendReplies{
  pollGid: string;
  firstName: string;
  lastName: string;
  questionAnswer: { [key: string]: string };

  constructor(
    pollGid: string,
    firstName: string,
    lastName: string,
    questionAnswer: Map<string, string>
  ) {
    this.pollGid = pollGid;
    this.firstName = firstName;
    this.lastName = lastName;
    this.questionAnswer = Object.fromEntries(questionAnswer.entries());
  }
}
