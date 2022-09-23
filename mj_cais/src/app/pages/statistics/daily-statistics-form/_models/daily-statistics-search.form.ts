import { FormControl, FormGroup, Validators } from "@angular/forms";
import { BaseForm } from "../../../../@core/models/common/base.form";

export class DailyStatisticsSearchForm extends BaseForm {
  public group: FormGroup;
  public fromDate: FormControl;
  public toDate: FormControl;
  public statisticsType: FormControl;
  public status: FormControl;

  constructor() {
    super();
    //todo add second dropdown values
    let dateDayBefore = new Date();
    dateDayBefore.setDate(dateDayBefore.getDate() - 1);
    this.fromDate = new FormControl(dateDayBefore);
    this.toDate = new FormControl(new Date());
    this.statisticsType = new FormControl(null, Validators.required);
    this.status = new FormControl();

    this.group = new FormGroup({
      fromDate: this.fromDate,
      toDate: this.toDate,
      statisticsType: this.statisticsType,
      status: this.status,
    });
  }
}
