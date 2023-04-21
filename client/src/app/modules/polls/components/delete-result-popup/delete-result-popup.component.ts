import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {IPollResults} from "../../models/poll-results";
import {Subscription} from "rxjs";
import {Location} from "@angular/common";
import {PollsService} from "../../services/polls-service.service";
import {DeletePollResult} from "../../models/delete-poll-result";

@Component({
  selector: 'app-delete-result-popup',
  templateUrl: './delete-result-popup.component.html',
  styleUrls: ['./delete-result-popup.component.scss']
})
export class DeleteResultPopupComponent implements OnInit {
  @Input() result: IPollResults;
  @Output()
  pollResultDeleted: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output()
  pollResultRejected: EventEmitter<boolean> = new EventEmitter<boolean>();
  deletePollResultSubscription: Subscription;


  constructor(
    private location: Location,
    private _pollsService: PollsService
  ) { }

  ngOnInit(): void {
  }

  onConfirm(){
    let request = new DeletePollResult(this.result.pollGid, this.result.gid);

    this.deletePollResultSubscription = this._pollsService.deletePollResult(request)
      .subscribe((data) => {
        if(data){
          this.pollResultDeleted.emit(true);
        }
      })
  }

  onReject(){
    this.pollResultRejected.emit(false);
  }
}
