import { Injectable } from '@angular/core';
import { HttpService } from "../../../core/services/http/http-service";
import { map, Observable} from "rxjs";
import { CheckPollKey } from "../models/check-poll-key";
import { ApiMethod } from "../../../core/enums/api-methods";

@Injectable({
  providedIn: 'root'
})
export class PollsService {

  constructor(
    private _http: HttpService
  ) { }

  // @ts-ignore
  checkPollKey(payload: CheckPollKey) : Observable<boolean>{
      // @ts-ignore
    return  this._http.requestCall<boolean>(`/polls/check-key?PollGid=${payload?.pollGid}&Key=${payload?.key}`, ApiMethod.GET)
        .pipe(
          map((data: boolean) => {
            return data;
          })
        )
  }
}
