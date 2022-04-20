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
  public statusId: FormControl;
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
  public birthPlace: AddressForm;
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

  public decisionTypeId: FormControl;
  public decisionNumber: FormControl;
  public decisionDate: FormControl;
  public decisionFinalDate: FormControl;
  public decidingAuthId: FormControl;
  public decisionEcli: FormControl;

  public caseTypeId: FormControl;
  public caseNumber: FormControl;
  public caseYear: FormControl;
  public caseAuthId:FormControl;
  public convRemarks: FormControl;

  public noSanction: FormControl;
  public prevSuspSent: FormControl;
  public prevSuspSentDescr: FormControl;
  public locked: FormControl;

  public offancesTransactions: FormControl;
  public sanctionsTransactions: FormControl;
  public decisionsTransactions: FormControl;

  constructor(bulletinStstus: string, isEdit: boolean, locked: boolean) {
    this.initFormControls(locked);
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
    //this.group.disable();
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
    this.birthPlace = new AddressForm(false, true);
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
    this.idDocValidDate.disable();
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
    this.caseAuthId.disable();
    this.convRemarks.disable();
    this.noSanction.disable();
    this.prevSuspSent.disable();
    this.prevSuspSentDescr.disable();
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
    this.birthPlace = new AddressForm(false, true);
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
    this.idDocValidDate.disable();
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
    this.caseAuthId.disable();
    this.convRemarks.disable();
    this.noSanction.disable();
    this.prevSuspSent.disable();
    this.prevSuspSentDescr.disable();
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
    this.birthPlace = new AddressForm(true);
    this.egn.setValidators([Validators.required, Validators.maxLength(100)]);
    this.lnch.setValidators([Validators.required, Validators.maxLength(100)]);
    this.ln.setValidators([Validators.required, Validators.maxLength(100)]);
    this.nationalities = new MultipleChooseForm(true);
    this.afisNumber.setValidators(Validators.maxLength(100));
    this.idDocNumber.setValidators(Validators.maxLength(100));
    this.idDocCategoryId.setValidators(Validators.maxLength(50));
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
  }

  private initGroup(): void {
    this.group = new FormGroup({
      id: this.id,
      registrationNumber: this.registrationNumber,
      csAuthorityName: this.csAuthorityName,
      sequentialIndex: this.sequentialIndex,
      statusId: this.statusId,
      statusIdDisplay: this.statusIdDisplay,
      alphabeticalIndex: this.alphabeticalIndex,
      ecrisConvictionId: this.ecrisConvictionId,
      bulletinReceivedDate: this.bulletinReceivedDate,
      bulletinType: this.bulletinType,
      bulletinAuthorityId: this.bulletinAuthorityId,
      bulletinCreateDate: this.bulletinCreateDate,
      createdByNames: this.createdByNames,
      createdByPosition: this.createdByPosition,
      approvedByNames: this.approvedByNames,
      approvedByPosition: this.approvedByPosition,

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

      decisionNumber: this.decisionNumber,
      decisionDate: this.decisionDate,
      decisionFinalDate: this.decisionFinalDate,
      decidingAuthId: this.decidingAuthId,
      decisionTypeId: this.decisionTypeId,
      decisionEcli: this.decisionEcli,

      caseTypeId: this.caseTypeId,
      caseNumber: this.caseNumber,
      caseYear: this.caseYear,
      caseAuthId:this.caseAuthId,
      convRemarks: this.convRemarks,


      offancesTransactions: this.offancesTransactions,
      sanctionsTransactions: this.sanctionsTransactions,
      decisionsTransactions: this.decisionsTransactions,

      noSanction: this.noSanction,
      prevSuspSent: this.prevSuspSent,
      prevSuspSentDescr: this.prevSuspSentDescr,
      locked: this.locked,
    });
  }

  private initFormControls(locked: boolean): void {
    this.id = new FormControl(null);
    this.registrationNumber = new FormControl(null);
    this.csAuthorityName = new FormControl(null);
    this.sequentialIndex = new FormControl(null);
    this.statusIdDisplay = new FormControl(BulletinStatusTypeEnum.NewOffice);
    this.statusId = new FormControl(BulletinStatusTypeEnum.NewOffice);
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
    this.birthPlace = new AddressForm(false);
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
    
    this.decisionTypeId = new FormControl(null);
    this.decisionNumber = new FormControl(null);
    this.decisionDate = new FormControl(null);
    this.decisionFinalDate = new FormControl(null);
    this.decidingAuthId = new FormControl(null);
    this.decisionEcli = new FormControl(null);
    
    this.caseTypeId = new FormControl(null);
    this.caseNumber = new FormControl(null);
    this.caseYear = new FormControl(null);
    this.caseAuthId = new FormControl(null);
    this.convRemarks = new FormControl(null);

    this.noSanction = new FormControl(null);
    this.prevSuspSent = new FormControl(null);
    this.prevSuspSentDescr = new FormControl(null);
    this.locked = new FormControl(locked);

    this.offancesTransactions = new FormControl(null);
    this.sanctionsTransactions = new FormControl(null);
    this.decisionsTransactions = new FormControl(null);
  }
}
