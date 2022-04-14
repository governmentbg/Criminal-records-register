import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root",
})
export class DocTypeConstants {
  static get ecris() {
    return "5";
  }

  public static defaultDialogConfig = {
    closeOnBackdropClick: false,
  };
}
