import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PollsRoutingModule } from "./polls-routing.module";
import { PollsListComponent } from './pages/polls-list/polls-list.component';
import { PollsItemComponent } from './components/polls-item/polls-item.component';

@NgModule({
  declarations: [
    PollsListComponent,
    PollsItemComponent
  ],
  imports: [
    CommonModule,
    PollsRoutingModule
  ]
})
export class PollsModule { }
