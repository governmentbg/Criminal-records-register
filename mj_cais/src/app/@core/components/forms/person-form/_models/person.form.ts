import { FormControl, FormGroup, Validators } from "@angular/forms";
import { AddressForm } from "../../address-form/_model/address.form";
import { MultipleChooseForm } from "../../inputs/multiple-choose/models/multiple-choose.form";
import { PersonContextEnum } from "./person-context-enum";

export class PersonForm {
  public group: FormGroup;
  public id: FormControl;
  public suid: FormControl;
  public version: FormControl;
  // applying validation rules,
  // showing or hiding form controls
  // depends on context type
  public contextType: FormControl;
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
  public egn: FormControl;
  public egnDisplay: FormControl;
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
  public personAlias: FormControl;

  constructor(context: string, isDisabled: boolean = true) {
    this.id = new FormControl(null);
    this.suid = new FormControl(null);
    this.suid.disable();
    this.version = new FormControl(null);
    this.contextType = new FormControl(context);
    this.firstname = new FormControl(null);
    this.surname = new FormControl(null);
    this.familyname = new FormControl(null);
    this.fullname = new FormControl(null);
    this.firstnameLat = new FormControl(null);
    this.surnameLat = new FormControl(null);
    this.familynameLat = new FormControl(null);
    this.fullnameLat = new FormControl(null);
    this.personAliasTransactions = new FormControl(null);
    this.personAlias = new FormControl(null);
    this.sex = new FormControl(null);
    this.birthDate = new FormControl(null);
    this.egn = new FormControl(null);
    this.egnDisplay = new FormControl(null);
    this.lnch = new FormControl(null);
    this.ln = new FormControl(null);
    this.nationalities = new MultipleChooseForm();
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
    if (isDisabled) {
      this.birthPlace = new AddressForm(false, true);
      this.birthPlace.group.disable();
    } else {
      this.birthPlace = new AddressForm(false, false);
      if (context != PersonContextEnum.Fbbc) {
        this.firstname.setValidators([
          Validators.required,
          Validators.maxLength(200),
        ]);

        this.surname.setValidators([
          Validators.required,
          Validators.maxLength(200),
        ]);

        this.familyname.setValidators([
          Validators.required,
          Validators.maxLength(200),
        ]);
        this.fullname.setValidators(Validators.maxLength(200));
        this.fullnameLat.setValidators(Validators.maxLength(200));
        this.motherFullname.setValidators(Validators.maxLength(200));
        this.fatherFullname.setValidators(Validators.maxLength(200));
      }

      if (
        context == PersonContextEnum.Application ||
        context == PersonContextEnum.Bulletin
      ) {
        this.nationalities = new MultipleChooseForm(true);
      }

      if (
        context == PersonContextEnum.Person ||
        context == PersonContextEnum.Bulletin
      ) {
        this.afisNumber.setValidators(Validators.maxLength(100));
        this.idDocNumber.setValidators(Validators.maxLength(100));
        this.idDocCategoryId.setValidators(Validators.maxLength(50));
      }

      if (context == PersonContextEnum.Bulletin) {
        this.firstnameLat.setValidators([
          Validators.maxLength(200),
        ]);
        this.surnameLat.setValidators([
          Validators.maxLength(200),
        ]);
        this.familynameLat.setValidators([
          Validators.maxLength(200),
        ]);
      }

      this.birthDate.setValidators(Validators.required);
      this.birthPlace = new AddressForm(false);
      this.motherFirstname.setValidators(Validators.maxLength(200));
      this.motherSurname.setValidators(Validators.maxLength(200));
      this.motherFamilyname.setValidators(Validators.maxLength(200));
      this.fatherFirstname.setValidators(Validators.maxLength(200));
      this.fatherSurname.setValidators(Validators.maxLength(200));
      this.fatherFamilyname.setValidators(Validators.maxLength(200));
    }

    this.group = new FormGroup({
      id: this.id,
      suid: this.suid,
      version: this.version,
      contextType: this.contextType,
      firstname: this.firstname,
      surname: this.surname,
      familyname: this.familyname,
      fullname: this.fullname,
      firstnameLat: this.firstnameLat,
      surnameLat: this.surnameLat,
      familynameLat: this.familynameLat,
      fullnameLat: this.fullnameLat,
      personAliasTransactions: this.personAliasTransactions,
      personAlias: this.personAlias,
      sex: this.sex,
      birthDate: this.birthDate,
      birthPlace: this.birthPlace.group,
      egn: this.egn,
      egnDisplay: this.egnDisplay,
      lnch: this.lnch,
      ln: this.ln,
      nationalities: this.nationalities.group,
      afisNumber: this.afisNumber,
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
