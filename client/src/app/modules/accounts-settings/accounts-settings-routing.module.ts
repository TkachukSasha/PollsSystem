import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountsSettingsComponent } from "./pages/accounts-settings/accounts-settings.component";

const routes: Routes = [
  {path: "", component: AccountsSettingsComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class AccountsSettingsRoutingModule { }
