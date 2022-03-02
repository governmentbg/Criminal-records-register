import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";

export class BulletinForm {
  public group: FormGroup;

  public id: FormControl;
  public version: FormControl;
  public csAuthorityId: FormControl;
  public registrationNumber: FormControl;
  public sequentialIndex: FormControl;
  public decisionNumber: FormControl;
  public decisionDate: FormControl;
  public decisionFinalDate: FormControl;
  public decidingAuthId: FormControl;
  public decisionTypeId: FormControl;
  public caseTypeId: FormControl;
  public caseNumber: FormControl;
  public caseYear: FormControl;
  public convRemarks: FormControl;
  public alphabeticalIndex: FormControl;
  public decisionEcli: FormControl;
  public bulletinCreateDate: FormControl;
  public bulletinReceivedDate: FormControl;
  public bulletinAuthorityId: FormControl;
  public createdByNames: FormControl;
  public approvedByNames: FormControl;
  public approvedByPosition: FormControl;
  public statusId: FormControl;
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
  public birthDate: FormControl;
  public birthDatePrecision: FormControl;
  public birthCityId: FormControl;
  public birthCountryId: FormControl;
  public birthPlaceOther: FormControl;
  public fullnameLat: FormControl;
  public idDocNumber: FormControl;
  public idDocCategoryId: FormControl;
  public idDocTypeDescr: FormControl;
  public idDocIssuingAuthority: FormControl;
  public idDocIssuingDate: FormControl;
  public idDocIssuingDatePrec: FormControl;
  public idDocValidDate: FormControl;
  public idDocValidDatePrec: FormControl;
  public motherFirstname: FormControl;
  public motherFamilyname: FormControl;
  public motherFullname: FormControl;
  public fatherFirstname: FormControl;
  public fatherSurname: FormControl;
  public fatherFamilyname: FormControl;
  public fatherFullname: FormControl;
  public motherSurname: FormControl;
  public afisNumber: FormControl;
  public convIsTransmittable: FormControl;
  public convRetPeriodEndDate: FormControl;
  public createdByPosition: FormControl;
  public bulletinType: FormControl;
  public offancesTransactions: FormControl;
  public sanctionsTransactions: FormControl;

  constructor() {
    var guid = Guid.create().toString();
    this.id = new FormControl(guid);
    this.version = new FormControl(null);
    this.csAuthorityId = new FormControl(null);
    this.registrationNumber = new FormControl(null);
    this.sequentialIndex = new FormControl(null);
    this.decisionNumber = new FormControl(null);
    this.decisionDate = new FormControl(null);
    this.decisionFinalDate = new FormControl(null);
    this.decidingAuthId = new FormControl(null);
    this.decisionTypeId = new FormControl(null);
    this.caseTypeId = new FormControl(null);
    this.caseNumber = new FormControl(null);
    this.caseYear = new FormControl(null);
    this.convRemarks = new FormControl(null);
    this.alphabeticalIndex = new FormControl(null);
    this.decisionEcli = new FormControl(null);
    this.bulletinCreateDate = new FormControl(null);
    this.bulletinReceivedDate = new FormControl(null);
    this.bulletinAuthorityId = new FormControl(null);
    this.createdByNames = new FormControl(null);
    this.approvedByNames = new FormControl(null);
    this.approvedByPosition = new FormControl(null);
    this.statusId = new FormControl(null);
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
    this.birthDate = new FormControl(null);
    this.birthDatePrecision = new FormControl(null);
    this.birthCityId = new FormControl(null);
    this.birthCountryId = new FormControl(null);
    this.birthPlaceOther = new FormControl(null);
    this.fullnameLat = new FormControl(null);
    this.idDocNumber = new FormControl(null);
    this.idDocCategoryId = new FormControl(null);
    this.idDocTypeDescr = new FormControl(null);
    this.idDocIssuingAuthority = new FormControl(null);
    this.idDocIssuingDate = new FormControl(null);
    this.idDocIssuingDatePrec = new FormControl(null);
    this.idDocValidDate = new FormControl(null);
    this.idDocValidDatePrec = new FormControl(null);
    this.motherFirstname = new FormControl(null);
    this.motherFamilyname = new FormControl(null);
    this.motherFullname = new FormControl(null);
    this.fatherFirstname = new FormControl(null);
    this.fatherSurname = new FormControl(null);
    this.fatherFamilyname = new FormControl(null);
    this.fatherFullname = new FormControl(null);
    this.motherSurname = new FormControl(null);
    this.afisNumber = new FormControl(null);
    this.convIsTransmittable = new FormControl(null);
    this.convRetPeriodEndDate = new FormControl(null);
    this.createdByPosition = new FormControl(null);
    this.bulletinType = new FormControl(null);
    this.offancesTransactions = new FormControl(null);
    this.sanctionsTransactions = new FormControl(null);

    this.group = new FormGroup({
      id: this.id,
      version: this.version,
      csAuthorityId: this.csAuthorityId,
      registrationNumber: this.registrationNumber,
      sequentialIndex: this.sequentialIndex,
      decisionNumber: this.decisionNumber,
      decisionDate: this.decisionDate,
      decisionFinalDate: this.decisionFinalDate,
      decidingAuthId: this.decidingAuthId,
      decisionTypeId: this.decisionTypeId,
      caseTypeId: this.caseTypeId,
      caseNumber: this.caseNumber,
      caseYear: this.caseYear,
      convRemarks: this.convRemarks,
      alphabeticalIndex: this.alphabeticalIndex,
      decisionEcli: this.decisionEcli,
      bulletinCreateDate: this.bulletinCreateDate,
      bulletinReceivedDate: this.bulletinReceivedDate,
      bulletinAuthorityId: this.bulletinAuthorityId,
      createdByNames: this.createdByNames,
      approvedByNames: this.approvedByNames,
      approvedByPosition: this.approvedByPosition,
      statusId: this.statusId,
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
      birthDate: this.birthDate,
      birthDatePrecision: this.birthDatePrecision,
      birthCityId: this.birthCityId,
      birthCountryId: this.birthCountryId,
      birthPlaceOther: this.birthPlaceOther,
      fullnameLat: this.fullnameLat,
      idDocNumber: this.idDocNumber,
      idDocCategoryId: this.idDocCategoryId,
      idDocTypeDescr: this.idDocTypeDescr,
      idDocIssuingAuthority: this.idDocIssuingAuthority,
      idDocIssuingDate: this.idDocIssuingDate,
      idDocIssuingDatePrec: this.idDocIssuingDatePrec,
      idDocValidDate: this.idDocValidDate,
      idDocValidDatePrec: this.idDocValidDatePrec,
      motherFirstname: this.motherFirstname,
      motherFamilyname: this.motherFamilyname,
      motherFullname: this.motherFullname,
      fatherFirstname: this.fatherFirstname,
      fatherSurname: this.fatherSurname,
      fatherFamilyname: this.fatherFamilyname,
      fatherFullname: this.fatherFullname,
      motherSurname: this.motherSurname,
      afisNumber: this.afisNumber,
      convIsTransmittable: this.convIsTransmittable,
      convRetPeriodEndDate: this.convRetPeriodEndDate,
      createdByPosition: this.createdByPosition,
      bulletinType: this.bulletinType,
      offancesTransactions: this.offancesTransactions,
      sanctionsTransactions: this.sanctionsTransactions
    });
  }
}
