import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root",
})
export class DocTypeConstants {
  static get ECRIS() {
    return "5";
  }

  static get CBSHandwritten() {
    return "2";
  }

  public static defaultDialogConfig = {
    closeOnBackdropClick: false,
  };
}
