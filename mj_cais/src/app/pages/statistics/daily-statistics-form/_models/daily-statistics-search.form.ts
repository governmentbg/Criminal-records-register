import { FormControl, FormGroup, Validators } from "@angular/forms";
import { BaseForm } from "../../../../@core/models/common/base.form";
import { timeSpanValidator } from "../../../../@core/validators/timespan-validator-function";

export class DailyStatisticsSearchForm extends BaseForm {
  public group: FormGroup;
  public fromDate: FormControl;
  public toDate: FormControl;
  public statisticsType: FormControl;
  public status: FormControl;

  constructor() {
    super();
    //fromDate is current date -1
    this.fromDate = new FormControl(
      new Date().setDate(new Date().getDate() - 1),
      Validators.required
    );
    this.toDate = new FormControl(new Date(), Validators.required);
    this.statisticsType = new FormControl(null, Validators.required);
    this.status = new FormControl();

    this.group = new FormGroup(
      {
        fromDate: this.fromDate,
        toDate: this.toDate,
        statisticsType: this.statisticsType,
        status: this.status,
      },
      [timeSpanValidator()]
    );
  }
}
