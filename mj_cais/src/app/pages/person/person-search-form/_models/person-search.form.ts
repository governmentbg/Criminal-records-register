import { FormControl, FormGroup } from "@angular/forms";
import { AddressForm } from "../../../../@core/components/forms/address-form/_model/address.form";
import { DatePrecisionModelForm } from "../../../../@core/components/forms/inputs/date-precision/_models/date-precision.form";

export class PersonSearchForm {
  public group: FormGroup;

  public id: FormControl;
  public firstname: FormControl;
  public surname: FormControl;
  public familyname: FormControl;
  public fullname: FormControl;
  public pid: FormControl;
  public pidType: FormControl;
  public birthDate: DatePrecisionModelForm;
  public sex: FormControl;
  public birthPlace: AddressForm;

  constructor() {
    this.id = new FormControl(null);
    this.firstname = new FormControl(null);
    this.surname = new FormControl(null);
    this.familyname = new FormControl(null);
    this.fullname = new FormControl(null);
    this.pid = new FormControl(null);
    this.pidType = new FormControl(null);
    this.birthDate = new DatePrecisionModelForm(null);
    this.sex = new FormControl(null);
    this.birthPlace = new AddressForm(false);

    this.group = new FormGroup({
      id: this.id,
      firstname: this.firstname,
      surname: this.surname,
      familyname: this.familyname,
      fullname: this.fullname,
      pid: this.pid,
      pidType: this.pidType,
      birthDate: this.birthDate.group,
      sex: this.sex,
      birthPlace: this.birthPlace.group,
    });
  }
}
