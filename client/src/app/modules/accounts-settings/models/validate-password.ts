export class ValidatePassword{
  userGid: string;
  password: string;

  constructor(
    userGid: string,
    password: string
  ) {
    this.userGid = userGid;
    this.password = password;
  }
}
