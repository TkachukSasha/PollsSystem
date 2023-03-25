export class ChangeUsername{
  currentUserName: string;
  userName: string;

  constructor(
    currentUserName: string,
    userName: string
  ) {
    this.currentUserName = currentUserName;
    this.userName = userName;
  }
}
