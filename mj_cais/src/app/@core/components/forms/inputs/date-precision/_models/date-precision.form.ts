import { FormControl, FormGroup, Validators } from "@angular/forms";

export class DatePrecisionModelForm {
  public group: FormGroup;

  public date: FormControl;
  //public yearAndMonth: FormControl;
  // public year: FormControl;
  public precision: FormControl;

  constructor(isRequired?: boolean, disabled?: boolean) {
    let validators = isRequired && !disabled ? [Validators.required] : [];
    //this.fullDate = new FormControl();
    //this.yearAndMonth = new FormControl();
    // this.year = new FormControl();
    this.precision = new FormControl(validators);
    this.date = new FormControl(validators);

    this.group = new FormGroup({
      // fullDate: this.fullDate,
      //yearAndMonth: this.yearAndMonth,
      //year: this.year,
      date: this.date,
      precision: this.precision,
    });

    if (disabled) {
      this.group.disable();
    }
  }
}
