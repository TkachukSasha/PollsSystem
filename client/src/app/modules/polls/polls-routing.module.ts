import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PollsListComponent } from "./pages/polls-list/polls-list.component";
import { MainLayoutComponent } from "./pages/main-layout/main-layout.component";
import { PollPassComponent } from "./pages/poll-pass/poll-pass.component";
import { MyPollsComponent } from "./pages/my-polls/my-polls.component";
import { PollPassThanksgivingComponent } from "./pages/poll-pass-thanksgiving/poll-pass-thanksgiving.component";
import { EditPollComponentPopup } from "./components/edit-poll-popup/edit-poll.component-popup";
import { CreatePollComponentPopup } from "./components/create-poll-popup/create-poll.component-popup";

const routes: Routes = [
  {
    path: "",
    component: MainLayoutComponent,
    children: [
      {path: "polls-list", component: PollsListComponent},
      {path: "my-polls", component: MyPollsComponent},
      {path: "poll-pass/:pollGid/:duration", component: PollPassComponent},
      {path: "poll-pass/thanksgiving", component: PollPassThanksgivingComponent},
      {path: "create-poll-popup", component: CreatePollComponentPopup },
      {path: "edit-poll-popup/:pollGid", component: EditPollComponentPopup}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PollsRoutingModule { }
