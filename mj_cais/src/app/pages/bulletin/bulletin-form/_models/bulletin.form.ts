import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";
import { AddressForm } from "../../../../@core/components/forms/address-form/model/address.form";
import { MultipleChooseForm } from "../../../../@core/components/forms/inputs/multiple-choose/models/multiple-choose.form";
import { BulletinStatusTypeEnum } from "../../bulletin-overview/_models/bulletin-status-type.constants";

export class BulletinForm {
  public group: FormGroup;

  public id: FormControl;
  public registrationNumber: FormControl;
  public csAuthorityName: FormControl;
  public sequentialIndex: FormControl;
  public statusIdDisplay: FormControl;
  public alphabeticalIndex: FormControl;
  public ecrisConvictionId: FormControl;
  public bulletinReceivedDate: FormControl;
  public bulletinType: FormControl;
  public bulletinAuthorityId: FormControl;
  public bulletinCreateDate: FormControl;
  public createdByNames: FormControl;
  public createdByPosition: FormControl;
  public approvedByNames: FormControl;
  public approvedByPosition: FormControl;

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
  public birthDatePrecision: FormControl;
  public egn: FormControl;
  public lnch: FormControl;
  public ln: FormControl;

  public nationalities: MultipleChooseForm;

  public afisNumber: FormControl;

  public noSanction: FormControl;
  public prevSuspSent: FormControl;

  public idDocNumber: FormControl;
  public idDocCategoryId: FormControl;
  public idDocTypeDescr: FormControl;
  public idDocIssuingAuthority: FormControl;
  public idDocIssuingDate: FormControl;
  public idDocIssuingDatePrec: FormControl;
  public idDocValidDate: FormControl;
  public idDocValidDatePrec: FormControl;

  public motherFirstname: FormControl;
  public motherSurname: FormControl;
  public motherFamilyname: FormControl;
  public motherFullname: FormControl;

  public fatherFirstname: FormControl;
  public fatherSurname: FormControl;
  public fatherFamilyname: FormControl;
  public fatherFullname: FormControl;

  public decisionTypeId: FormControl;
  public decisionNumber: FormControl;
  public decisionDate: FormControl;
  public decisionFinalDate: FormControl;
  public decidingAuthId: FormControl;
  public decisionEcli: FormControl;

  public caseTypeId: FormControl;
  public caseNumber: FormControl;
  public caseYear: FormControl;
  public convRemarks: FormControl;

  public statusId: FormControl;

  public offancesTransactions: FormControl;
  public sanctionsTransactions: FormControl;
  public decisionsTransactions: FormControl;
  public documentsTransactions: FormControl;
  public address: AddressForm;

  constructor(bulletinStstus: string, isEdit: boolean, locked: boolean) {
    this.initFormControls();
    // няма рестрикции при добавяне на бюлетин
    // редакция на бюлетин от служирел БС преди актуализация
    // или ако бюлетина е бил отключен посредством администратор
    let unlockedRecord =
      locked == false ||
      !isEdit ||
      (isEdit && bulletinStstus == BulletinStatusTypeEnum.NewOffice);
    // няма рестрикции по формата
    if (unlockedRecord) {
      this.initUnlocked();
      this.initGroup();
      return;
    }

    // ако е редакция на NewEISS се позволява редакция на определени полета
    if (bulletinStstus == BulletinStatusTypeEnum.NewEISS) {
      this.initForEditNewEISS();
      this.initGroup();
      return;
    }

    // за всички останали статуси на този етап всичко е заключено без
    // Допълнителни сведения
    this.initNonEditableObj();
    this.initGroup();
  }

  private initNonEditableObj(): void {
    this.registrationNumber.disable();
    this.csAuthorityName.disable();
    this.sequentialIndex.disable();
    this.statusIdDisplay.disable();
    this.alphabeticalIndex.disable();
    this.ecrisConvictionId.disable();
    this.bulletinReceivedDate.disable();
    this.bulletinType.disable();
    this.bulletinAuthorityId.disable();
    this.bulletinCreateDate.disable();
    this.createdByNames.disable();
    this.createdByPosition.disable();
    this.approvedByNames.disable();
    this.approvedByPosition.disable();
    this.firstname.disable();
    this.surname.disable();
    this.familyname.disable();
    this.fullname.disable();
    this.firstnameLat.disable();
    this.surnameLat.disable();
    this.familynameLat.disable();
    this.fullnameLat.disable();
    this.sex.disable();
    this.birthDate.disable();
    this.birthDatePrecision.disable();
    this.egn.disable();
    this.lnch.disable();
    this.ln.disable();
    this.nationalities = new MultipleChooseForm(false, true);
    this.afisNumber.disable();
    this.idDocNumber.disable();
    this.idDocCategoryId.disable();
    this.idDocTypeDescr.disable();
    this.idDocIssuingAuthority.disable();
    this.idDocIssuingDate.disable();
    this.idDocIssuingDatePrec.disable();
    this.idDocValidDate.disable();
    this.idDocValidDatePrec.disable();
    this.motherFirstname.disable();
    this.motherSurname.disable();
    this.motherFamilyname.disable();
    this.motherFullname.disable();
    this.fatherFirstname.disable();
    this.fatherSurname.disable();
    this.fatherFamilyname.disable();
    this.fatherFullname.disable();
    this.decisionTypeId.disable();
    this.decisionNumber.disable();
    this.decisionDate.disable();
    this.decisionFinalDate.disable();
    this.decidingAuthId.disable();
    this.decisionEcli.disable();
    this.caseTypeId.disable();
    this.caseNumber.disable();
    this.caseYear.disable();
    this.convRemarks.disable();
    this.noSanction.disable();
    this.prevSuspSent.disable();
    this.address = new AddressForm(false, true);
  }

  private initForEditNewEISS(): void {
    this.registrationNumber.setValidators(Validators.maxLength(100));
    this.csAuthorityName.disable();
    this.sequentialIndex.setValidators(Validators.required);
    this.statusIdDisplay.disable();
    this.alphabeticalIndex.setValidators([
      Validators.required,
      Validators.maxLength(100),
    ]);
    this.ecrisConvictionId.setValidators(Validators.maxLength(50));
    this.bulletinType.setValidators([
      Validators.required,
      Validators.maxLength(50),
    ]);
    this.bulletinAuthorityId.disable();
    this.bulletinCreateDate.disable();
    this.createdByNames.disable();
    this.createdByPosition.disable();
    this.approvedByNames.disable();
    this.approvedByPosition.disable();
    this.firstname.disable();
    this.surname.disable();
    this.familyname.disable();
    this.fullname.disable();
    this.firstnameLat.disable();
    this.surnameLat.disable();
    this.familynameLat.disable();
    this.fullnameLat.disable();
    this.sex.disable();
    this.birthDate.disable();
    this.birthDatePrecision.disable();
    this.egn.disable();
    this.lnch.disable();
    this.ln.disable();
    this.nationalities = new MultipleChooseForm(false, true);
    this.afisNumber.disable();
    this.idDocNumber.disable();
    this.idDocCategoryId.disable();
    this.idDocTypeDescr.disable();
    this.idDocIssuingAuthority.disable();
    this.idDocIssuingDate.disable();
    this.idDocIssuingDatePrec.disable();
    this.idDocValidDate.disable();
    this.idDocValidDatePrec.disable();
    this.motherFirstname.disable();
    this.motherSurname.disable();
    this.motherFamilyname.disable();
    this.motherFullname.disable();
    this.fatherFirstname.disable();
    this.fatherSurname.disable();
    this.fatherFamilyname.disable();
    this.fatherFullname.disable();
    this.decisionTypeId.disable();
    this.decisionNumber.disable();
    this.decisionDate.disable();
    this.decisionFinalDate.disable();
    this.decidingAuthId.disable();
    this.decisionEcli.disable();
    this.caseTypeId.disable();
    this.caseNumber.disable();
    this.caseYear.disable();
    this.convRemarks.disable();
    this.noSanction.disable();
    this.prevSuspSent.disable();
    this.address = new AddressForm(false, true);
  }

  private initUnlocked(): void {
    var guid = Guid.create().toString();
    this.id = new FormControl(guid);
    this.registrationNumber.setValidators(Validators.maxLength(100));
    this.csAuthorityName.disable();
    this.sequentialIndex.setValidators(Validators.required);
    this.statusIdDisplay.disable();
    this.alphabeticalIndex.setValidators([
      Validators.required,
      Validators.maxLength(100),
    ]);
    this.ecrisConvictionId.setValidators(Validators.maxLength(50));
    this.bulletinType.setValidators([
      Validators.required,
      Validators.maxLength(50),
    ]);
    this.bulletinAuthorityId.setValidators(Validators.maxLength(50));
    this.bulletinCreateDate.setValidators(Validators.required);
    this.createdByNames.setValidators(Validators.required);
    this.createdByPosition.setValidators(Validators.maxLength(200));
    this.approvedByNames.setValidators(Validators.required);
    this.approvedByPosition.setValidators(Validators.required);
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
    this.firstnameLat.setValidators([
      Validators.required,
      Validators.maxLength(200),
    ]);
    this.surnameLat.setValidators([
      Validators.required,
      Validators.maxLength(200),
    ]);
    this.familynameLat.setValidators([
      Validators.required,
      Validators.maxLength(200),
    ]);
    this.fullnameLat.setValidators(Validators.maxLength(200));
    this.sex.setValidators(Validators.required);
    this.birthDate.setValidators(Validators.required);
    this.birthDatePrecision.setValidators(Validators.maxLength(200));
    this.egn.setValidators([Validators.required, Validators.maxLength(100)]);
    this.lnch.setValidators([Validators.required, Validators.maxLength(100)]);
    this.ln.setValidators([Validators.required, Validators.maxLength(100)]);
    this.nationalities = new MultipleChooseForm(true);
    this.afisNumber.setValidators(Validators.maxLength(100));
    this.idDocNumber.setValidators(Validators.maxLength(100));
    this.idDocCategoryId.setValidators(Validators.maxLength(50));
    this.idDocIssuingDatePrec.setValidators(Validators.maxLength(200));
    this.idDocValidDatePrec.setValidators(Validators.maxLength(200));
    this.motherFirstname.setValidators(Validators.maxLength(200));
    this.motherSurname.setValidators(Validators.maxLength(200));
    this.motherFamilyname.setValidators(Validators.maxLength(200));
    this.motherFullname.setValidators(Validators.maxLength(200));
    this.fatherFirstname.setValidators(Validators.maxLength(200));
    this.fatherSurname.setValidators(Validators.maxLength(200));
    this.fatherFamilyname.setValidators(Validators.maxLength(200));
    this.fatherFullname.setValidators(Validators.maxLength(200));
    this.decisionTypeId.setValidators(Validators.maxLength(200));
    this.decisionNumber.setValidators([
      Validators.required,
      Validators.maxLength(100),
    ]);
    this.decisionDate.setValidators(Validators.required);
    this.decisionFinalDate.setValidators(Validators.required);
    this.decidingAuthId.setValidators([
      Validators.required,
      Validators.maxLength(50),
    ]);
    this.decisionEcli.setValidators(Validators.maxLength(100));

    this.caseTypeId.setValidators(Validators.maxLength(50));
    this.caseNumber.setValidators([
      Validators.required,
      Validators.maxLength(100),
    ]);
    this.caseYear.setValidators(Validators.required);
    this.statusId.setValidators(Validators.maxLength(50));
    this.address = new AddressForm(true);
  }

  private initGroup(): void {
    this.group = new FormGroup({
      id: this.id,
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
      statusIdDisplay: this.statusIdDisplay,
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
      createdByPosition: this.createdByPosition,
      bulletinType: this.bulletinType,
      offancesTransactions: this.offancesTransactions,
      sanctionsTransactions: this.sanctionsTransactions,
      decisionsTransactions: this.decisionsTransactions,
      documentsTransactions: this.documentsTransactions,
      ecrisConvictionId: this.ecrisConvictionId,
      personAliasTransactions: this.personAliasTransactions,
      nationalities: this.nationalities.group,
      address: this.address.group,
      prevSuspSent: this.prevSuspSent,
      noSanction: this.noSanction,
    });
  }

  private initFormControls(): void {
    this.id = new FormControl(null);
    this.registrationNumber = new FormControl(null);

    this.csAuthorityName = new FormControl(null);
    this.sequentialIndex = new FormControl(null);
    this.statusIdDisplay = new FormControl(BulletinStatusTypeEnum.NewOffice);
    this.alphabeticalIndex = new FormControl(null);
    this.ecrisConvictionId = new FormControl(null);
    this.bulletinReceivedDate = new FormControl(null);
    this.bulletinType = new FormControl(null);
    this.bulletinAuthorityId = new FormControl(null);
    this.bulletinCreateDate = new FormControl(null);
    this.createdByNames = new FormControl(null);
    this.createdByPosition = new FormControl(null);
    this.approvedByNames = new FormControl(null);
    this.approvedByPosition = new FormControl(null);
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
    this.birthDatePrecision = new FormControl(null);
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
    this.idDocIssuingDatePrec = new FormControl(null);
    this.idDocValidDate = new FormControl(null);
    this.idDocValidDatePrec = new FormControl(null);
    this.motherFirstname = new FormControl(null);
    this.motherSurname = new FormControl(null);
    this.motherFamilyname = new FormControl(null);
    this.motherFullname = new FormControl(null);
    this.fatherFirstname = new FormControl(null);
    this.fatherSurname = new FormControl(null);
    this.fatherFamilyname = new FormControl(null);
    this.fatherFullname = new FormControl(null);
    this.decisionTypeId = new FormControl(null);
    this.decisionNumber = new FormControl(null);
    this.decisionDate = new FormControl(null);
    this.decisionFinalDate = new FormControl(null);
    this.decidingAuthId = new FormControl(null);
    this.decisionEcli = new FormControl(null);
    this.caseTypeId = new FormControl(null);
    this.caseNumber = new FormControl(null);
    this.caseYear = new FormControl(null);
    this.convRemarks = new FormControl(null);
    this.statusId = new FormControl(BulletinStatusTypeEnum.NewOffice);
    this.offancesTransactions = new FormControl(null);
    this.sanctionsTransactions = new FormControl(null);
    this.decisionsTransactions = new FormControl(null);
    this.documentsTransactions = new FormControl(null);
    this.noSanction = new FormControl(null);
    this.prevSuspSent = new FormControl(null);
  }
}
