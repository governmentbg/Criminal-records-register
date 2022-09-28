import { Component, Input, OnInit } from "@angular/core";
import { NbDialogRef } from "@nebular/theme";

@Component({
  selector: "cais-confirm-dialog-component",
  templateUrl: "./confirm-dialog-component.component.html",
  styleUrls: ["./confirm-dialog-component.component.scss"],
})
export class ConfirmDialogComponent implements OnInit {
  constructor(protected ref: NbDialogRef<ConfirmDialogComponent>) {}

  @Input()
  color: "danger" | "success" | "primary" = "danger";
  public confirmMessage: string;
  public showHeder: boolean = true;

  ngOnInit(): void {}

  success() {
    this.ref.close(true);
  }

  dismiss() {
    this.ref.close(false);
  }
}
