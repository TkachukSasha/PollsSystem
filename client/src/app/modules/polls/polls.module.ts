import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PollsListComponent } from './pages/polls-list/polls-list.component';
import { PollsItemComponent } from './components/polls-item/polls-item.component';
import { PollsRoutingModule } from "./polls-routing.module";



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
