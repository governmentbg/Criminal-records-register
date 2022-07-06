import { FormControl, FormGroup } from "@angular/forms";

export class StatisticsSearchForm {
  public group: FormGroup;

  public fromDate: FormControl;
  public toDate: FormControl;
  public authority: FormControl;

  constructor() {
    let dateMonthBefore = new Date();
    dateMonthBefore.setMonth(dateMonthBefore.getMonth() - 1);
    this.fromDate = new FormControl(dateMonthBefore);
    this.toDate = new FormControl(new Date());
    this.authority = new FormControl(null);

    this.group = new FormGroup({
      fromDate: this.fromDate,
      toDate: this.toDate,
      authority: this.authority,
    });
  }
}
