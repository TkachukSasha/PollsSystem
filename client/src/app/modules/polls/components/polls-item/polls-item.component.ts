import { Component, Input, OnInit } from '@angular/core';
import { IPoll } from "../../models/poll-model";

@Component({
  selector: 'app-polls-item',
  templateUrl: './polls-item.component.html',
  styleUrls: ['./polls-item.component.scss']
})
export class PollsItemComponent implements OnInit {

  @Input() poll: IPoll

  constructor() { }

  ngOnInit(): void {
  }

}
