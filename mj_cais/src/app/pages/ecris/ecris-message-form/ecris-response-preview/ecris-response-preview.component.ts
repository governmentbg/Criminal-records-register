import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";
import { NbDialogRef } from "@nebular/theme";
import { EcrisNotPreviewComponent } from "../ecris-not-preview/ecris-not-preview.component";
import { EcrisRequestPreviewService } from "../ecris-req-preview/_data/ecris-request-preview.service";

@Component({
  selector: "cais-ecris-response-preview",
  templateUrl: "./ecris-response-preview.component.html",
  styleUrls: ["./ecris-response-preview.component.scss"],
})
export class EcrisResponsePreviewComponent implements OnInit {
  displayTitle: string = 'Отговор';
  ecrisId: string;
  ecrisType: string;
  formGroup: FormGroup;
  constructor(
    private formBuilder: FormBuilder,
    private ecrisRequestPreviewService: EcrisRequestPreviewService,
    protected ref: NbDialogRef<EcrisResponsePreviewComponent>
  ) {}

  ngOnInit(): void {
    this.ecrisRequestPreviewService
      .getEcrisRequest(this.ecrisId, this.ecrisType)
      .subscribe((result) => {
        this.formGroup = this.buildFormImpl();
        this.formGroup.patchValue(result);
      });
  }

  buildFormImpl(): FormGroup {
    return this.formBuilder.group({
      id: [{ value: "", disabled: true }],
      ecrisId: [{ value: "", disabled: true }],

      firstName: [{ value: "", disabled: true }],
      middleName: [{ value: "", disabled: true }],
      lastName: [{ value: "", disabled: true }],
      lastNameSecond: [{ value: "", disabled: true }],
      fullName: [{ value: "", disabled: true }],
      nationality: [{ value: "", disabled: true }],
      countryPerson: [{ value: "", disabled: true }],
      municipalityPerson: [{ value: "", disabled: true }],
      cityPerson: [{ value: "", disabled: true }],
      personId: [{ value: "", disabled: true }],
      sex: [{ value: "", disabled: true }],
      birthday: [{ value: "", disabled: true }],
    });
  }

  public dialogSimpleCancelFunction = () => {
    this.ref.close();
  };
}
