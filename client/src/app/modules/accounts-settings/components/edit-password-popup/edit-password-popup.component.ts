import {ChangeDetectionStrategy, Component, OnDestroy, OnInit} from '@angular/core';
import { AccountSettingsService } from "../../services/account-settings.service";
import { NgxSpinnerService } from "ngx-spinner";
import { StorageService } from "../../../../core/services/storage/storage-service";
import { ValidatePassword } from "../../models/validate-password";
import { ChangePassword } from "../../models/change-password";
import { Router } from "@angular/router";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-edit-password-popup',
  templateUrl: './edit-password-popup.component.html',
  styleUrls: ['./edit-password-popup.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EditPasswordPopupComponent implements OnInit, OnDestroy {
  isVerified: boolean = false;
  isPopupShow: boolean = false;
  isChanged: boolean = false;
  isLoaded: boolean = false;
  currentPassword: string;
  password: string;
  editPasswordSubscription: Subscription;

  constructor(
    private _storage: StorageService,
    private _accountsService: AccountSettingsService,
    private router: Router,
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

    this.editPasswordSubscription = this._accountsService.verifyPassword(request)
        .subscribe(
          (data) => {
            this.isVerified = data;
            this.isPopupShow = true;
            this.onRequestExcecuted();
          }
        )
  }

  onPasswordChange(){
    // @ts-ignore
    const data = JSON.parse(this._storage.getData('auth'));

    let userGid = data?.userGid;

    let request = new ChangePassword(userGid, this.currentPassword, this.password);

    console.log(`change password request: ${request}`);

    this.spinner.show();

    this._accountsService.changePassword(request)
      .subscribe(
        (data: boolean) => {
          this.isChanged = data;

          if(this.isChanged === true) {
            this.isPopupShow = false;
            this.onRequestExcecuted();
            this._storage.clearData('auth');
            this.router.navigateByUrl('');
          }
        }
      )
  }

  onRequestExcecuted() {
    this.isLoaded = true;
    this.spinner.hide();
  }

  ngOnDestroy(): void {
    if (this.editPasswordSubscription) {
      this.editPasswordSubscription.unsubscribe();
    }
  }

}
