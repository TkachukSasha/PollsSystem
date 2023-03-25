export class SignUpRequest{
  firstName: string;
  lastName: string;
  userName: string;
  password: string;
  roleGid: string;

  constructor(firstName: string, lastName: string, userName: string, password: string) {
    this.firstName = firstName;
    this.lastName = lastName;
    this.userName = userName;
    this.password = password;
    // @ts-ignore
    this.roleGid = null;
  }
}
