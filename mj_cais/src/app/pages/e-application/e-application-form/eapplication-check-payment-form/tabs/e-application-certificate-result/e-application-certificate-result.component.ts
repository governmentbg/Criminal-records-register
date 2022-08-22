import { Component, Input, OnInit } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";
import * as fileSaver from "file-saver";
import { CustomToastrService } from "../../../../../../@core/services/common/custom-toastr.service";

import { EApplicationCertificateResultModel } from "./_data/e-application-certificate-result.model";
import { WCertificateService } from "./_data/w-certificate.service";

@Component({
  selector: "cais-e-application-certificate-result",
  templateUrl: "./e-application-certificate-result.component.html",
  styleUrls: ["./e-application-certificate-result.component.scss"],
})
export class EApplicationCertificateResultComponent implements OnInit {
  @Input()
  eAppCert: EApplicationCertificateResultModel;

  formGroup: FormGroup;
  constructor(
    private formBuilder: FormBuilder,
    private wCertificateService: WCertificateService,
    private toastr: CustomToastrService
  ) {}

  ngOnInit(): void {
    debugger;
    this.formGroup = this.buildFormImpl();
    this.formGroup.patchValue(this.eAppCert);
  }

  buildFormImpl(): FormGroup {
    return this.formBuilder.group({
      registrationNumber: [{ value: null, disabled: true }],
      accessCode1: [{ value: "", disabled: true }],
      validFrom: [{ value: "", disabled: true }],
      validTo: [{ value: "", disabled: true }],
      wApplId: [{ value: "", disabled: true }],
    });
  }

  downloadContent() {
    this.wCertificateService
      .getWCertificateContentByAppId(this.eAppCert.wApplId)
      .subscribe((response: any) => {
        //this.fullForm.group.disable();
        debugger
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
      }),
      (error) => {
        var errorText = error.status + " " + error.statusText;
        this.toastr.showBodyToast(
          "danger",
          "Грешка при печат на свидетелство:",
          errorText
        );
      };
  }
}
