import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {IPollResults} from "../../models/poll-results";

@Component({
  selector: 'app-poll-result-item',
  templateUrl: './poll-result-item.component.html',
  styleUrls: ['./poll-result-item.component.scss']
})
export class PollResultItemComponent implements OnInit {
  @Input() result: IPollResults;
  @Output()
  pollResultLaunchSelected: EventEmitter<string> = new EventEmitter<string>();
  @Output()
  pollResultDeletedSelected: EventEmitter<string> = new EventEmitter<string>();

  constructor() { }

  ngOnInit(): void {
  }

  onDelete(){
    this.pollResultDeletedSelected.emit(this.result.gid);
  }

  onLaunch(){
    this.pollResultLaunchSelected.emit(this.result.gid);
  }
}
