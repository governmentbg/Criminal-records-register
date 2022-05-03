import { FormControl, FormGroup, Validators } from "@angular/forms";
import { DatePrecisionConstants } from "./date-precision.constants";

export class DatePrecisionModelForm {
  public group: FormGroup;
  public date: FormControl;
  public year: FormControl;
  public month: FormControl;
  public precision: FormControl;

  constructor(isRequired?: boolean, disabled?: boolean) {
    let validators = isRequired && !disabled ? [Validators.required] : [];
    this.precision = new FormControl(
      DatePrecisionConstants.fullDate.id,
      validators
    );
    this.date = new FormControl(null, validators);
    this.year = new FormControl(null, [
      Validators.maxLength(4),
      Validators.minLength(4),
    ]);
    this.month = new FormControl(null, [
      Validators.maxLength(1),
      Validators.minLength(2),
    ]);

    this.group = new FormGroup({
      precision: this.precision,
      date: this.date,
      year: this.year,
      month: this.month,
    });

    if (disabled) {
      this.group.disable();
    }
  }

  public getFullYear(): Date {
    if (this.precision.value == DatePrecisionConstants.fullDate.id) {
      return this.date.value;
    } else if (this.precision.value == DatePrecisionConstants.yearAndMonth.id) {
      let newDate = new Date(this.year.value, this.month.value, 1);
      return newDate;
    }
    if (this.precision.value == DatePrecisionConstants.fullDate.id) {
      let newDate = new Date(this.year.value, 1, 1);
      return newDate;
    }

    return null;
  }
}
