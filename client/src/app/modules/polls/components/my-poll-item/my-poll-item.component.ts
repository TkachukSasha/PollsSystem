import {ChangeDetectionStrategy, Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import { IPoll } from "../../models/poll-model";

@Component({
  selector: 'app-my-poll-item',
  templateUrl: './my-poll-item.component.html',
  styleUrls: ['./my-poll-item.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MyPollItemComponent implements OnInit {
  @Input() poll: IPoll
  @Output()
  pollTitleSelected: EventEmitter<string> = new EventEmitter<string>();
  @Output()
  pollSelected: EventEmitter<string> = new EventEmitter<string>();

  constructor() { }

  ngOnInit(): void {
  }

  onTitleClick(){
    console.log(`selected`)
    this.pollTitleSelected.emit(this.poll.gid);
  }

  onDeletePoll(){
    this.pollSelected.emit(this.poll.gid);
  }
}
