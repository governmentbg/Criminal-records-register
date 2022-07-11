import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function createEmailValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    if (!value) return null;

    const isValid = /^[a-z0-9._%+-]+@[a-z0-9.-]+.[a-z]{2,4}$/.test(value);

    return !isValid ? { emailValidation: true } : null;
  };
}
