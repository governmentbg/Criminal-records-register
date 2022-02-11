import { Injectable } from "@angular/core";
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
}
