import { Subscription } from 'rxjs';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { AuthService } from '../services/auth.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ValidatePasswords } from '../validators/equality.validator';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})

export class RegisterFormComponent {
  failed: boolean = false;
  clicked: boolean = false;
  form: FormGroup;

  constructor(private fb: FormBuilder,
    private authService: AuthService,
    private router: Router) {

    this.form = this.fb.group({
      email: ['', Validators.compose([Validators.required, Validators.email])],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      password: ['', Validators.required],
      confirmedPassword: ['', Validators.required]
    }, { validator: this.EqualityValidator });
  }

  EqualityValidator (control: FormGroup) {
    const val = control.value;
    let condition = val.password == val.confirmedPassword;
    if (!condition) {
      return { EqualityValidator: 'passwords do not match' }
    }
    return null;
  }

  register() {
    const val = this.form.value;
    if (this.form.valid) {
      this.clicked = true;
      this.failed = false;
      this.authService.register(val.email, val.firstName, val.lastName, val.password)
        .subscribe(
        result => {
            this.router.navigateByUrl('/');
          },
          error => {
            this.failed = true;
            this.clicked = false;
          }
        );
    }
  }
}
