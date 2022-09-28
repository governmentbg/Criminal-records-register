import {
  FormControl,
  FormGroup,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from "@angular/forms";

export function timeSpanValidator(
  maxMonths: number = 1,
  fromDateField: string = "fromDate",
  toDateField: string = "toDate",
  validatorsArr: [any] = [Validators.required]
): ValidatorFn {
  return (group: FormGroup): ValidationErrors | null => {
    const [fromDateValue, toDateValue] = [
      group.get(fromDateField)!.value != null
        ? new Date(group.get(fromDateField)!.value)
        : null,
      group.get(toDateField)!.value != null
        ? new Date(group.get(toDateField)!.value)
        : null,
    ];

    function manageValidators(controlName: string, validatorsArray: [any]) {
      var fromDateControl = group.controls[controlName] as FormControl;
      if (fromDateControl.invalid) {
        fromDateControl.clearValidators();
        fromDateControl.setValidators(validatorsArray);
        fromDateControl.updateValueAndValidity();
      }
    }

    if (fromDateValue == null || toDateValue == null) {
      if (group.get(fromDateField).getError("timeSpan") == true) {
        manageValidators(fromDateField, validatorsArr);
      }

      return null;
    }

    var minMonth: Date = toDateValue;
    minMonth.setMonth(toDateValue.getMonth() - maxMonths);

    if (fromDateValue < minMonth && fromDateValue != null) {
      group.get(fromDateField).setErrors({ timeSpan: true });
    } else {
      manageValidators(fromDateField, validatorsArr);

      return null;
    }

    return fromDateValue < minMonth && fromDateValue != null
      ? {
          timeSpan: {
            value: "Дата не може да бъде с повече от месец назад!",
          },
        }
      : null;
  };
}
