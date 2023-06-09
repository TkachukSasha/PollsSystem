import { Injectable } from "@angular/core";
import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { ErrorService } from "../error/error-service";
import {Observable, throwError} from "rxjs";
import { catchError } from "rxjs/operators"
import { environment } from "../../../../environments/environment";
import { ApiMethod } from "../../enums/api-methods";

@Injectable({
  providedIn: 'root'
})
export class HttpService{
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(
    private http: HttpClient,
    private errorService: ErrorService
  ) {
  }

  requestCall(endpoint: string, method: ApiMethod, data?: any) : Observable<any>{
    let response;

    switch (method) {
      case ApiMethod.GET:
        response = this.http.get(`${environment.apiUrl}${endpoint}`)
          .pipe(catchError(async (err) => this.handleError(err, this)));
        break;
      case ApiMethod.POST:
        response = this.http.post(`${environment.apiUrl}${endpoint}`, data, this.httpOptions)
          .pipe(catchError(async (err) => this.handleError(err, this)));
        break;
      case ApiMethod.PUT:
        response = this.http.put(`${environment.apiUrl}${endpoint}`, data, this.httpOptions)
          .pipe(catchError(async (err) => this.handleError(err, this)));
        break;
      case ApiMethod.PATCH:
        response = this.http.patch(`${environment.apiUrl}${endpoint}`, data, this.httpOptions)
          .pipe(catchError(async (err) => this.handleError(err, this)));
        break;
      case ApiMethod.DELETE:
        response = this.http.delete(`${environment.apiUrl}${endpoint}`, {
          headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
          body: data
        })
          .pipe(catchError(async (err) => this.handleError(err, this)));
        break;
      default:
        break;
    }

    // @ts-ignore
    return response;
  }

  handleError(error: HttpErrorResponse, self: any){
    this.errorService.displayError((error.message));
    return throwError({error: error.message, status: error.status});
  }
}
