import {ChangeDetectionStrategy, Component, HostListener, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import { HttpService } from "../../../../core/services/http/http-service";
import {map, Observable, Subscription, tap} from "rxjs";
import {IQuestionsWithAnswers} from "../../models/question-model";
import {NgxSpinnerService} from "ngx-spinner";
import {ApiMethod} from "../../../../core/enums/api-methods";
import {StorageService} from "../../../../core/services/storage/storage-service";

@Component({
  selector: 'app-poll-pass',
  templateUrl: './poll-pass.component.html',
  styleUrls: ['./poll-pass.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PollPassComponent implements OnInit, OnDestroy {
  pollGid: string;
  duration: number;
  isLoaded: boolean = false;
  isTimeOver: boolean = false;
  isResultExist: boolean = false;
  selectedOptions: Map<string, string> = new Map<string, string>();
  questions$: Observable<IQuestionsWithAnswers[]>;
  questionSubscription: Subscription;
  existingResultSubscription: Subscription;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private _storage: StorageService,
    private _http: HttpService,
    private spinner: NgxSpinnerService
  ) {
    this.route.params.subscribe(params => {
      this.pollGid = params['pollGid'];
      this.duration = parseInt(params['duration']) * 60;
    });

    // @ts-ignore
    const data = JSON.parse(this._storage.getData('auth'));

    // @ts-ignore
    this.existingResultSubscription = this._http.requestCall<boolean>(`/statistics/get-result?PollGid=${this.pollGid}&LastName=${data?.lastName}`, ApiMethod.GET)
      .pipe(
        map((data) => this.isResultExist = data),
        tap(() => this.onRequestExecuted())
      )
  }

  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    $event.returnValue = true;
  }


  ngOnInit() {
    this.spinner.show();
    // @ts-ignore
    this.questions$ = this._http.requestCall<IQuestionsWithAnswers[]>(`/polls/questions?PollGid=${this.pollGid}`, ApiMethod.GET)
      .pipe(
        tap(() => this.onRequestExecuted()),
        map(data => data)
      )

    this.questionSubscription = this.questions$.subscribe();
  }

  ngOnDestroy(): void {
    if (this.questionSubscription) {
      this.questionSubscription.unsubscribe();
    }
  }

  onTimeEnded(){
      this.isTimeOver = true;
  }

  onRequestExecuted(){
    this.isLoaded = true;
    this.spinner.hide();
  }

  onRadioChange(questionId: string, selectedId: string) {
    this.selectedOptions.set(questionId, selectedId);
  }

  onSubmitClick(){
    console.log(this.selectedOptions)
    this.router.navigateByUrl('/polls/poll-pass/thanksgiving');
  }

}
