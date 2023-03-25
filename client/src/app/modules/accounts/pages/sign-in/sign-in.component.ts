import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { StorageService } from "../../../../core/services/storage/storage-service";
import { Router } from "@angular/router";
import { SignInRequest} from "../../models/sign-in-request";
import { NgxSpinnerService } from "ngx-spinner";
import { AuthService } from "../../services/auth.service";

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SignInComponent implements OnInit {
  isLoaded: boolean = false;
  signInForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private spinner: NgxSpinnerService,
    private _authService: AuthService,
    private _storage: StorageService
  ) { }

  ngOnInit(): void {
    this.signInForm = this.formBuilder.group({
      userName: ['', [Validators.required, Validators.minLength(8)]],
      password: ['', [Validators.required, Validators.minLength(8)]]
    })
  }

  handleLogin(): any{
    let userName = this.signInForm.controls['userName'].value;
    let password = this.signInForm.controls['password'].value;

    let request = new SignInRequest(userName, password);

    this.spinner.show();

    this._authService.signIn(request)
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
