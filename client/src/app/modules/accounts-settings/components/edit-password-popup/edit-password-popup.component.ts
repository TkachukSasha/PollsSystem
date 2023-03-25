import { Component, OnInit } from '@angular/core';
import { AccountSettingsService } from "../../services/account-settings.service";
import { NgxSpinnerService } from "ngx-spinner";
import { StorageService } from "../../../../core/services/storage/storage-service";
import {ValidatePassword} from "../../models/validate-password";

@Component({
  selector: 'app-edit-password-popup',
  templateUrl: './edit-password-popup.component.html',
  styleUrls: ['./edit-password-popup.component.scss']
})
export class EditPasswordPopupComponent implements OnInit {
  isVerified: boolean = false;
  isLoaded: boolean = false;
  currentPassword: string;

  constructor(
    private _storage: StorageService,
    private _accountsService: AccountSettingsService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
  }

  onPasswordVerify() {
      // @ts-ignore
    const data = JSON.parse(this._storage.getData('auth'));

    let userGid = data?.userGid;

    let request = new ValidatePassword(userGid, this.currentPassword);

    this.spinner.show();

      this._accountsService.verifyPassword(request)
        .subscribe(
          (data) => {
            this.isVerified = data;
            this.onRequestExcecuted();
          }
        )
  }

  onRequestExcecuted() {
    this.isLoaded = true;
    this.spinner.hide();
  }

}
