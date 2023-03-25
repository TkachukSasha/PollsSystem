import { Injectable } from '@angular/core';
import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StorageService } from "../services/storage/storage-service";
import { HttpService } from "../services/http/http-service";
import { IAuthResponse } from "../../modules/accounts/models/auth-response";
import { ApiMethod } from "../enums/api-methods";
import { catchError, map } from "rxjs/operators";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(
    private _http: HttpService,
    private _storage: StorageService
  ) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    // @ts-ignore
    const data = JSON.parse(this._storage.getData('auth'));

    if(data?.accessToken){
      console.log(`data from interceptor: ${data?.accessToken}`)

      let req = request.clone({
        headers: request.headers.append('Authorization', `Bearer ${data?.accessToken}`)
      })

      return next.handle(req);
    }

    // @ts-ignore
    return next.handle(request).pipe(catchError(error => {
      if(error instanceof  HttpErrorResponse && error.status === 401 || error.status === 403){
        // @ts-ignore
        let revokeToken = this._http.requestCall<IAuthResponse>('/accounts/revoke-token', ApiMethod.POST, {userGid})
          .pipe(
            map((data) => {
              this._storage.clearData('auth');
              this._storage.storeData('auth', JSON.stringify(data));
            })
          )

        // @ts-ignore
        if(JSON.parse(revokeToken)){
          console.log(`revokeToken: ${revokeToken}`)

          let req = request.clone({
            // @ts-ignore
            headers: request.headers.append('Authorization', `Bearer ${revokeToken?.accessToken}`)
          })

          return next.handle(req);
        }
      }
    }));
  }
}
