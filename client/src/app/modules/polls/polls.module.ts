import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PollsItemComponent } from './components/polls-item/polls-item.component';
import { PollsListComponent } from './pages/polls-list/polls-list.component';
import { PollsRoutingModule } from "./polls-routing.module";

@NgModule({
  declarations: [
    PollsItemComponent,
    PollsListComponent
  ],
  imports: [
    CommonModule,
    PollsRoutingModule
  ]
})
export class PollsModule { }
