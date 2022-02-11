import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root",
})
export class CommonConstants {
  static get bgLocale() {
    return "bg-BG";
  }

  public static defaultDialogConfig = {
    closeOnBackdropClick: false,
  };
}
