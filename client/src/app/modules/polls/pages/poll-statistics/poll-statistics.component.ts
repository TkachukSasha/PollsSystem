import {Component, OnInit} from '@angular/core';
import {IPollResults} from "../../models/poll-results";
import {map, Observable, tap} from "rxjs";
import {HttpService} from "../../../../core/services/http/http-service";
import {ApiMethod} from "../../../../core/enums/api-methods";
import {ActivatedRoute} from "@angular/router";
import {NgxSpinnerService} from "ngx-spinner";

@Component({
  selector: 'app-poll-statistics',
  templateUrl: './poll-statistics.component.html',
  styleUrls: ['./poll-statistics.component.scss']
})
export class PollStatisticsComponent implements OnInit {
  isDeleteItemPopupShowed: boolean = false;
  isEditItemPopupShowed: boolean = false;
  pollGid: string;
  selectedResultGid: string;
  isLoaded: boolean = false;
  result: IPollResults;
  pollResults: IPollResults[];
  pollResults$: Observable<IPollResults[]>;

  constructor(
    private _http: HttpService,
    private route: ActivatedRoute,
    private spinner: NgxSpinnerService
  ) {
    this.route.params.subscribe(params => {
      this.pollGid = params['pollGid'];
    });
  }

  ngOnInit(): void {
    this.spinner.show();

    // @ts-ignore
    this.pollResults$ = this._http.requestCall<IPollResults[]>(`/statistics/results?PollGid=${this.pollGid}`, ApiMethod.GET)
      .pipe(
        map((data) => this.pollResults = data),
        tap(() => this.onResultsLoaded())
      )
  }

  onResultsLoaded(){
    this.isLoaded = true;
    this.spinner.hide();
  }

  onResultSelected(resultGid: string){
    this.selectedResultGid = resultGid;
    this.result = this.pollResults.find((item) => item.gid === this.selectedResultGid);
    this.isDeleteItemPopupShowed = true;
  }

  onResultLaunchSelected(resultGid: string){
    this.selectedResultGid = resultGid;
    this.result = this.pollResults.find((item) => item.gid === this.selectedResultGid);
    this.isEditItemPopupShowed = true;
  }

  onDelete(status: boolean){
    if(status){
      this.isDeleteItemPopupShowed = false;
      location.reload();
    }
  }

  onEditReject(status: boolean){
    this.isEditItemPopupShowed = status;
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
