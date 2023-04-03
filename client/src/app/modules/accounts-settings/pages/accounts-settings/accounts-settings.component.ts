import {ChangeDetectionStrategy, Component, OnDestroy, OnInit} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { StorageService } from "../../../../core/services/storage/storage-service";
import { DeleteAccount } from "../../models/delete-account";
import { NgxSpinnerService } from "ngx-spinner";
import { AccountSettingsService } from "../../services/account-settings.service";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-accounts-settings',
  templateUrl: './accounts-settings.component.html',
  styleUrls: ['./accounts-settings.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AccountsSettingsComponent implements OnInit, OnDestroy {
  isDeleted: boolean = false;
  isLoaded: boolean = false;
  isEditUserNameShow: boolean = false;
  isEditPasswordShow: boolean = false;
  accountSettingForm!: FormGroup;
  accountSettingSubscription: Subscription;

  constructor(
    private formBuilder: FormBuilder,
    private _storage: StorageService,
    private  _accountsService: AccountSettingsService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.accountSettingForm = this.formBuilder.group({
      userName: ['test', [Validators.required, Validators.minLength(8)]],
      password: ['test', [Validators.required, Validators.minLength(8)]]
    })
  }

  ngOnDestroy(): void {
    if (this.accountSettingSubscription) {
      this.accountSettingSubscription.unsubscribe();
    }
  }

  onEditUserNameClick(){
    this.isEditUserNameShow = true;
  }

  onEditPasswordNameClick(){
    this.isEditPasswordShow = true;
  }

  onDeleteAccoount(){
      // @ts-ignore
    const data = JSON.parse(this._storage.getData('auth'));

    let request = new DeleteAccount(data?.userGid);

    this.spinner.show();

    this.accountSettingSubscription = this._accountsService.deleteAccount(request)
      .subscribe(
        (data: boolean) => {
          this.isDeleted = data;

          if(this.isDeleted === true){
            this.onRequestExcecuted();
            this.router.navigateByUrl("");
          }
        }
      )
  }

  onRequestExcecuted(){
    this.isLoaded = true;
    this.spinner.hide();
  }
}
