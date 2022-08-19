import { FormControl, FormGroup } from "@angular/forms";
import { AddressForm } from "../../../../@core/components/forms/address-form/_model/address.form";
import { PersonContextEnum } from "../../../../@core/components/forms/person-form/_models/person-context-enum";
import { PersonForm } from "../../../../@core/components/forms/person-form/_models/person.form";
import { BaseForm } from "../../../../@core/models/common/base.form";

export class EApplicationForm extends BaseForm {
  public group: FormGroup;

  public registrationNumber: FormControl;
  public purpose: FormControl;
  public email: FormControl;
  public birthDate: FormControl;
  public birthDatePrecision: FormControl;
  public applicantName: FormControl;
  public address: FormControl;
  public addrName: FormControl;
  public addrStr: FormControl;
  public addrDistrict: FormControl;
  public addrTown: FormControl;
  public addrState: FormControl;
  public addrPhone: FormControl;
  public addrEmail: FormControl;
  public description: FormControl;
  public isLocal: FormControl;
  public fromCosul: FormControl;
  public birthPlaceOther: FormControl;
  public userId: FormControl;
  public wApplicationId: FormControl;
  public purposeId: FormControl;
  public srvcResRcptMethId: FormControl;
  public applicationTypeId: FormControl;
  public csAuthorityId: FormControl;
  public paymentMethodId: FormControl;
  public birthCountryId: FormControl;
  public birthCityId: FormControl;
  public userCitizenId: FormControl;
  public userExtId: FormControl;
  public csAuthorityBirthId: FormControl;
  public statusCode: FormControl;
  // public birthAddress: AddressForm;
  public person: PersonForm;

  constructor() {
    super();
    this.registrationNumber = new FormControl(null);
    this.purpose = new FormControl(null);
    this.email = new FormControl(null);
    this.birthDate = new FormControl(null);
    this.birthDatePrecision = new FormControl(null);
    this.applicantName = new FormControl(null);
    this.address = new FormControl(null);
    this.addrName = new FormControl(null);
    this.addrStr = new FormControl(null);
    this.addrDistrict = new FormControl(null);
    this.addrTown = new FormControl(null);
    this.addrState = new FormControl(null);
    this.addrPhone = new FormControl(null);
    this.addrEmail = new FormControl(null);
    this.description = new FormControl(null);
    this.isLocal = new FormControl(null);
    this.fromCosul = new FormControl(null);
    this.birthPlaceOther = new FormControl(null);
    // // this.birthAddress = new AddressForm();
    this.person = new PersonForm(PersonContextEnum.Application, false);
    this.userId = new FormControl(null);
    this.wApplicationId = new FormControl(null);
    this.purposeId = new FormControl(null);
    this.srvcResRcptMethId = new FormControl(null);
    this.applicationTypeId = new FormControl(null);
    this.csAuthorityId = new FormControl(null);
    this.paymentMethodId = new FormControl(null);
    this.birthCountryId = new FormControl(null);
    this.birthCityId = new FormControl(null);
    this.userCitizenId = new FormControl(null);
    this.userExtId = new FormControl(null);
    this.csAuthorityBirthId = new FormControl(null);
    this.statusCode = new FormControl(null);

    this.group = new FormGroup({
      id: this.id,
      version: this.version,
      registrationNumber: this.registrationNumber,
      purpose: this.purpose,
      email: this.email,
      birthDate: this.birthDate,
      birthDatePrecision: this.birthDatePrecision,
      applicantName: this.applicantName,
      address: this.address,
      addrName: this.addrName,
      addrStr: this.addrStr,
      addrDistrict: this.addrDistrict,
      addrTown: this.addrTown,
      addrState: this.addrState,
      addrPhone: this.addrPhone,
      addrEmail: this.addrEmail,
      description: this.description,
      isLocal: this.isLocal,
      fromCosul: this.fromCosul,
      birthPlaceOther: this.birthPlaceOther,
      userId: this.userId,
      wApplicationId: this.wApplicationId,
      purposeId: this.purposeId,
      srvcResRcptMethId: this.srvcResRcptMethId,
      applicationTypeId: this.applicationTypeId,
      csAuthorityId: this.csAuthorityId,
      paymentMethodId: this.paymentMethodId,
      birthCountryId: this.birthCountryId,
      birthCityId: this.birthCityId,
      userCitizenId: this.userCitizenId,
      userExtId: this.userExtId,
      csAuthorityBirthId: this.csAuthorityBirthId,
      statusCode: this.statusCode,
      // birthAddress: this.birthAddress.group,
      person: this.person.group,
    });
  }
}
