import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";
import { LnchUtils } from "../utils/lnch.utils";

export function createLnchValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    if (!value) return null;

    let isValid = LnchUtils.isValid(value);
    return !isValid ? { lnchValidation: true } : null;
  };
}
