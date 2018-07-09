import { Subscription } from 'rxjs';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { AuthService } from '../services/auth.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})

export class LoginFormComponent {
  registered: boolean = false;
  failed: boolean = false;
  form: FormGroup;

  constructor(private fb: FormBuilder,
    private authService: AuthService,
    private router: Router) {
    this.registered = localStorage.getItem('registered') != null;

    this.form = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  login() {
    if (localStorage.getItem('registered'))
      localStorage.removeItem('registered');
    const val = this.form.value;

    if (val.email && val.password) {
      this.authService.login(val.email, val.password)
        .subscribe(
        result => {
          localStorage.setItem('user_name', result.appUser.firstName + ' ' + result.appUser.lastName); 
          localStorage.setItem('auth_token', result.token); 
          console.log("User is logged in");
          this.router.navigateByUrl('/tour');
        },
        error => {
          this.failed = true;
        }
        );
    }
  }
}
