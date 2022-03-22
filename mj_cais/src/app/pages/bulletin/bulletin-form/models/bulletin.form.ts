import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";
import { AddressForm } from "../../../../@core/components/forms/address-form/model/address.form";
import { MultipleChooseForm } from "../../../../@core/components/forms/inputs/multiple-choose/models/multiple-choose.form";

export class BulletinForm {
  public group: FormGroup;

  public id: FormControl;
  public version: FormControl;
  public csAuthorityName: FormControl;
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
  public decisionsTransactions: FormControl;
  public documentsTransactions: FormControl;
  public ecrisConvictionId: FormControl;
  public personAliasTransactions: FormControl;
  public nationalities: MultipleChooseForm;
  public address: AddressForm;

  constructor() {
    var guid = Guid.create().toString();
    this.id = new FormControl(guid);
    this.version = new FormControl(null);
    this.csAuthorityName = new FormControl(null);
    this.registrationNumber = new FormControl(null);
    this.sequentialIndex = new FormControl(null, [Validators.required]);
    this.decisionNumber = new FormControl(null, [Validators.required]);
    this.decisionDate = new FormControl(null, [Validators.required]);
    this.decisionFinalDate = new FormControl(null, [Validators.required]);
    this.decidingAuthId = new FormControl(null, [Validators.required]);
    this.decisionTypeId = new FormControl(null);
    this.caseTypeId = new FormControl(null);
    this.caseNumber = new FormControl(null, [Validators.required]);
    this.caseYear = new FormControl(null, [Validators.required]);
    this.convRemarks = new FormControl(null);
    this.alphabeticalIndex = new FormControl(null, [Validators.required]);
    this.decisionEcli = new FormControl(null);
    this.bulletinCreateDate = new FormControl(null, [Validators.required]);
    this.bulletinReceivedDate = new FormControl(null);
    this.bulletinAuthorityId = new FormControl(null);
    this.createdByNames = new FormControl(null, [Validators.required]);
    this.approvedByNames = new FormControl(null, [Validators.required]);
    this.approvedByPosition = new FormControl(null, [Validators.required]);
    //this.statusId = new FormControl(null, [Validators.required]); // todo:
    this.statusId = new FormControl(null);
    this.firstname = new FormControl(null, [Validators.required]);
    this.surname = new FormControl(null, [Validators.required]);
    this.familyname = new FormControl(null, [Validators.required]);
    this.fullname = new FormControl(null);
    this.firstnameLat = new FormControl(null, [Validators.required]);
    this.surnameLat = new FormControl(null, [Validators.required]);
    this.familynameLat = new FormControl(null, [Validators.required]);
    this.sex = new FormControl(null, [Validators.required]);
    this.egn = new FormControl(null, [Validators.required]);
    this.ln = new FormControl(null, [Validators.required]);
    this.lnch = new FormControl(null, [Validators.required]);
    this.birthDate = new FormControl(null, [Validators.required]);
    this.birthDatePrecision = new FormControl(null);
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
    this.decisionsTransactions = new FormControl(null);
    this.documentsTransactions = new FormControl(null);
    this.ecrisConvictionId = new FormControl(null);
    this.personAliasTransactions = new FormControl(null);
    this.nationalities = new MultipleChooseForm(true);
    this.address = new AddressForm();

    this.group = new FormGroup({
      id: this.id,
      version: this.version,
      csAuthorityName: this.csAuthorityName,
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
      sanctionsTransactions: this.sanctionsTransactions,
      decisionsTransactions: this.decisionsTransactions,
      documentsTransactions: this.documentsTransactions,
      ecrisConvictionId: this.ecrisConvictionId,
      personAliasTransactions: this.personAliasTransactions,
      nationalities: this.nationalities.group,
      address: this.address.group
    });
  }
}
