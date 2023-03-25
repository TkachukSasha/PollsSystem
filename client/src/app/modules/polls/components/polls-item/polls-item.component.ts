import { Component, EventEmitter, Input, Output, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { IPoll } from "../../models/poll-model";

@Component({
  selector: 'app-polls-item',
  templateUrl: './polls-item.component.html',
  styleUrls: ['./polls-item.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PollsItemComponent implements OnInit {
  @Input() poll: IPoll
  @Output()
  pollSelected: EventEmitter<string> = new EventEmitter<string>();

  constructor() { }

  ngOnInit(): void {
  }

  onCardClick(){
    this.pollSelected.emit(this.poll.gid);
  }
}
