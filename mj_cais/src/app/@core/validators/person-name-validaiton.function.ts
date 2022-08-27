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
    const isValid = (firstNameIsSet && familyNameIsSet) || fullNameIsSet;

    if (!isValid) {
      form.get(firstNameFiled).setErrors({ invalidPersonNames: true });
      form.get(familyNameFiled).setErrors({ invalidPersonNames: true });
      form.get(fullNameFiled).setErrors({ invalidPersonNames: true });
    } else {
      var firstNameControl = form.controls[firstNameFiled] as FormControl;
      if (firstNameControl.invalid) {
        firstNameControl.clearValidators();
        firstNameControl.setValidators([
          Validators.maxLength(200),
          createCyrillicValidator(),
        ]);
        firstNameControl.updateValueAndValidity();
      }

      var familyNameControl = form.controls[familyNameFiled] as FormControl;
      if (familyNameControl.invalid) {
        familyNameControl.clearValidators();
        familyNameControl.setValidators([
          Validators.maxLength(200),
          createCyrillicValidator(),
        ]);
        familyNameControl.updateValueAndValidity();
      }

      var fullNameControl = form.controls[fullNameFiled] as FormControl;
      if (fullNameControl.invalid) {
        fullNameControl.clearValidators();
        fullNameControl.setValidators([
          Validators.maxLength(200),
          createCyrillicValidator(),
        ]);
        fullNameControl.updateValueAndValidity();
      }

      // if (form.get(firstNameFiled).errors?.invalidPersonNames) {
      //   form.get(firstNameFiled).errors.invalidPersonNames = null;
      // }

      // if (form.get(familyNameFiled).errors?.invalidPersonNames) {
      //   form.get(familyNameFiled).errors.invalidPersonNames = null;
      // }

      // if (form.get(fullNameFiled).errors?.invalidPersonNames) {
      //   form.get(fullNameFiled).errors.invalidPersonNames = null;
      // }
    }

    return null;
  };
}
