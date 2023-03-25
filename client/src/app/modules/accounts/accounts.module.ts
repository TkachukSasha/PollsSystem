import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountsRoutingModule } from "./accounts-routing.module";
import { SignInComponent } from './pages/sign-in/sign-in.component';
import { ReactiveFormsModule } from "@angular/forms";
import { SignUpComponent } from './pages/sign-up/sign-up.component';
import { SharedModule } from "../../shared/shared.module";
import { NgxSpinnerModule } from "ngx-spinner";

@NgModule({
  declarations: [
    SignInComponent,
    SignUpComponent
  ],
  imports: [
    CommonModule,
    AccountsRoutingModule,
    ReactiveFormsModule,
    SharedModule,
    NgxSpinnerModule
  ]
})
export class AccountsModule { }
