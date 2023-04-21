import {Injectable} from '@angular/core';
import {HttpService} from "../../../core/services/http/http-service";
import {map, Observable} from "rxjs";
import {CheckPollKey} from "../models/check-poll-key";
import {ApiMethod} from "../../../core/enums/api-methods";
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
import {IQuestionsWithAnswersAndScores} from "../models/question-model";
import {ChangeAnswerText} from "../models/change-answer-text";
import {DeleteQuestionAnswer} from "../models/delete-question-answer";
import {IScore} from "../models/score";
import {ChangeAnswerScore} from "../models/change-answer-score";
import {CreatePollQuestions} from "../models/create-poll-questions";
import {IPollResults} from "../models/poll-results";
import {DeletePollResult} from "../models/delete-poll-result";
import {ChangeResultScore} from "../models/change-result-score";

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

  deletePollResult(payload: DeletePollResult) : Observable<boolean>{
    let request = JSON.stringify(payload);

    // @ts-ignore
    return  this._http.requestCall<boolean>(`/statistics/delete-result`, ApiMethod.DELETE, request)
      .pipe(
        map((data: boolean) => {
          return data;
        })
      )
  }

  getPollQuestionsWithScores(pollGid: string) : Observable<IQuestionsWithAnswersAndScores[]>{
    // @ts-ignore
    return this._http.requestCall<IQuestionsWithAnswersAndScores[]>(`/polls/questions-with-scores?PollGid=${pollGid}`, ApiMethod.GET)
      .pipe(
        map(data => data)
      )
  }

  getScores() : Observable<IScore[]>{
    // @ts-ignore
    return this._http.requestCall<IScore[]>(`/polls/scores`, ApiMethod.GET)
      .pipe(
        map(data => data)
      )
  }

  changeAnswerText(payload: ChangeAnswerText) : Observable<boolean> {
    let request = JSON.stringify(payload);

    // @ts-ignore
    return this._http.requestCall<boolean>(`/holders/change-answer-text`, ApiMethod.PATCH, request)
      .pipe(
        map((data: boolean) => {
          return data;
        })
      )
  }

  changeAnswerScore(payload: ChangeAnswerScore) : Observable<boolean> {
    let request = JSON.stringify(payload);

    // @ts-ignore
    return this._http.requestCall<boolean>(`/holders/change-answer-score`, ApiMethod.PATCH, request)
      .pipe(
        map((data: boolean) => {
          return data;
        })
      )
  }

  changeResultScore(payload: ChangeResultScore) : Observable<boolean> {
    let request = JSON.stringify(payload);

    // @ts-ignore
    return this._http.requestCall<boolean>(`/statistics/change-score`, ApiMethod.PATCH, request)
      .pipe(
        map((data: boolean) => {
          return data;
        })
      )
  }

  deleteQuestionAnswer(payload: DeleteQuestionAnswer): Observable<boolean> {
    let request = JSON.stringify(payload);

    // @ts-ignore
    return this._http.requestCall<boolean>(`/holders/delete-question-answer`, ApiMethod.DELETE, request)
      .pipe(
        map((data: boolean) => {
          return data;
        })
      )
  }

  createPollQuestions(payload: CreatePollQuestions) : Observable<boolean> {
    let request = JSON.stringify(payload);

    // @ts-ignore
    return this._http.requestCall<boolean>(`/holders/create-poll-questions`, ApiMethod.POST, request)
      .pipe(
        map((data: boolean) => {
          return data;
        })
      )
  }
}
