import { Component, Input, OnInit } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";

import { EApplicationCertificateResultModel } from "./_data/e-application-certificate-result.model";

@Component({
  selector: "cais-e-application-certificate-result",
  templateUrl: "./e-application-certificate-result.component.html",
  styleUrls: ["./e-application-certificate-result.component.scss"],
})
export class EApplicationCertificateResultComponent implements OnInit {
  @Input()
  eAppCert: EApplicationCertificateResultModel;

  formGroup: FormGroup;
  constructor(private formBuilder: FormBuilder) {}

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
    });
  }

  downloadContent(){

  }
}
