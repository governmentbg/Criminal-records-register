import { Component, OnInit } from "@angular/core";
import * as fileSaver from "file-saver";
import { RoleNameEnum } from "../../@core/constants/role-name.enum";
import { CustomToastrService } from "../../@core/services/common/custom-toastr.service";
import { HelpService } from "./_data/help.service";

@Component({
  selector: "cais-help",
  templateUrl: "./help.component.html",
  styleUrls: ["./help.component.scss"],
})
export class HelpComponent implements OnInit {
  constructor(
    public toastr: CustomToastrService,
    public service: HelpService
  ) {}

  public RoleNameEnum = RoleNameEnum;

  ngOnInit(): void {}

  getFileForCbs() {
    let action = this.service.downloadContentCbs();
    this.subcribeAction(action);
  }

  getFileForEmployee() {
    let action = this.service.downloadContentEmployee();
    this.subcribeAction(action);
  }

  getFileForAdministration() {
    let action = this.service.downloadContentAdministration();
    this.subcribeAction(action);
  }

  getFileForJudge() {
    let action = this.service.downloadContentJudge();
    this.subcribeAction(action);
  }

  private subcribeAction(action) {
    action.subscribe({
      next: (response) => {
        this.getFileContent(response);
      },
      error: (errorResponse) => {
        this.onError(errorResponse);
      },
    });
  }

  private getFileContent(response) {
    let blob = new Blob([response.body]);
    window.URL.createObjectURL(blob);

    let header = response.headers.get("Content-Disposition");
    let filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;

    let fileName = "download";

    var matches = filenameRegex.exec(header);
    if (matches != null && matches[1]) {
      fileName = matches[1].replace(/['"]/g, "");
    }

    fileSaver.saveAs(blob, fileName);
  }

  private onError(error) {
    var errorText = error.status + " " + error.statusText;
    this.toastr.showBodyToast(
      "danger",
      "Грешка при изтегляне на файл:",
      errorText
    );
  }
}
