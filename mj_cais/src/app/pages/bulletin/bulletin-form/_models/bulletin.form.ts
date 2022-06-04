import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";
import { PersonContextEnum } from "../../../../@core/components/forms/person-form/_models/person-context-enum";
import { PersonForm } from "../../../../@core/components/forms/person-form/_models/person.form";
import { BaseForm } from "../../../../@core/models/common/base.form";
import { BulletinStatusTypeEnum } from "../../bulletin-overview/_models/bulletin-status-type.enum";

export class BulletinForm extends BaseForm {
  public group: FormGroup;

  public registrationNumberDisplay: FormControl;
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
  public person: PersonForm;

  public decisionTypeId: FormControl;
  public decisionNumber: FormControl;
  public decisionDate: FormControl;
  public decisionFinalDate: FormControl;
  public decidingAuthId: FormControl;
  public decisionEcli: FormControl;

  public caseTypeId: FormControl;
  public caseNumber: FormControl;
  public caseYear: FormControl;
  public caseAuthId: FormControl;
  public convRemarks: FormControl;

  public noSanction: FormControl;
  public prevSuspSent: FormControl;
  public prevSuspSentDescr: FormControl;
  public locked: FormControl;

  public offancesTransactions: FormControl;
  public sanctionsTransactions: FormControl;
  public decisionsTransactions: FormControl;

  constructor(bulletinStstus: string, isEdit: boolean, locked: boolean) {
    super();
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
    this.person = new PersonForm(PersonContextEnum.Bulletin, true);
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
    this.person.group.disable();
  }

  private initForEditNewEISS(): void {
    this.person = new PersonForm(PersonContextEnum.Bulletin, true);

    this.csAuthorityName.disable();
    this.statusIdDisplay.disable();
    this.alphabeticalIndex.setValidators([
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
    this.person.group.disable();
  }

  private initUnlocked(): void {
    var guid = Guid.create().toString();
    this.id = new FormControl(guid);
    this.person = new PersonForm(PersonContextEnum.Bulletin, false);
    this.csAuthorityName.disable();
    this.statusIdDisplay.disable();
    this.alphabeticalIndex.setValidators([
      Validators.maxLength(100),
    ]);
    this.ecrisConvictionId.setValidators(Validators.maxLength(50));
    this.bulletinType.setValidators([
      Validators.required,
      Validators.maxLength(50),
    ]);
    this.bulletinAuthorityId.setValidators(Validators.maxLength(50));
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
      version: this.version,
      registrationNumberDisplay: this.registrationNumberDisplay,
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
      person: this.person.group,

      decisionNumber: this.decisionNumber,
      decisionDate: this.decisionDate,
      decisionFinalDate: this.decisionFinalDate,
      decidingAuthId: this.decidingAuthId,
      decisionTypeId: this.decisionTypeId,
      decisionEcli: this.decisionEcli,

      caseTypeId: this.caseTypeId,
      caseNumber: this.caseNumber,
      caseYear: this.caseYear,
      caseAuthId: this.caseAuthId,
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
    this.registrationNumberDisplay = new FormControl(null);
    this.registrationNumberDisplay.disable();
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
