import {Component, Input, OnInit} from '@angular/core';
import {IPollResults} from "../../models/poll-results";

@Component({
  selector: 'app-graphics-item',
  templateUrl: './graphics-item.component.html',
  styleUrls: ['./graphics-item.component.scss']
})
export class GraphicsItemComponent implements OnInit {
  @Input() results: IPollResults[];
  activeTab: string = 'BarChart'

  constructor() { }

  ngOnInit(): void {
  }

  onTabClick(name: string){
    this.activeTab = name;
  }

}
