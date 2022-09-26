import { Injectable } from "@angular/core";
import { IgxGridRowComponent } from "@infragistics/igniteui-angular";
import { DatePrecisionConstants } from "../../components/forms/inputs/date-precision/_models/date-precision.constants";
import { CommonConstants } from "../../constants/common.constants";

@Injectable({
  providedIn: "root",
})
export class DateFormatService {
  constructor() {}

  public formatDate(val: Date): any {
    const options: Intl.DateTimeFormatOptions = {
      year: "numeric",
      month: "numeric",
      day: "numeric",
    };

    let locale = CommonConstants.bgLocale;
    return new Intl.DateTimeFormat(locale, options).format(val);
  }

  public formatDateTime(val: Date): any {
    const options: Intl.DateTimeFormatOptions = {
      year: "numeric",
      month: "numeric",
      day: "numeric",
      hour: "numeric",
      minute: "numeric",
      second: "numeric",
    };

    let locale = CommonConstants.bgLocale;
    return new Intl.DateTimeFormat(locale, options).format(val);
  }

  public displayDate(val: string): string {
    if (!val) {
      return "";
    }

    let date = new Date(val);

    let result = date.toLocaleDateString(CommonConstants.bgLocale);
    return result;
  }

  public displayDateTime(val: string): string {
    if (!val) {
      return "";
    }

    let date = new Date(val);
    let result = date.toLocaleString(CommonConstants.bgLocale);
    return result;
  }

  public displayDateTimeWithPrecision(val: string, precision: string): string {
    return this.getDisplayDateWithPrecision(val, precision, true);
  }

  public displayDateWithPrecision(val: string, precision: string): string {
    return this.getDisplayDateWithPrecision(val, precision, false);
  }

  private getDisplayDateWithPrecision(
    val: string,
    precision: string,
    withTime: boolean
  ): string {
    if (!val) {
      return "";
    }

    let date = new Date(val);
    if (precision == DatePrecisionConstants.year.id) {
      return date.getFullYear().toString();
    }

    if (precision == DatePrecisionConstants.yearAndMonth.id) {
      return date.getMonth() + "." + date.getFullYear();
    }

    if (withTime) {
      return date.toLocaleString(CommonConstants.bgLocale);
    }

    return date.toLocaleDateString(CommonConstants.bgLocale);
  }

  public parseDatesFromGridRow(
    event: IgxGridRowComponent,
    datePropNames: string[]
  ) {
    let rowData = event.rowData;

    for (let prop of datePropNames) {
      if (rowData[prop] && typeof rowData[prop] !== "object") {
        let localDate = new Date(rowData[prop]);
        event.rowData[prop] = localDate;
      }
    }
  }
}
