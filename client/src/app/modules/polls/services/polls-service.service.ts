import { Injectable } from '@angular/core';
import { HttpService } from "../../../core/services/http/http-service";
import {map, Observable, tap} from "rxjs";
import { CheckPollKey } from "../models/check-poll-key";
import { ApiMethod } from "../../../core/enums/api-methods";
import {SendReplies} from "../models/send-replies";
import {DeletePoll} from "../models/delete-poll";
import {CreatePoll} from "../models/create-poll";
import {ChangePollTitle} from "../models/change-poll-title";
import {ChangePollDescription} from "../models/change-poll-description";
import {ChangePollDuration} from "../models/change-poll-duration";
import {ChangePollKey} from "../models/change-poll-key";
import {ICreatePollResponse} from "../models/create-poll-response";
import {ChangeQuestionText} from "../models/change-question-text";
import {DeletePollQuestion} from "../models/delete-poll-question";
import {IQuestionsWithAnswers} from "../models/question-model";

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

  // @ts-ignore
  sendReplies(payload: SendReplies) : Observable<boolean>{
    let request = JSON.stringify(payload);

    // @ts-ignore
    return  this._http.requestCall<boolean>(`/external/send-replies`, ApiMethod.POST, request)
      .pipe(
        map((data: boolean) => {
          return data;
        })
      )
  }

  deletePoll(payload: DeletePoll) : Observable<boolean>{
    let request = JSON.stringify(payload);

    // @ts-ignore
    return  this._http.requestCall<boolean>(`/holders/delete-poll`, ApiMethod.DELETE, request)
      .pipe(
        map((data: boolean) => {
          return data;
        })
      )
  }

  createPoll(payload: CreatePoll) : Observable<ICreatePollResponse>{
    let request = JSON.stringify(payload);

    // @ts-ignore
    return  this._http.requestCall<boolean>(`/holders/create-poll`, ApiMethod.POST, request)
      .pipe(
        map((data: ICreatePollResponse) => {
          return data;
        })
      )
  }

  changePollTitle(payload: ChangePollTitle) : Observable<boolean>{
    let request = JSON.stringify(payload);

    // @ts-ignore
    return  this._http.requestCall<boolean>(`/holders/change-poll-title`, ApiMethod.PATCH, request)
      .pipe(
        map((data: boolean) => {
          return data;
        })
      )
  }

  changePollDescription(payload: ChangePollDescription) : Observable<boolean>{
    let request = JSON.stringify(payload);

    // @ts-ignore
    return  this._http.requestCall<boolean>(`/holders/change-poll-description`, ApiMethod.PATCH, request)
      .pipe(
        map((data: boolean) => {
          return data;
        })
      )
  }

  changePollDuration(payload: ChangePollDuration) : Observable<boolean>{
    let request = JSON.stringify(payload);

    // @ts-ignore
    return  this._http.requestCall<boolean>(`/holders/change-poll-duration`, ApiMethod.PATCH, request)
      .pipe(
        map((data: boolean) => {
          return data;
        })
      )
  }

  changePollKey(payload: ChangePollKey): Observable<boolean>{
    let request = JSON.stringify(payload);

    // @ts-ignore
    return  this._http.requestCall<boolean>(`/holders/change-poll-key`, ApiMethod.PATCH, request)
      .pipe(
        map((data: boolean) => {
          return data;
        })
      )
  }

  changeQuestionText(payload: ChangeQuestionText) : Observable<boolean>{
    let request = JSON.stringify(payload);

    // @ts-ignore
    return  this._http.requestCall<boolean>(`/holders/change-question-text`, ApiMethod.PATCH, request)
      .pipe(
        map((data: boolean) => {
          return data;
        })
      )
  }

  deletePollQuestion(payload: DeletePollQuestion) : Observable<boolean>{
    let request = JSON.stringify(payload);

    // @ts-ignore
    return  this._http.requestCall<boolean>(`/holders/delete-poll-question`, ApiMethod.DELETE, request)
      .pipe(
        map((data: boolean) => {
          return data;
        })
      )
  }

  getPollQuestions(pollGid: string) : Observable<IQuestionsWithAnswers[]>{
    // @ts-ignore
    return this._http.requestCall<IQuestionsWithAnswers[]>(`/polls/questions?PollGid=${pollGid}`, ApiMethod.GET)
      .pipe(
        map(data => data)
      )
  }
}
