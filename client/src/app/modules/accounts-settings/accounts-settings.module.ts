import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountsSettingsRoutingModule } from "./accounts-settings-routing.module";
import { AccountsSettingsComponent } from "./pages/accounts-settings/accounts-settings.component";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import { EditUsernamePopupComponent } from './components/edit-username-popup/edit-username-popup.component';
import { EditPasswordPopupComponent } from './components/edit-password-popup/edit-password-popup.component';
import {MatIconModule} from "@angular/material/icon";
import {NgxSpinnerModule} from "ngx-spinner";

@NgModule({
  declarations: [
    AccountsSettingsComponent,
    EditUsernamePopupComponent,
    EditPasswordPopupComponent
  ],
  imports: [
    CommonModule,
    AccountsSettingsRoutingModule,
    ReactiveFormsModule,
    MatIconModule,
    FormsModule,
    NgxSpinnerModule
  ]
})
export class AccountsSettingsModule { }
