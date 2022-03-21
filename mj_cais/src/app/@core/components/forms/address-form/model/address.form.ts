import { FormControl, FormGroup, Validators } from "@angular/forms";
import { AddressModel } from "./address.model";

export class AddressForm {
  public isSetForNativeAddress: boolean = false;

  public group: FormGroup;

  public countryId: FormControl;
  public municipalityId: FormControl;
  public districtId: FormControl;
  public cityId: FormControl;

  public mapFromAddressModel(address: AddressModel) {
    this.countryId.setValue(address.countryId);
    this.municipalityId.setValue(address.municipalityId);
    this.districtId.setValue(address.districtId);
    this.cityId.setValue(address.cityId);
  }

  constructor() {
    this.countryId = new FormControl(null, [Validators.required]);
    this.municipalityId = new FormControl(null, [Validators.required]);
    this.districtId = new FormControl(null, [Validators.required]);
    this.cityId = new FormControl(null, [Validators.required]);

    this.group = new FormGroup({
      countryId: this.countryId,
      districtId: this.districtId,
      municipalityId: this.municipalityId,
      cityId: this.cityId,
    });
  }

  public setForForeignAddress() {
    this.districtId.disable();
    this.districtId.setValue(null);

    this.municipalityId.disable();
    this.municipalityId.setValue(null);

    this.cityId.disable();
    this.cityId.setValue(null);
    this.isSetForNativeAddress = false;
  }

  public setForBulgarianAddress() {
    this.districtId.enable();
    this.municipalityId.enable();
    this.cityId.enable();
    this.isSetForNativeAddress = true;
  }
}
