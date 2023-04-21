import {ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {IPollResults} from "../../models/poll-results";
import {PollsService} from "../../services/polls-service.service";
import {NgxSpinnerService} from "ngx-spinner";
import {ChangeResultScore} from "../../models/change-result-score";

@Component({
  selector: 'app-edit-poll-result-popup',
  templateUrl: './edit-poll-result-popup.component.html',
  styleUrls: ['./edit-poll-result-popup.component.scss']
})
export class EditPollResultPopupComponent implements OnInit {
  @Input() result: IPollResults;
  isUpdated: boolean = false;
  @Output()
  pollResultEditingRejected: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output()
  pollResultEditingUpdated: EventEmitter<boolean> = new EventEmitter<boolean>();


  constructor(
    private _pollsService: PollsService,
    private spinner: NgxSpinnerService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
  }

  onReject(){
    this.pollResultEditingRejected.emit(false);
    this.pollResultEditingUpdated.emit(this.isUpdated);
  }

  onScoreResultChange() {
    let request = new ChangeResultScore(this.result.pollGid, this.result.lastName, this.result.score);

    this._pollsService.changeResultScore(request)
      .subscribe((data) => { data ? this.isUpdated = true :  this.isUpdated = false }
      )

    this.cdr.detectChanges();
  }
}
