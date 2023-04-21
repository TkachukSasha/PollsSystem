import {ChangeDetectionStrategy, Component, OnDestroy, OnInit} from '@angular/core';
import {map, Observable, Subscription, tap} from "rxjs";
import {IPoll} from "../../models/poll-model";
import {HttpService} from "../../../../core/services/http/http-service";
import {NgxSpinnerService} from "ngx-spinner";
import {ApiMethod} from "../../../../core/enums/api-methods";
import {StorageService} from "../../../../core/services/storage/storage-service";
import {Router} from "@angular/router";
import { Location } from '@angular/common';

@Component({
  selector: 'app-my-polls',
  templateUrl: './my-polls.component.html',
  styleUrls: ['./my-polls.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MyPollsComponent implements OnInit, OnDestroy {
  isDeleteItemPopupShowed: boolean = false;
  isEditItemPopupShowed: boolean = false;
  isCreateItemPopupShowed: boolean = false;
  isLoaded: boolean = false;
  searchText: string;
  poll: IPoll;
  polls: IPoll[];
  polls$: Observable<IPoll[]>;
  pollsSubscription: Subscription[] = [];
  selectedPollGid: string;

  constructor(
    private location: Location,
    private router: Router,
    private _http: HttpService,
    private _storage: StorageService,
    private spinner: NgxSpinnerService
  ) {
  }

  ngOnInit(): void {
    // @ts-ignore
    const data = JSON.parse(this._storage.getData('auth'));

    this.spinner.show();

    this.pollsSubscription.push(
      // @ts-ignore
      this.polls$ = this._http.requestCall<IPoll[]>(`/polls/user-polls?UserGid=${data?.userGid}`, ApiMethod.GET)
        .pipe(
          map((data) => this.polls = data),
          tap(() => this.onPollsLoaded())
        )
    )
  }

  ngOnDestroy(): void {
  }

  onSearchTextEntered(searchValue: string){
      this.searchText = searchValue;
  }

  onPollsLoaded(){
    this.isLoaded = true;
    this.spinner.hide();
  }

  onCreatePoll(){
    this.isCreateItemPopupShowed = true;
  }

  onLaunchSelected(pollGid: string){
    this.selectedPollGid = pollGid;
    this.router.navigate(['/polls/poll-statistics/', pollGid]);
  }

  onPollSelected(pollGid: string){
    this.selectedPollGid = pollGid;
    // @ts-ignore
    this.poll = this.polls.find((item) => item.gid === this.selectedPollGid);
    this.isDeleteItemPopupShowed = true;
  }

  onPollTitleSelected(pollGid: string){
    this.selectedPollGid = pollGid;
    // @ts-ignore
    this.poll = this.polls.find((item) => item.gid === this.selectedPollGid);
    this.isEditItemPopupShowed = true;
  }

  onEditReject(status: boolean){
    this.isEditItemPopupShowed = status;
  }

  onCreateReject(status: boolean){
    this.isCreateItemPopupShowed = status;
  }

  onPollQuestionsCreated(status: boolean){
    if(status)
      this.isCreateItemPopupShowed = false;
  }

  onDeleted(status: boolean){
    if(status){
      this.isDeleteItemPopupShowed = false;
      location.reload();
    }
  }

  onReject(status: boolean){
    this.isDeleteItemPopupShowed = status;
  }

  onUpdate(status: boolean){
    if(status){
      location.reload();
    }
  }
}
