import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import CustomValidators from "../../validators/custom-validators";
import { Router } from "@angular/router";
import { HttpService } from "../../../../core/services/http/http-service";
import { StorageService } from "../../../../core/services/storage/storage-service";
import { SignUpRequest } from "../../models/sign-up-request";
import { NgxSpinnerService } from "ngx-spinner";

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SignUpComponent implements OnInit {
  isLoaded: boolean = false;
  signUpForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private spinner: NgxSpinnerService,
    private _http: HttpService,
    private _storage: StorageService
  ) { }

  ngOnInit(): void {
    this.signUpForm = this.formBuilder.group({
      firstName: ['', [Validators.required, Validators.minLength(2)]],
      lastName: ['', [Validators.required, Validators.minLength(2)]],
      userName: ['', [Validators.required, Validators.minLength(8)]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      repeatPassword: ['', [Validators.required, Validators.minLength(1)]]
    }, {
        validators: [CustomValidators.match('password', 'repeatPassword')]
    })
  }

  handleRegister(){
    let firstName = this.signUpForm.controls['firstName'].value;
    let lastName = this.signUpForm.controls['lastName'].value;
    let userName = this.signUpForm.controls['userName'].value;
    let password = this.signUpForm.controls['password'].value;

    let request = new SignUpRequest(firstName, lastName, userName, password);

    this.spinner.show();

    // @ts-ignore
    this._authService.signUp(request)
      .subscribe(
        (data: any) => {
          this._storage.storeData('auth', JSON.stringify(data));
          this.router.navigateByUrl('/polls');
          this.onRequestExcecuted();
        }
      );
  }

  onRequestExcecuted(){
    this.isLoaded = true;
    this.spinner.hide();
  }

}
