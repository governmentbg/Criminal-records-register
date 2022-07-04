import { FormControl, FormGroup } from "@angular/forms";

export class StatisticsSearchForm {
  public group: FormGroup;

  public fromDate: FormControl;
  public toDate: FormControl;
  public authorityId: FormControl;

  constructor() {
    this.fromDate = new FormControl(null);
    this.toDate = new FormControl(null);
    this.authorityId = new FormControl(null);

    this.group = new FormGroup({
      fromDate: this.fromDate,
      toDate: this.toDate,
      authorityId: this.authorityId,
    });
  }
}
