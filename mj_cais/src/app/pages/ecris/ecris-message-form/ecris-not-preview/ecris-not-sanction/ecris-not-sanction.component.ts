import { Component, Input, OnInit } from "@angular/core";
import { FormGroup, FormBuilder } from "@angular/forms";

import { EcrisNotificationSanctionsModel } from "../_models/ecris-notification-preview.model";

@Component({
  selector: "cais-ecris-not-sanction",
  templateUrl: "./ecris-not-sanction.component.html",
  styleUrls: ["./ecris-not-sanction.component.scss"],
})
export class EcrisNotSanctionComponent implements OnInit {
  @Input()
  public sanction: any;

  formGroup: FormGroup;
  constructor(private formBuilder: FormBuilder) {}

  ngOnInit(): void {
    debugger;
    this.formGroup = this.buildFormImpl();
    this.formGroup.patchValue(this.sanction);
  }

  buildFormImpl(): FormGroup {
    return this.formBuilder.group({
      commonCategory: [{ value: "", disabled: true }],
      alternative: [{ value: "", disabled: true }],
      nationalCategoryTitle: [{ value: "", disabled: true }],
      convictionStartDate: [{ value: "", disabled: true }],
      convictionEndDate: [{ value: "", disabled: true }],
      convictionDuration: [{ value: "", disabled: true }],
      countryPerson: [{ value: "", disabled: true }],
      municipalityPerson: [{ value: "", disabled: true }],
      sanctionAmountOfIndividualFine: [{ value: "", disabled: true }],
      remarks: [{ value: "", disabled: true }],
      sanctionIsSpecificToMinor: [{ value: "", disabled: true }],
      sanctionNumberOfFines: [{ value: "", disabled: true }],
      sanctionCurrencyOfFine: [{ value: "", disabled: true }],
    });
  }
}
