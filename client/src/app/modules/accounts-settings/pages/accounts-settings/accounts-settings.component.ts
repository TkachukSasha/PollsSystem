import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-accounts-settings',
  templateUrl: './accounts-settings.component.html',
  styleUrls: ['./accounts-settings.component.scss']
})
export class AccountsSettingsComponent implements OnInit {
  isEditUserNameShow: boolean = false;
  isEditPasswordShow: boolean = false;
  accountSettingForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {
    this.accountSettingForm = this.formBuilder.group({
      userName: ['test', [Validators.required, Validators.minLength(8)]],
      password: ['test', [Validators.required, Validators.minLength(8)]]
    })
  }

  handleUpdate(){
    console.log("update");
  }

  onEditUserNameClick(){
    this.isEditUserNameShow = true;
  }

  onEditPasswordNameClick(){
    this.isEditPasswordShow = true;
  }
}
