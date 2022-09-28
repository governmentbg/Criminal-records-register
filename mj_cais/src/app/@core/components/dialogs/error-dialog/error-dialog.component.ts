import { Component, Input, OnInit } from "@angular/core";
import { NbDialogRef } from "@nebular/theme";

@Component({
  selector: "cais-error-dialog",
  templateUrl: "./error-dialog.component.html",
  styleUrls: ["./error-dialog.component.scss"],
})
export class ErrorDialogComponent implements OnInit {
  constructor(protected ref: NbDialogRef<ErrorDialogComponent>) {}
  public messages: string[] = [];
  public header: string;
  ngOnInit(): void {}

  success() {
    this.ref.close(true);
  }

  dismiss() {
    this.ref.close(false);
  }
}
