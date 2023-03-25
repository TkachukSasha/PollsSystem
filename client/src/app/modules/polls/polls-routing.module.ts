import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PollsListComponent } from "./pages/polls-list/polls-list.component";
import { MainLayoutComponent } from "./pages/main-layout/main-layout.component";

const routes: Routes = [
  {
    path: "",
    component: MainLayoutComponent,
    children: [
      {path: "polls-list", component: PollsListComponent}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PollsRoutingModule { }
