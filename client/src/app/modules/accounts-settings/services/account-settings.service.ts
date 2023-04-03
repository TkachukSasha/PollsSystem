import { Injectable } from '@angular/core';
import { HttpService } from "../../../core/services/http/http-service";
import { map, Observable } from "rxjs";
import { ApiMethod } from "../../../core/enums/api-methods";
import { ValidatePassword } from "../models/validate-password";
import { ChangePassword } from "../models/change-password";
import { ChangeUsername } from "../models/change-username";
import {DeleteAccount} from "../models/delete-account";

@Injectable({
  providedIn: 'root'
})
export class AccountSettingsService {

  constructor(
    private _http: HttpService
  ) { }

  getUserName(userGid: string) : Observable<any>{
    // @ts-ignore
    return this._http.requestCall<any>(`/accounts/username?UserGid=${userGid}`, ApiMethod.GET)
      .pipe(
        map((data: any) => {
          return data;
        })
      )
  }

  changeUserName(payload: ChangeUsername) : Observable<boolean>{
    let request = JSON.stringify(payload);

    // @ts-ignore
    return this._http.requestCall<any>('/accounts/change-username', ApiMethod.PATCH, request)
      .pipe(
        map((data: any) => {
          return data;
        })
      )
  }

  verifyPassword(payload: ValidatePassword) : Observable<boolean>{
    let request = JSON.stringify(payload);

    // @ts-ignore
    return this._http.requestCall<boolean>('/accounts/validate-password', ApiMethod.PATCH, request)
      .pipe(
        map((data: boolean) => {
          return data;
        })
      )
  }

  changePassword(payload: ChangePassword) : Observable<boolean>{
    let request = JSON.stringify(payload);

    // @ts-ignore
    return this._http.requestCall<any>('/accounts/change-password', ApiMethod.PATCH, request)
      .pipe(
        map((data: any) => {
          return data;
        })
      )
  }

  deleteAccount(payload: DeleteAccount) : Observable<boolean>{
    let request = JSON.stringify(payload);

    // @ts-ignore
    return this._http.requestCall<any>('/accounts/delete', ApiMethod.DELETE, request)
      .pipe(
        map((data: any) => {
          return data;
        })
      )
  }
}
