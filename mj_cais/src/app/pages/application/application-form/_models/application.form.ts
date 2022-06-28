import { FormControl, FormGroup, Validators } from "@angular/forms";
import { AddressForm } from "../../../../@core/components/forms/address-form/_model/address.form";
import { PersonContextEnum } from "../../../../@core/components/forms/person-form/_models/person-context-enum";
import { PersonForm } from "../../../../@core/components/forms/person-form/_models/person.form";
import { BaseForm } from "../../../../@core/models/common/base.form";

export class ApplicationForm extends BaseForm {
  public group: FormGroup;

  public registrationNumber: FormControl;
  public purpose: FormControl;
  public currentApplicationStatus: FormControl;
  public firstname: FormControl;
  public surname: FormControl;
  public familyname: FormControl;
  public fullname: FormControl;
  public firstnameLat: FormControl;
  public surnameLat: FormControl;
  public familynameLat: FormControl;
  public sex: FormControl;
  public egn: FormControl;
  public ln: FormControl;
  public lnch: FormControl;
  public personId: FormControl;
  public applicantName: FormControl;
  public motherFirstname: FormControl;
  public motherSurname: FormControl;
  public motherFamilyname: FormControl;
  public fatherFirstname: FormControl;
  public fatherSurname: FormControl;
  public fatherFamilyname: FormControl;
  public motherFullname: FormControl;
  public fatherFullname: FormControl;
  public purposeCountry: FormControl;
  public purposePosition: FormControl;
  public srvcResRcptMethId: FormControl;
  public addrName: FormControl;
  public addrStr: FormControl;
  public addrDistrict: FormControl;
  public addrTown: FormControl;
  public addrState: FormControl;
  public addrPhone: FormControl;
  public addrEmail: FormControl;
  public description: FormControl;
  public email: FormControl;
  public applicationTypeId: FormControl;
  public csAuthorityId: FormControl;
  public isLocal: FormControl;
  public purposeId: FormControl;
  public paymentMethodId: FormControl;
  public fromCosul: FormControl;
  public docContentId: FormControl;
  public statusCode: FormControl;
  public birthDate: FormControl;
  public birthDatePrecision: FormControl;
  public birthCountryId: FormControl;
  public birthCityId: FormControl;
  public birthPlaceOther: FormControl;
  public userCitizenId: FormControl;
  public userId: FormControl;
  public userExtId: FormControl;
  public address: FormControl;
  public birthAddress: AddressForm;
  public person: PersonForm;

  constructor() {
    super();
    this.registrationNumber = new FormControl(null);
    this.purpose = new FormControl(null);
    this.currentApplicationStatus = new FormControl(null);
    this.firstname = new FormControl(null);
    this.surname = new FormControl(null);
    this.familyname = new FormControl(null);
    this.fullname = new FormControl(null);
    this.firstnameLat = new FormControl(null);
    this.surnameLat = new FormControl(null);
    this.familynameLat = new FormControl(null);
    this.sex = new FormControl(null);
    this.egn = new FormControl(null);
    this.ln = new FormControl(null);
    this.lnch = new FormControl(null);
    this.personId = new FormControl(null);
    this.applicantName = new FormControl(null);
    this.address = new FormControl(null);
    this.motherFirstname = new FormControl(null);
    this.motherSurname = new FormControl(null);
    this.motherFamilyname = new FormControl(null);
    this.fatherFirstname = new FormControl(null);
    this.fatherSurname = new FormControl(null);
    this.fatherFamilyname = new FormControl(null);
    this.motherFullname = new FormControl(null);
    this.fatherFullname = new FormControl(null);
    this.purposeCountry = new FormControl(null);
    this.purposePosition = new FormControl(null);
    this.srvcResRcptMethId = new FormControl(null);
    this.addrName = new FormControl(null);
    this.addrStr = new FormControl(null);
    this.addrDistrict = new FormControl(null);
    this.addrTown = new FormControl(null);
    this.addrState = new FormControl(null);
    this.addrPhone = new FormControl(null);
    this.addrEmail = new FormControl(null);
    this.description = new FormControl(null);
    this.email = new FormControl(null);
    this.applicationTypeId = new FormControl(null);
    this.csAuthorityId = new FormControl(null);
    this.isLocal = new FormControl(null);
    this.purposeId = new FormControl(null);
    this.paymentMethodId = new FormControl(null);
    this.fromCosul = new FormControl(null);
    this.docContentId = new FormControl(null);
    this.statusCode = new FormControl(null);
    this.birthDate = new FormControl(null);
    this.birthDatePrecision = new FormControl(null);
    this.birthCountryId = new FormControl(null);
    this.birthCityId = new FormControl(null);
    this.birthPlaceOther = new FormControl(null);
    this.userCitizenId = new FormControl(null);
    this.userId = new FormControl(null);
    this.userExtId = new FormControl(null);
    this.birthAddress = new AddressForm();
    this.person = new PersonForm(PersonContextEnum.Application, false);
    
    //Validators
    this.registrationNumber.disable();
    this.currentApplicationStatus.disable();
    
    this.group = new FormGroup({
      id: this.id,
      version: this.version,
      registrationNumber: this.registrationNumber,
      currentApplicationStatus: this.currentApplicationStatus,
      purpose: this.purpose,
      firstname: this.firstname,
      surname: this.surname,
      familyname: this.familyname,
      fullname: this.fullname,
      firstnameLat: this.firstnameLat,
      surnameLat: this.surnameLat,
      familynameLat: this.familynameLat,
      sex: this.sex,
      egn: this.egn,
      ln: this.ln,
      lnch: this.lnch,
      personId: this.personId,
      applicantName: this.applicantName,
      address: this.address,
      motherFirstname: this.motherFirstname,
      motherSurname: this.motherSurname,
      motherFamilyname: this.motherFamilyname,
      fatherFirstname: this.fatherFirstname,
      fatherSurname: this.fatherSurname,
      fatherFamilyname: this.fatherFamilyname,
      motherFullname: this.motherFullname,
      fatherFullname: this.fatherFullname,
      purposeCountry: this.purposeCountry,
      purposePosition: this.purposePosition,
      srvcResRcptMethId: this.srvcResRcptMethId,
      addrName: this.addrName,
      addrStr: this.addrStr,
      addrDistrict: this.addrDistrict,
      addrTown: this.addrTown,
      addrState: this.addrState,
      addrPhone: this.addrPhone,
      addrEmail: this.addrEmail,
      description: this.description,
      email: this.email,
      applicationTypeId: this.applicationTypeId,
      csAuthorityId: this.csAuthorityId,
      isLocal: this.isLocal,
      purposeId: this.purposeId,
      paymentMethodId: this.paymentMethodId,
      fromCosul: this.fromCosul,
      docContentId: this.docContentId,
      statusCode: this.statusCode,
      birthDate: this.birthDate,
      birthDatePrecision: this.birthDatePrecision,
      birthCountryId: this.birthCountryId,
      birthCityId: this.birthCityId,
      birthPlaceOther: this.birthPlaceOther,
      userCitizenId: this.userCitizenId,
      userId: this.userId,
      userExtId: this.userExtId,
      birthAddress: this.birthAddress.group,
      person: this.person.group
    });
  }
}
