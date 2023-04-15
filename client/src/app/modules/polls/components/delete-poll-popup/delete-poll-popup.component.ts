import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import { IPoll } from "../../models/poll-model";
import { PollsService } from "../../services/polls-service.service";
import {Subscription} from "rxjs";
import {DeletePoll} from "../../models/delete-poll";
import {StorageService} from "../../../../core/services/storage/storage-service";
import {Location} from "@angular/common";

@Component({
  selector: 'app-delete-poll-popup',
  templateUrl: './delete-poll-popup.component.html',
  styleUrls: ['./delete-poll-popup.component.scss']
})
export class DeletePollPopupComponent implements OnInit {
  polls: IPoll[];
  @Input() poll: IPoll;
  @Output()
  pollDeleted: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output()
  pollRejected: EventEmitter<boolean> = new EventEmitter<boolean>();
  deletePollSubscription: Subscription;

  constructor(
    private location: Location,
    private _pollsService: PollsService,
    private _storage: StorageService
  ) { }

  ngOnInit(): void {
  }

  onConfirm(){
    let request = new DeletePoll(this.poll.gid);

    this.deletePollSubscription = this._pollsService.deletePoll(request)
      .subscribe((data) => {
        if(data){
          this.pollDeleted.emit(true);
        }
      })
  }

  onReject(){
    this.pollRejected.emit(false);
  }
}
