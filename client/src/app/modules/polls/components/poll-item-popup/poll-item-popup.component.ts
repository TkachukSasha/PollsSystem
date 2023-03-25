import {ChangeDetectionStrategy, Component, Input, OnInit} from '@angular/core';
import { IPoll } from "../../models/poll-model";

@Component({
  selector: 'app-poll-item-popup',
  templateUrl: './poll-item-popup.component.html',
  styleUrls: ['./poll-item-popup.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PollItemPopupComponent implements OnInit {

  @Input() poll: IPoll

  constructor() { }

  ngOnInit(): void {
  }

}
