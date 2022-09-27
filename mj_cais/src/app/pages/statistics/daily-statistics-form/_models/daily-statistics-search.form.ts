import {
  AbstractControl,
  FormControl,
  FormGroup,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from "@angular/forms";
import { BaseForm } from "../../../../@core/models/common/base.form";

export function timeSpanValidator(maxMonths: number = 1): ValidatorFn {
  return (group: FormGroup): ValidationErrors | null => {
    const [fromDate, toDate] = [
      group.get("fromDate")!.value != null
        ? new Date(group.get("fromDate")!.value)
        : null,
      group.get("toDate")!.value != null
        ? new Date(group.get("toDate")!.value)
        : null,
    ];

    if (fromDate == null || toDate == null) {
      var fromDateControl = group.controls["fromDate"] as FormControl;
      if (fromDateControl.invalid) {
        fromDateControl.clearValidators();
        fromDateControl.setValidators([Validators.required]);
        fromDateControl.updateValueAndValidity();
      }

      return null;
    }

    var minMonth: Date = toDate;
    //previousMonth.setHours(23, 59, 59);
    minMonth.setMonth(toDate.getMonth() - maxMonths);

    if (fromDate < minMonth && fromDate != null) {
      group.get("fromDate").setErrors({ timeSpan: true });
    } else {
      var fromDateControl = group.controls["fromDate"] as FormControl;
      if (fromDateControl.invalid) {
        fromDateControl.clearValidators();
        fromDateControl.setValidators([Validators.required]);
        fromDateControl.updateValueAndValidity();
      }

      return null;
    }

    return fromDate < minMonth && fromDate != null
      ? {
          timeSpan: {
            value: "Дата не може да бъде с повече от месец назад!",
          },
        }
      : null;
  };
}

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
      [timeSpanValidator(2)]
    );
  }
}
