import { Component, OnInit } from '@angular/core';
import { AccountSettingsService } from "../../services/account-settings.service";
import { NgxSpinnerService } from "ngx-spinner";
import {ChangeUsername} from "../../models/change-username";

@Component({
  selector: 'app-edit-username-popup',
  templateUrl: './edit-username-popup.component.html',
  styleUrls: ['./edit-username-popup.component.scss']
})
export class EditUsernamePopupComponent implements OnInit {
  isLoaded: boolean = false;
  currentUserName: string = '';
  userName: string;

  constructor(
    private _accountsService: AccountSettingsService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.spinner.show();

    this._accountsService.getUserName()
      .subscribe(
        (data: string) => {
          this.currentUserName = data;
          this.onRequestExcecuted();
        }
      )
  }

  onUserNameUpdate(){
    let request = new ChangeUsername(this.currentUserName, this.userName);

    this.spinner.show();

    this._accountsService.changeUserName(request)
      .subscribe(
        (data) => {
          this.onRequestExcecuted();
        }
      )
  }

  onRequestExcecuted(){
    this.isLoaded = true;
    this.spinner.hide();
  }

}
