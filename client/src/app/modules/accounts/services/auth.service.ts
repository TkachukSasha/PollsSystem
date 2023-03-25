import { Injectable } from '@angular/core';
import { HttpService } from "../../../core/services/http/http-service";
import { IAuthResponse } from "../models/auth-response";
import { ApiMethod } from "../../../core/enums/api-methods";
import { SignInRequest } from "../models/sign-in-request";
import {map, Observable} from 'rxjs';
import {SignUpRequest} from "../models/sign-up-request";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(
    private _http: HttpService
  ) { }

  // @ts-ignore
  signIn(payload: SignInRequest): Observable<any>{
    let request = JSON.stringify(payload);

    // @ts-ignore
    return this._http.requestCall<IAuthResponse>('/accounts/sign-in', ApiMethod.POST, request)
      .pipe(
        map((data: any) => {
          return data;
        })
      );
  }

  // @ts-ignore
  signUp(payload: SignUpRequest) : Observable<any>{
    let request = JSON.stringify(payload);

    // @ts-ignore
    return this._http.requestCall<IAuthResponse>('/accounts/sign-up', ApiMethod.POST, request)
      .pipe(
        map((data: any) => {
          return data;
        })
      );
  }
}
