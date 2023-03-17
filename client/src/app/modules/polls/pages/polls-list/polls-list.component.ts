import { Component, OnInit } from '@angular/core';
import { IPoll } from "../../models/poll-model";
import { polls as data } from "../../fake-data/polls"

@Component({
  selector: 'app-polls-list',
  templateUrl: './polls-list.component.html',
  styleUrls: ['./polls-list.component.scss']
})
export class PollsListComponent implements OnInit {

  polls: IPoll[] = data

  constructor() { }

  ngOnInit(): void {
  }

}
