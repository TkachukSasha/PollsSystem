import { Injectable } from '@angular/core';
import { HttpService } from "../../../core/services/http/http-service";
import { map, Observable } from "rxjs";
import { ApiMethod } from "../../../core/enums/api-methods";
import { ValidatePassword } from "../models/validate-password";
import { ChangePassword } from "../models/change-password";
import { ChangeUsername } from "../models/change-username";

@Injectable({
  providedIn: 'root'
})
export class AccountSettingsService {

  constructor(
    private _http: HttpService
  ) { }

  getUserName() : Observable<string>{
    // @ts-ignore
    return this._http.requestCall<string>('/accounts/username', ApiMethod.GET)
      .pipe(
        map((data: string) => {
          return data;
        })
      )
  }

  changeUserName(payload: ChangeUsername) : Observable<any>{
    let request = JSON.stringify(payload);

    // @ts-ignore
    return this._http.requestCall<any>('/accounts/change-username', ApiMethod.POST, request)
      .pipe(
        map((data: any) => {
          return data;
        })
      )
  }

  verifyPassword(payload: ValidatePassword) : Observable<boolean>{
    let request = JSON.stringify(payload);

    // @ts-ignore
    return this._http.requestCall<boolean>('/accounts/validate-password', ApiMethod.POST, request)
      .pipe(
        map((data: boolean) => {
          return data;
        })
      )
  }

  changePassword(payload: ChangePassword) : Observable<any>{
    let request = JSON.stringify(payload);

    // @ts-ignore
    return this._http.requestCall<any>('/accounts/change-password', ApiMethod.POST, request)
      .pipe(
        map((data: any) => {
          return data;
        })
      )
  }
}
