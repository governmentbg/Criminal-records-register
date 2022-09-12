import {
  FormControl,
  FormGroup,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from "@angular/forms";
import { createCyrillicValidator } from "./cyrillic-validation-function";

export function personNamesValidator(
  firstNameFiled: string,
  familyNameFiled: string,
  fullNameFiled: string
): ValidatorFn {
  return (form: FormGroup): ValidationErrors | null => {
    const regex = /^[а-яА-Я\s-]+$/;

    const firstNameValue = form.get(firstNameFiled).value;
    const familyNameValue = form.get(familyNameFiled).value;
    const fullNameNameValue = form.get(fullNameFiled).value;

    let firstNameIsSet =
      firstNameValue != undefined &&
      firstNameValue != null &&
      firstNameValue != "";
    let familyNameIsSet =
      familyNameValue != undefined &&
      familyNameValue != null &&
      familyNameValue != "";
    let fullNameIsSet =
      fullNameNameValue != undefined &&
      fullNameNameValue != null &&
      fullNameNameValue != "";

    let isValid = (firstNameIsSet && familyNameIsSet) || fullNameIsSet;
    if (isValid) {
      let invalidCyrillic = false;

      if (firstNameIsSet && !regex.test(firstNameValue)) {
        form.get(firstNameFiled).setErrors({ cyrillicValidation: true });
        invalidCyrillic = true;
      }

      if (familyNameIsSet && !regex.test(familyNameValue)) {
        form.get(familyNameFiled).setErrors({ cyrillicValidation: true });
        invalidCyrillic = true;
      }

      if (fullNameIsSet && !regex.test(fullNameNameValue)) {
        form.get(fullNameFiled).setErrors({ cyrillicValidation: true });
        invalidCyrillic = true;
      }

      if (!invalidCyrillic) {
        var firstNameControl = form.controls[firstNameFiled] as FormControl;
        var familyNameControl = form.controls[familyNameFiled] as FormControl;
        var fullNameControl = form.controls[fullNameFiled] as FormControl;

        if (firstNameControl.invalid) {
          firstNameControl.clearValidators();
          firstNameControl.updateValueAndValidity();
        }

        if (familyNameControl.invalid) {
          familyNameControl.clearValidators();
          familyNameControl.updateValueAndValidity();
        }

        if (fullNameControl.invalid) {
          fullNameControl.clearValidators();
          fullNameControl.updateValueAndValidity();
        }
      }

      return null;
    }

    if (!isValid) {
      form.get(firstNameFiled).setErrors({ invalidPersonNames: true });
      form.get(familyNameFiled).setErrors({ invalidPersonNames: true });
      form.get(fullNameFiled).setErrors({ invalidPersonNames: true });
    }

    return null;
  };
}
