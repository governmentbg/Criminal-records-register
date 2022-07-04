import { FormControl, FormGroup } from "@angular/forms";
import { AddressForm } from "../../../../@core/components/forms/address-form/_model/address.form";
import { LookupForm } from "../../../../@core/components/forms/inputs/lookup/models/lookup.form";

export class ReportPersonSearchForm {
  public group: FormGroup;

  public firstname: FormControl;
  public surname: FormControl;
  public familyname: FormControl;
  public egn: FormControl;
  public lnch: FormControl;
  public birthDate: FormControl;
  public birthPlace: AddressForm;
  public sex: FormControl;
  public idDocNumber: FormControl;
  public idDocIssuingDate: FormControl;
  public idDocValidDate: FormControl;
  public fromDate: FormControl;
  public toDate: FormControl;
  public nationalityTypeCode: FormControl;
  public nationalityCountryId : FormControl;
  public nationalityCountry: LookupForm;

  constructor() {
    this.firstname = new FormControl(null);
    this.surname = new FormControl(null);
    this.familyname = new FormControl(null);
    this.egn = new FormControl(null);
    this.lnch = new FormControl(null);
    this.birthDate = new FormControl(null);
    this.birthPlace = new AddressForm();
    this.sex = new FormControl(null);
    this.idDocNumber = new FormControl(null);
    this.idDocIssuingDate = new FormControl(null);
    this.idDocValidDate = new FormControl(null);
    this.fromDate = new FormControl(null);
    this.toDate = new FormControl(null);
    this.nationalityTypeCode = new FormControl(null);
    this.nationalityCountryId = new FormControl(null);
    this.nationalityCountry = new LookupForm(null);

    this.group = new FormGroup({
      firstname: this.firstname,
      surname: this.surname,
      familyname: this.familyname,
      egn: this.egn,
      lnch: this.lnch,
      birthDate: this.birthDate,
      sex: this.sex,
      idDocNumber: this.idDocNumber,
      idDocIssuingDate: this.idDocIssuingDate,
      idDocValidDate: this.idDocValidDate,
      fromDate: this.fromDate,
      toDate: this.toDate,
      nationalityTypeCode: this.nationalityTypeCode,
      nationalityCountryId: this.nationalityCountryId,
      nationalityCountry: this.nationalityCountry.group
    });
  }
}
