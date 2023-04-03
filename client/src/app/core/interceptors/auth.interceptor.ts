import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StorageService } from "../services/storage/storage-service";
import { HttpService } from "../services/http/http-service";
import { IAuthResponse } from "../../modules/accounts/models/auth-response";
import { ApiMethod } from "../enums/api-methods";
import { map } from "rxjs/operators";
import * as jwtDecode from 'jwt-decode';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(
    private _http: HttpService,
    private _storage: StorageService
  ) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    // @ts-ignore
    const data = JSON.parse(this._storage.getData('auth'));

    let userGid = data?.userGid;

    let access_token = data?.accessToken;

    if(access_token){
      let req = request.clone({
        // @ts-ignore
        headers: request.headers.append('Authorization', `Bearer ${access_token}`)
      })

      return next.handle(req);
    }

    return next.handle(request);
  }
}
