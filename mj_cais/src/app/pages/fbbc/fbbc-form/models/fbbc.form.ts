import { FormControl, FormGroup, Validators } from "@angular/forms";

export class FbbcForm {
  public group: FormGroup;

  public id: FormControl;
  public countryId: FormControl;
  public docTypeId: FormControl;
  public sanctionTypeId: FormControl;
  public receiveDate: FormControl;
  public issueDate: FormControl;
  public countryDescr: FormControl;
  public egn: FormControl;
  public firstname: FormControl;
  public surname: FormControl;
  public familyname: FormControl;
  public birthPlace: FormControl;
  public birthCityId: FormControl;
  public birtyCountryDescr: FormControl;
  public birthCountryId: FormControl;
  public birthDate: FormControl;
  public offenceStartDate: FormControl;
  public offenceEndDate: FormControl;
  public annotation: FormControl;
  public motherFirstname: FormControl;
  public motherSurname: FormControl;
  public motherFamilyname: FormControl;
  public fatherFirstname: FormControl;
  public fatherSurname: FormControl;
  public fatherFamilyname: FormControl;
  public gdkpNumber: FormControl;
  public gdkpDate: FormControl;
  public gdkpCaseNumber: FormControl;
  public gdkpTom: FormControl;
  public gdkpStr: FormControl;
  public njrCountry: FormControl;
  public njrIdentifier: FormControl;
  public njrFirstId: FormControl;
  public ecrisMsgId: FormControl;
  public ecrisConvId: FormControl;
  public ecrisUpdConvTypeId: FormControl;
  public ecrisUpdConvId: FormControl;
  public isAdministrative: FormControl;
  public convDecisionDate: FormControl;
  public convDecFinalDate: FormControl;
  public sequentialIndex: FormControl;
  public destroyedDate: FormControl;
  public personId: FormControl;
  public version: FormControl;

  constructor() {
    this.id = new FormControl(null);
    this.countryId = new FormControl(null);
    this.docTypeId = new FormControl(null);
    this.sanctionTypeId = new FormControl(null);
    this.receiveDate = new FormControl(null);
    this.issueDate = new FormControl(null);
    this.countryDescr = new FormControl(null);
    this.egn = new FormControl(null);
    this.firstname = new FormControl(null);
    this.surname = new FormControl(null);
    this.familyname = new FormControl(null);
    this.birthPlace = new FormControl(null);
    this.birthCityId = new FormControl(null);
    this.birtyCountryDescr = new FormControl(null);
    this.birthCountryId = new FormControl(null);
    this.birthDate = new FormControl(null);
    this.offenceStartDate = new FormControl(null);
    this.offenceEndDate = new FormControl(null);
    this.annotation = new FormControl(null);
    this.motherFirstname = new FormControl(null);
    this.motherSurname = new FormControl(null);
    this.motherFamilyname = new FormControl(null);
    this.fatherFirstname = new FormControl(null);
    this.fatherSurname = new FormControl(null);
    this.fatherFamilyname = new FormControl(null);
    this.gdkpNumber = new FormControl(null);
    this.gdkpDate = new FormControl(null);
    this.gdkpCaseNumber = new FormControl(null);
    this.gdkpTom = new FormControl(null);
    this.gdkpStr = new FormControl(null);
    this.njrCountry = new FormControl(null);
    this.njrIdentifier = new FormControl(null);
    this.njrFirstId = new FormControl(null);
    this.ecrisMsgId = new FormControl(null);
    this.ecrisConvId = new FormControl(null);
    this.ecrisUpdConvTypeId = new FormControl(null);
    this.ecrisUpdConvId = new FormControl(null);
    this.isAdministrative = new FormControl(null);
    this.convDecisionDate = new FormControl(null);
    this.convDecFinalDate = new FormControl(null);
    this.sequentialIndex = new FormControl(null);
    this.destroyedDate = new FormControl(null);
    this.personId = new FormControl(null);
    this.version = new FormControl(null);

    this.group = new FormGroup({
      id: this.id,
      countryId: this.countryId,
      docTypeId: this.docTypeId,
      sanctionTypeId: this.sanctionTypeId,
      receiveDate: this.receiveDate,
      issueDate: this.issueDate,
      countryDescr: this.countryDescr,
      egn: this.egn,
      firstname: this.firstname,
      surname: this.surname,
      familyname: this.familyname,
      birthPlace: this.birthPlace,
      birthCityId: this.birthCityId,
      birtyCountryDescr: this.birtyCountryDescr,
      birthCountryId: this.birthCountryId,
      birthDate: this.birthDate,
      offenceStartDate: this.offenceStartDate,
      offenceEndDate: this.offenceEndDate,
      annotation: this.annotation,
      motherFirstname: this.motherFirstname,
      motherSurname: this.motherSurname,
      motherFamilyname: this.motherFamilyname,
      fatherFirstname: this.fatherFirstname,
      fatherSurname: this.fatherSurname,
      fatherFamilyname: this.fatherFamilyname,
      gdkpNumber: this.gdkpNumber,
      gdkpDate: this.gdkpDate,
      gdkpCaseNumber: this.gdkpCaseNumber,
      gdkpTom: this.gdkpTom,
      gdkpStr: this.gdkpStr,
      njrCountry: this.njrCountry,
      njrIdentifier: this.njrIdentifier,
      njrFirstId: this.njrFirstId,
      ecrisMsgId: this.ecrisMsgId,
      ecrisConvId: this.ecrisConvId,
      ecrisUpdConvTypeId: this.ecrisUpdConvTypeId,
      ecrisUpdConvId: this.ecrisUpdConvId,
      isAdministrative: this.isAdministrative,
      convDecisionDate: this.convDecisionDate,
      convDecFinalDate: this.convDecFinalDate,
      sequentialIndex: this.sequentialIndex,
      destroyedDate: this.destroyedDate,
      personId: this.personId,
      version: this.version,
    });
  }
}
