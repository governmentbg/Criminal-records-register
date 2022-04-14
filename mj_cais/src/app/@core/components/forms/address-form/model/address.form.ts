import { FormControl, FormGroup, Validators } from "@angular/forms";
import { LookupForm } from "../../inputs/lookup/models/lookup.form";

export class AddressForm {
  public isSetForNativeAddress: boolean = true;
  public isSetForForeignAddress: boolean = false;
  private isDisabled: boolean = false;

  public group: FormGroup;

  public municipalityId: FormControl;
  public districtId: FormControl;
  public cityId: FormControl;
  public foreignCountryAddress: FormControl;
  public country: LookupForm;

  constructor(isRequired?: boolean, isDisabled?: boolean) {
    this.isDisabled = isDisabled;
    this.country = new LookupForm(isRequired);
    let validators =
      isRequired && (!isDisabled || isDisabled == null)
        ? [Validators.required]
        : [];

    this.municipalityId = new FormControl(null, validators);
    this.districtId = new FormControl(null, validators);
    this.cityId = new FormControl(null, validators);
    this.foreignCountryAddress = new FormControl(null, validators);

    if (isDisabled) {
      this.country = new LookupForm(false, true);
      this.municipalityId.disable();
      this.districtId.disable();
      this.cityId.disable();
      this.foreignCountryAddress.disable();
    }

    this.group = new FormGroup({
      country: this.country.group,
      districtId: this.districtId,
      municipalityId: this.municipalityId,
      cityId: this.cityId,
      foreignCountryAddress: this.foreignCountryAddress,
    });
  }

  public setForForeignAddress() {
    if (this.isDisabled) {
      this.foreignCountryAddress.disable();
      this.districtId.disable();
      this.municipalityId.disable();
      this.cityId.disable();
    } else {
      this.foreignCountryAddress.enable();
      this.districtId.disable();
      this.municipalityId.disable();
      this.cityId.disable();
    }

    this.districtId.setValue(null);
    this.municipalityId.setValue(null);
    this.cityId.setValue(null);

    this.isSetForNativeAddress = false;
    this.isSetForForeignAddress = true;
  }

  public setForNativeAddress() {
    if (this.isDisabled) {
      this.districtId.disable();
      this.municipalityId.disable();
      this.cityId.disable();
      this.foreignCountryAddress.disable();
    } else {
      this.districtId.enable();
      this.municipalityId.enable();
      this.cityId.enable();

      this.foreignCountryAddress.disable();
    }

    this.foreignCountryAddress.setValue(null);
    this.isSetForNativeAddress = true;
    this.isSetForForeignAddress = false;
  }
}
