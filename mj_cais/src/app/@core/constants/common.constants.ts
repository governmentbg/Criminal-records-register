import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root",
})
export class CommonConstants {
  static get bgLocale() {
    return "bg-BG";
  }

  static get bgCountryId(){
    return "CO-00-100-BGR";
  }

  static get bgCountryName(){
    return "България";
  }

  static get localDateFormat(){
    return 'dd.MM.yyyy г'
  }

  public static defaultDialogConfig = {
    closeOnBackdropClick: false,
  };
}
