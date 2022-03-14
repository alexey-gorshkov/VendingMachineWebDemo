import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { ILogin } from 'src/app/models/login';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth-page',
  templateUrl: './auth-page.component.html',
  styleUrls: ['./auth-page.component.css']
})
export class AuthPageComponent implements OnInit {

  loginForm: FormGroup;
  returnUrl: string;
  message: string;
  currentDate: Date;

  constructor(private formBuilder: FormBuilder,
              private router: Router,
              private authService: AuthService) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      email: ['testuser@testuser.com', [Validators.required, Validators.email]],
      password: ['testuser', [Validators.required, Validators.minLength(4)]]
    });
    this.returnUrl = '/home-page';
    this.authService.logout();
    this.currentDate = new Date();
  }

  // convenience getter for easy access to form fields
  get f() { return this.loginForm.controls; }
  get email() {
    return this.loginForm.get('email');
  }
  get password() {
    return this.loginForm.get('password');
  }

  login() {
    // stop here if form is invalid
    if (this.loginForm.invalid) {
        return;
    } else {
      const model: ILogin = { email: this.f.email.value, password: this.f.password.value };
      this.authService.getToken(model).subscribe(response => {
        if (response.isSuccess) {
          localStorage.setItem('isLoggedIn', 'true');
          localStorage.setItem('token', response.token);
          const expiresDate = new Date();
          expiresDate.setSeconds(expiresDate.getSeconds() + response.expiresIn);
          localStorage.setItem('expiresDate',  expiresDate.toString());
          this.router.navigate([this.returnUrl]);
        }
        this.message = response.message;
      }, (err: any) => {
        this.message = 'Login problem';
      });
    }
  }

  public register() {
    this.router.navigate(['/register']);
  }
}
