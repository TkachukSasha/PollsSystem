import { NgModule } from '@angular/core';
import {CommonModule, Location} from '@angular/common';
import { PollsListComponent } from './pages/polls-list/polls-list.component';
import { PollsItemComponent } from './components/polls-item/polls-item.component';
import { PollsRoutingModule } from "./polls-routing.module";
import { SharedModule } from "../../shared/shared.module";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import { MatIconModule } from "@angular/material/icon";
import { FilterPollsPipe } from "./pipes/filter-polls.pipe";
import { NgxSpinnerModule } from "ngx-spinner";
import { PollItemPopupComponent } from './components/poll-item-popup/poll-item-popup.component';
import { MainLayoutComponent } from './pages/main-layout/main-layout.component';
import { PollPassComponent } from './pages/poll-pass/poll-pass.component';
import { MyPollsComponent } from './pages/my-polls/my-polls.component';
import { MyPollItemComponent } from './components/my-poll-item/my-poll-item.component';
import { PollPassThanksgivingComponent } from './pages/poll-pass-thanksgiving/poll-pass-thanksgiving.component';
import { ExistResultPopupComponent } from './components/exist-result-popup/exist-result-popup.component';
import { TimeOverPopupComponent } from './components/time-over-popup/time-over-popup.component';
import { CreatePollComponentPopup } from "./components/create-poll-popup/create-poll.component-popup";
import { EditPollComponentPopup } from "./components/edit-poll-popup/edit-poll.component-popup";
import { DeletePollPopupComponent } from "./components/delete-poll-popup/delete-poll-popup.component";
import { PollStatisticsComponent } from './pages/poll-statistics/poll-statistics.component';

@NgModule({
  declarations: [
    PollsListComponent,
    PollsItemComponent,
    FilterPollsPipe,
    PollItemPopupComponent,
    MainLayoutComponent,
    PollPassComponent,
    MyPollsComponent,
    MyPollItemComponent,
    PollPassThanksgivingComponent,
    ExistResultPopupComponent,
    TimeOverPopupComponent,
    CreatePollComponentPopup,
    EditPollComponentPopup,
    DeletePollPopupComponent,
    PollStatisticsComponent
  ],
    imports: [
        CommonModule,
        PollsRoutingModule,
        SharedModule,
        FormsModule,
        MatIconModule,
        NgxSpinnerModule,
        ReactiveFormsModule
    ],
  exports: [
    FilterPollsPipe
  ],
  providers: [
    Location
  ]
})
export class PollsModule { }
