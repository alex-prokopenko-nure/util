import { AbstractControl } from '@angular/forms';

export function ValidatePasswords(control: AbstractControl, pass: string) {
  if (!(control.value == pass)) {
    return { validPassword: true };
  }
  return null;
}
