export class ChangePassword{
  userGid: string;
  currentPassword: string;
  password: string;

  constructor(
    userGid: string,
    currentPassword: string,
    password: string
  ) {
    this.userGid = userGid;
    this.currentPassword = currentPassword;
    this.password = password;
  }
}
