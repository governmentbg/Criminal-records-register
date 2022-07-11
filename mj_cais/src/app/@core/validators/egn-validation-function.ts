import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";
import { EgnUtils } from "../utils/egn.utils";

export function createEgnValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    if (!value) return null;

    let isValid = EgnUtils.isValid(value);
    return !isValid ? { egnValidation: true } : null;
  };
}
