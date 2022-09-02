import { Component, Input, OnInit } from "@angular/core";
import { NbDialogRef } from "@nebular/theme";
import { ConfirmDialogComponent } from "../confirm-dialog-component/confirm-dialog-component.component";

@Component({
  selector: "cais-confirm-template-dialog",
  templateUrl: "./confirm-template-dialog.component.html",
  styleUrls: ["./confirm-template-dialog.component.scss"],
})
export class ConfirmTemplateDialogComponent implements OnInit {
  constructor(protected ref: NbDialogRef<ConfirmDialogComponent>) {}
  @Input()
  color: "danger" | "success";
  public title: string;
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
