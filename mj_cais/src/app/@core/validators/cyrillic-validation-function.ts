import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function createCyrillicValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    if (!value) return null;

    const isValid = /^[а-яА-Я\s]+$/.test(value);

    return !isValid ? { cyrillicValidation: true } : null;
  };
}
