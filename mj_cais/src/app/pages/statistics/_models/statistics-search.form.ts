import { FormControl, FormGroup } from "@angular/forms";

export class StatisticsSearchForm {
  public group: FormGroup;

  public fromDate: FormControl;
  public toDate: FormControl;
  public authority: FormControl;

  constructor() {
    this.fromDate =  new FormControl( new Date());
    let dateMonthAfter = new Date();
    dateMonthAfter.setMonth(dateMonthAfter.getMonth() + 1);
    this.toDate = new FormControl(dateMonthAfter);
    this.authority = new FormControl(null);

    this.group = new FormGroup({
      fromDate: this.fromDate,
      toDate: this.toDate,
      authority: this.authority,
    });
  }
}
