import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { ILogin } from 'src/app/models/login';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent implements OnInit {

  registerForm: FormGroup;
  returnUrl: string;
  message: string;

  constructor(private formBuilder: FormBuilder,
              private router: Router,
              private authService: AuthService) { }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(4)]]
    });
    this.returnUrl = '/home-page';
    this.authService.logout();
  }

  // convenience getter for easy access to form fields
  get f() { return this.registerForm.controls; }
  get email() {
    return this.registerForm.get('email');
  }
  get password() {
    return this.registerForm.get('password');
  }

  public register() {
    // stop here if form is invalid
    if (this.registerForm.invalid) {
        return;
    } else {
      const model: ILogin = { email: this.f.email.value, password: this.f.password.value };
      this.authService.registerUser(model).subscribe(response => {
        this.message = response.message;
      }, (err: any) => {
        this.message = 'Register problem';
      });
    }
  }

  public backToLogin() {
    this.router.navigate(['/login']);
  }
}
