import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from "./core/guards/auth.guard";

const routes: Routes = [
  {
    path: "",
    loadChildren: () => import("./modules/accounts/accounts.module").then(m => m.AccountsModule)
  },
  {
    path: "polls",
    loadChildren: () => import("./modules/polls/polls.module").then(m => m.PollsModule),
    canActivate: [AuthGuard]
  },
  {
    path: "accounts-settings",
    loadChildren: () => import("./modules/accounts-settings/accounts-settings.module").then(m => m.AccountsSettingsModule),
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
