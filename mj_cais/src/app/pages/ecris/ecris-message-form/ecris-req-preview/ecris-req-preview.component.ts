import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";
import { NbDialogRef } from "@nebular/theme";
import { EcrisRequestPreviewService } from "./_data/ecris-request-preview.service";

@Component({
  selector: "cais-ecris-req-preview",
  templateUrl: "./ecris-req-preview.component.html",
  styleUrls: ["./ecris-req-preview.component.scss"],
})
export class EcrisReqPreviewComponent implements OnInit {
  displayTitle: string = 'Запитване';
  ecrisId: string;
  ecrisType: string;
  formGroup: FormGroup; 
  constructor(
    private formBuilder: FormBuilder,
    private ecrisRequestPreviewService: EcrisRequestPreviewService,
    protected ref: NbDialogRef<EcrisReqPreviewComponent>,
  ) {}

  ngOnInit(): void {
   debugger
    this.ecrisRequestPreviewService
      .getEcrisRequest(this.ecrisId,this.ecrisType)
      .subscribe((result) => {
        debugger;
        this.formGroup = this.buildFormImpl();
        this.formGroup.patchValue(result);
      });
  }

  buildFormImpl(): FormGroup {
    return this.formBuilder.group({
      id: [{ value: "", disabled: true }],
      ecrisId: [{ value: "", disabled: true }],
      sendingMemberState: [{ value: "", disabled: true }],
      receivingMemberState: [{ value: "", disabled: true }],
      requestAuthorityName: [{ value: "", disabled: true }],
      requestAuthorityType: [{ value: "", disabled: true }],
      requestAuthorityCode: [{ value: "", disabled: true }],
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
      firstNameFormer: [{ value: "", disabled: true }],
      middleNameFormer: [{ value: "", disabled: true }],
      lastNameFormer: [{ value: "", disabled: true }],
      country: [{ value: "", disabled: true }],
      municipality: [{ value: "", disabled: true }],
      city: [{ value: "", disabled: true }],
      street: [{ value: "", disabled: true }],
      postCode: [{ value: "", disabled: true }],
      fullAdress: [{ value: "", disabled: true }],
      adressNumber: [{ value: "", disabled: true }],
      requestPurposeCategory: [{ value: "", disabled: true }],
      requestPurpose: [{ value: "", disabled: true }],
      concernedPersonConsent: [{ value: "", disabled: true }],
      messageUrgency: [{ value: "", disabled: true }],
      accusationOffenceCategory: [{ value: "", disabled: true }],
      messageAccusation: [{ value: "", disabled: true }],
      caseRefereranceNumber: [{ value: "", disabled: true }],
    });
  }

  public dialogSimpleCancelFunction = () => {
    this.ref.close();
  };
}
