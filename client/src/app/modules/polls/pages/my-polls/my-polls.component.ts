import {ChangeDetectionStrategy, Component, OnDestroy, OnInit} from '@angular/core';
import {map, Observable, Subscription, tap} from "rxjs";
import {IPoll} from "../../models/poll-model";
import {HttpService} from "../../../../core/services/http/http-service";
import {NgxSpinnerService} from "ngx-spinner";
import {ApiMethod} from "../../../../core/enums/api-methods";
import {StorageService} from "../../../../core/services/storage/storage-service";

@Component({
  selector: 'app-my-polls',
  templateUrl: './my-polls.component.html',
  styleUrls: ['./my-polls.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MyPollsComponent implements OnInit, OnDestroy {
  isLoaded: boolean = false;
  searchText: string;
  polls$: Observable<IPoll[]>;
  pollsSubscription: Subscription[] = [];

  constructor(
    private _http: HttpService,
    private _storage: StorageService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    // @ts-ignore
    const data = JSON.parse(this._storage.getData('auth'));

    this.spinner.show();

    this.pollsSubscription.push(
      // @ts-ignore
      this.polls$ = this._http.requestCall<IPoll[]>(`/polls/user-polls?UserGid=${data?.userGid}`, ApiMethod.GET)
        .pipe(
          tap(() => this.onPollsLoaded())
        )
    )
  }

  ngOnDestroy(): void {
    this.pollsSubscription.forEach((subscription) => subscription.unsubscribe());
  }

  onSearchTextEntered(searchValue: string){
    this.searchText = searchValue;
  }

  onPollsLoaded(){
    this.isLoaded = true;
    this.spinner.hide();
  }

}
