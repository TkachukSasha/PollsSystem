import {ChangeDetectionStrategy, Component, HostListener, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import { HttpService } from "../../../../core/services/http/http-service";
import {map, Observable, Subscription, tap} from "rxjs";
import {IQuestionsWithAnswers} from "../../models/question-model";
import {NgxSpinnerService} from "ngx-spinner";
import {ApiMethod} from "../../../../core/enums/api-methods";
import {StorageService} from "../../../../core/services/storage/storage-service";
import {PollsService} from "../../services/polls-service.service";
import {SendReplies} from "../../models/send-replies";

@Component({
  selector: 'app-poll-pass',
  templateUrl: './poll-pass.component.html',
  styleUrls: ['./poll-pass.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PollPassComponent implements OnInit, OnDestroy {
  data: any;
  pollGid: string;
  duration: number;
  isLoaded: boolean = false;
  isTimeOver: boolean = false;
  isResultExist: boolean = false;
  selectedOptions: Map<string, string> = new Map<string, string>();
  questions$: Observable<IQuestionsWithAnswers[]>;
  existingResultSubscription: Subscription;
  sendRepliesSubscription: Subscription;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private _storage: StorageService,
    private _http: HttpService,
    private  _pollsService: PollsService,
    private spinner: NgxSpinnerService
  ) {
    this.route.params.subscribe(params => {
      this.pollGid = params['pollGid'];
      this.duration = parseInt(params['duration']) * 60;
    });
  }

  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    $event.returnValue = true;
  }


  ngOnInit() {
    // @ts-ignore
    this.data = JSON.parse(this._storage.getData('auth'));

    // @ts-ignore
    this.existingResultSubscription = this._http.requestCall<boolean>(`/statistics/get-result?PollGid=${this.pollGid}&LastName=${this.data?.lastName}`, ApiMethod.GET)
      .pipe(
        map((data) => this.isResultExist = data),
        tap(() => this.onRequestExecuted())
      )
      .subscribe(
        () => {}, // Success callback
        (error) => console.error(error) // Error callback
      );

    if(this.isResultExist === false){
      this.spinner.show();

      // @ts-ignore
      this.questions$ = this._http.requestCall<IQuestionsWithAnswers[]>(`/polls/questions?PollGid=${this.pollGid}`, ApiMethod.GET)
        .pipe(
          tap(() => this.onRequestExecuted()),
          map(data => data)
        )
    }
  }

  ngOnDestroy(): void {
    this.existingResultSubscription.unsubscribe();
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
    let request = new SendReplies(this.pollGid, this.data?.firstName, this.data?.lastName, this.selectedOptions);

    // @ts-ignore
    this.sendRepliesSubscription = this._pollsService.sendReplies(request)
      .subscribe((data) => {
        if(data)
          this.router.navigateByUrl('/polls/poll-pass/thanksgiving');
      })
  }
}
