import { FormControl, FormGroup, Validators } from "@angular/forms";

export class DatePrecisionModelForm {
  public group: FormGroup;
  public date: FormControl;
  public year: FormControl;
  public month: FormControl;
  public precision: FormControl;

  constructor(isRequired?: boolean, disabled?: boolean) {
    let validators = isRequired && !disabled ? [Validators.required] : [];
    this.precision = new FormControl(validators);
    this.date = new FormControl(validators);
    this.year = new FormControl(null, [
      Validators.maxLength(4),
      Validators.minLength(4),
    ]);
    this.month = new FormControl(
      Validators.maxLength(1),
      Validators.minLength(2)
    );

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
}
