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
  clicked: boolean = false;
  form: FormGroup;

  constructor(private fb: FormBuilder,
    private authService: AuthService,
    private router: Router) {
    this.form = this.fb.group({
      email: ['', Validators.compose([Validators.required, Validators.email])],
      password: ['', Validators.required]
    });
  }

  login() {
    const val = this.form.value;

    if (this.form.valid) {
      this.failed = false;
      this.clicked = true;
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
          this.clicked = false;
        }
        );
    }
  }
}
