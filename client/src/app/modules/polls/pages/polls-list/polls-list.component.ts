import {ChangeDetectionStrategy, Component, OnDestroy, OnInit} from '@angular/core';
import {IPoll} from "../../models/poll-model";
import {HttpService} from "../../../../core/services/http/http-service";
import {ApiMethod} from "../../../../core/enums/api-methods";
import {map, Observable, Subscription, tap} from "rxjs";
import {NgxSpinnerService} from "ngx-spinner";

@Component({
  selector: 'app-polls-list',
  templateUrl: './polls-list.component.html',
  styleUrls: ['./polls-list.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PollsListComponent implements OnInit, OnDestroy {
  isPollItemPopupShowed: boolean = false;
  isLoaded: boolean = false;
  searchText: string;
  selectedPollGid: string;
  poll: IPoll;
  polls: IPoll[];
  polls$: Observable<IPoll[]>;

  constructor(
    private _http: HttpService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    // @ts-ignore
    this.polls$ = this._http.requestCall<IPoll[]>(`/polls`, ApiMethod.GET)
      .pipe(
        map((data) => this.polls = data),
        tap(() => this.onPollsLoaded())
      )
  }

  ngOnDestroy(): void {
  }

  onPollsLoaded(){
    this.isLoaded = true;
    this.spinner.hide();
  }

  onSearchTextEntered(searchValue: string){
    this.searchText = searchValue;
  }

  onPollSelected(pollGid: string){
    this.selectedPollGid = pollGid;
    // @ts-ignore
    this.poll = this.polls.find((item) => item.gid == this.selectedPollGid);
    this.isPollItemPopupShowed = true;
  }
}
