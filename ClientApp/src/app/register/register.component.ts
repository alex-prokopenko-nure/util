import { Subscription } from 'rxjs';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { Credentials } from '../credentials.interface';
import { AuthService } from '../auth.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})

export class RegisterFormComponent {
  failed: boolean = false;
  form: FormGroup;

  constructor(private fb: FormBuilder,
    private authService: AuthService,
    private router: Router) {

    this.form = this.fb.group({
      email: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      password: ['', Validators.required],
      confirmedPassword: ['', Validators.required]
    });
  }

  register() {
    const val = this.form.value;

    if (val.email && val.firstName && val.lastName && val.password && val.password == val.confirmedPassword) {
      this.authService.register(val.email, val.firstName, val.lastName, val.password)
        .subscribe(
        result => {
          localStorage.setItem('registered', result.toString());
            this.router.navigateByUrl('/');
          },
          error => {
            this.failed = true;
          }
        );
    }
  }
}
