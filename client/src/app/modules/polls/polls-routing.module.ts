import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PollsListComponent } from "./pages/polls-list/polls-list.component";

const routes: Routes = [
  {
      path: "",
      component: PollsListComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PollsRoutingModule { }
