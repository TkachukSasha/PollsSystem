import {ChangeDetectionStrategy, Component, OnDestroy, OnInit} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { HttpService } from "../../../../core/services/http/http-service";
import { StorageService } from "../../../../core/services/storage/storage-service";
import { SignUpRequest } from "../../models/sign-up-request";
import { NgxSpinnerService } from "ngx-spinner";
import {AuthService} from "../../services/auth.service";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SignUpComponent implements OnInit, OnDestroy {
  isLoaded: boolean = false;
  signUpForm!: FormGroup;
  signUpSubscription: Subscription;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private spinner: NgxSpinnerService,
    private _storage: StorageService,
    private _authService: AuthService
  ) { }

  ngOnInit(): void {
    this.signUpForm = this.formBuilder.group({
      firstName: ['', [Validators.required, Validators.minLength(2)]],
      lastName: ['', [Validators.required, Validators.minLength(2)]],
      userName: ['', [Validators.required, Validators.minLength(6)]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      repeatPassword: ['', [Validators.required, Validators.minLength(1)]]
    });
  }

  ngOnDestroy() : void{
    if (this.signUpSubscription) {
      this.signUpSubscription.unsubscribe();
    }
  }

  handleRegister(){
    let firstName = this.signUpForm.controls['firstName'].value;
    let lastName = this.signUpForm.controls['lastName'].value;
    let userName = this.signUpForm.controls['userName'].value;
    let password = this.signUpForm.controls['password'].value;

    let request = new SignUpRequest(firstName, lastName, userName, password);

    console.log(`request: ${request}`)

    this.spinner.show();

    // @ts-ignore
    this.signUpSubscription = this._authService.signUp(request)
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
