import {ChangeDetectionStrategy, Component, OnDestroy, OnInit} from '@angular/core';
import { AccountSettingsService } from "../../services/account-settings.service";
import { NgxSpinnerService } from "ngx-spinner";
import { ChangeUsername } from "../../models/change-username";
import { Router } from "@angular/router";
import { StorageService } from "../../../../core/services/storage/storage-service";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-edit-username-popup',
  templateUrl: './edit-username-popup.component.html',
  styleUrls: ['./edit-username-popup.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EditUsernamePopupComponent implements OnInit, OnDestroy{
  isLoaded: boolean = false;
  isDisabled: boolean = false;
  currentUserName: string;
  userName: string;
  editUserNameSubscription: Subscription;

  constructor(
    private _accountsService: AccountSettingsService,
    private _storage: StorageService,
    private router: Router,
    private spinner: NgxSpinnerService
  ) {
    // @ts-ignore
    const data = JSON.parse(this._storage.getData('auth'));

    this.spinner.show();

    this.editUserNameSubscription = this._accountsService.getUserName(data?.userGid)
      .subscribe(
        (data: any) => {
          this.currentUserName = data.userName;
          this.isDisabled = true;
          this.onRequestExcecuted();
        }
      )
  }


  ngOnInit(): void {
  }

  onUserNameUpdate(){
    let request = new ChangeUsername(this.currentUserName, this.userName);

    this.spinner.show();

    this._accountsService.changeUserName(request)
      .subscribe(
        (data) => {
          this.onRequestExcecuted();
          this.router.navigateByUrl('/accounts-settings');
        }
      )
  }

  onRequestExcecuted(){
    this.isLoaded = true;
    this.spinner.hide();
  }

  ngOnDestroy(): void {
    if (this.editUserNameSubscription) {
      this.editUserNameSubscription.unsubscribe();
    }
  }

}
