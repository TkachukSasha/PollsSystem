import {ChangeDetectionStrategy, Component, OnInit} from '@angular/core';
import { Router } from "@angular/router";

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MainLayoutComponent implements OnInit {

  constructor(
    private router: Router
  ) { }

  ngOnInit(): void {
      this.router.navigateByUrl('polls/polls-list');
  }

}
