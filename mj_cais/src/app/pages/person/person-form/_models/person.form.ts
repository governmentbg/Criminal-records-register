import { FormControl, FormGroup } from "@angular/forms";
import { AddressForm } from "../../../../@core/components/forms/address-form/model/address.form";
import { MultipleChooseForm } from "../../../../@core/components/forms/inputs/multiple-choose/models/multiple-choose.form";

export class PersonForm {
  public group: FormGroup;
  public id: FormControl;
  public firstname: FormControl;
  public surname: FormControl;
  public familyname: FormControl;
  public fullname: FormControl;
  public firstnameLat: FormControl;
  public surnameLat: FormControl;
  public familynameLat: FormControl;
  public fullnameLat: FormControl;
  public personAliasTransactions: FormControl;
  public sex: FormControl;
  public birthDate: FormControl;
  public birthPlace: AddressForm;
  public birthPlaceAuthId: FormControl;
  public egn: FormControl;
  public lnch: FormControl;
  public ln: FormControl;
  public nationalities: MultipleChooseForm;
  public afisNumber: FormControl;
  public idDocNumber: FormControl;
  public idDocCategoryId: FormControl;
  public idDocTypeDescr: FormControl;
  public idDocIssuingAuthority: FormControl;
  public idDocIssuingDate: FormControl;
  public idDocValidDate: FormControl;
  public motherFirstname: FormControl;
  public motherSurname: FormControl;
  public motherFamilyname: FormControl;
  public motherFullname: FormControl;
  public fatherFirstname: FormControl;
  public fatherSurname: FormControl;
  public fatherFamilyname: FormControl;
  public fatherFullname: FormControl;

  constructor() {
    this.id = new FormControl(null);
    this.firstname = new FormControl(null);
    this.surname = new FormControl(null);
    this.familyname = new FormControl(null);
    this.fullname = new FormControl(null);
    this.firstnameLat = new FormControl(null);
    this.surnameLat = new FormControl(null);
    this.familynameLat = new FormControl(null);
    this.fullnameLat = new FormControl(null);
    this.personAliasTransactions = new FormControl(null);
    this.sex = new FormControl(null);
    this.birthDate = new FormControl(null);
    this.birthPlace = new AddressForm(false);
    this.birthPlaceAuthId = new FormControl(null);
    this.egn = new FormControl(null);
    this.lnch = new FormControl(null);
    this.ln = new FormControl(null);
    this.nationalities = new MultipleChooseForm(false);
    this.afisNumber = new FormControl(null);
    this.idDocNumber = new FormControl(null);
    this.idDocCategoryId = new FormControl(null);
    this.idDocTypeDescr = new FormControl(null);
    this.idDocIssuingAuthority = new FormControl(null);
    this.idDocIssuingDate = new FormControl(null);
    this.idDocValidDate = new FormControl(null);
    this.motherFirstname = new FormControl(null);
    this.motherSurname = new FormControl(null);
    this.motherFamilyname = new FormControl(null);
    this.motherFullname = new FormControl(null);
    this.fatherFirstname = new FormControl(null);
    this.fatherSurname = new FormControl(null);
    this.fatherFamilyname = new FormControl(null);
    this.fatherFullname = new FormControl(null);

    this.group = new FormGroup({
      id: this.id,
      firstname: this.firstname,
      surname: this.surname,
      familyname: this.familyname,
      fullname: this.fullname,
      firstnameLat: this.firstnameLat,
      surnameLat: this.surnameLat,
      familynameLat: this.familynameLat,
      personAliasTransactions: this.personAliasTransactions,
      sex: this.sex,
      birthDate: this.birthDate,
      birthPlace: this.birthPlace.group,
      birthPlaceAuthId: this.birthPlaceAuthId,
      egn: this.egn,
      lnch: this.lnch,
      ln: this.ln,
      nationalities: this.nationalities.group,
      afisNumber: this.afisNumber,
      fullnameLat: this.fullnameLat,
      idDocNumber: this.idDocNumber,
      idDocCategoryId: this.idDocCategoryId,
      idDocTypeDescr: this.idDocTypeDescr,
      idDocIssuingAuthority: this.idDocIssuingAuthority,
      idDocIssuingDate: this.idDocIssuingDate,
      idDocValidDate: this.idDocValidDate,
      motherFirstname: this.motherFirstname,
      motherSurname: this.motherSurname,
      motherFamilyname: this.motherFamilyname,
      motherFullname: this.motherFullname,
      fatherFirstname: this.fatherFirstname,
      fatherSurname: this.fatherSurname,
      fatherFamilyname: this.fatherFamilyname,
      fatherFullname: this.fatherFullname,
    });
  }
}
