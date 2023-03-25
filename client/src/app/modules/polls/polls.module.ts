import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PollsListComponent } from './pages/polls-list/polls-list.component';
import { PollsItemComponent } from './components/polls-item/polls-item.component';
import { PollsRoutingModule } from "./polls-routing.module";
import { SharedModule } from "../../shared/shared.module";
import { FormsModule } from "@angular/forms";
import { MatIconModule } from "@angular/material/icon";
import { FilterPollsPipe } from "./pipes/filter-polls.pipe";
import { NgxSpinnerModule } from "ngx-spinner";
import { PollItemPopupComponent } from './components/poll-item-popup/poll-item-popup.component';
import { MainLayoutComponent } from './pages/main-layout/main-layout.component';

@NgModule({
  declarations: [
    PollsListComponent,
    PollsItemComponent,
    FilterPollsPipe,
    PollItemPopupComponent,
    MainLayoutComponent
  ],
    imports: [
        CommonModule,
        PollsRoutingModule,
        SharedModule,
        FormsModule,
        MatIconModule,
        NgxSpinnerModule
    ],
  exports: [
    FilterPollsPipe
  ]
})
export class PollsModule { }
