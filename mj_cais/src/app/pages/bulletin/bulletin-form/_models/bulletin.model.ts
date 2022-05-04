import { PersonModel } from "../../../../@core/components/forms/person-form/_models/person.model";

export class BulletinModel {
  public id: string = null;
  public registrationNumber: string = null;
  public csAuthorityName: string = null;
  public sequentialIndex: number = null;
  public statusId: string = null;
  public statusIdDisplay: string = null;
  public alphabeticalIndex: string = null;
  public ecrisConvictionId: string = null;
  public bulletinReceivedDate: Date = null;
  public bulletinType: string = null;
  public bulletinAuthorityId: string = null;
  public bulletinCreateDate: Date = null;
  public createdByNames: string = null;
  public createdByPosition: string = null;
  public approvedByNames: string = null;
  public approvedByPosition: string = null;
  public person: PersonModel;
  public decisionTypeId: string = null;
  public decisionNumber: string = null;
  public decisionDate: Date = null;
  public decisionFinalDate: Date = null;
  public decidingAuthId: string = null;
  public decisionEcli: string = null;

  public caseTypeId: string = null;
  public caseNumber: string = null;
  public caseYear: number = null;
  public caseAuthId: string = null;
  public convRemarks: string = null;

  public noSanction: boolean = null;
  public prevSuspSent: boolean = null;
  public prevSuspSentDescr: string = null;
  public locked: boolean = null;

  constructor(init?: Partial<BulletinModel>) {
    this.id = init?.id ?? null;
    this.registrationNumber = init?.registrationNumber ?? null;
    this.csAuthorityName = init?.csAuthorityName ?? null;
    this.sequentialIndex = init?.sequentialIndex ?? null;
    this.statusId = init?.statusId ?? null;
    this.statusIdDisplay = init?.statusId ?? null;
    this.alphabeticalIndex = init?.alphabeticalIndex ?? null;
    this.bulletinReceivedDate = init?.bulletinReceivedDate ?? null;
    this.bulletinType = init?.bulletinType ?? null;
    this.bulletinAuthorityId = init?.bulletinAuthorityId ?? null;
    this.bulletinCreateDate = init?.bulletinCreateDate ?? null;
    this.createdByNames = init?.createdByNames ?? null;
    this.createdByPosition = init?.createdByPosition ?? null;
    this.approvedByNames = init?.approvedByNames ?? null;
    this.approvedByPosition = init?.approvedByPosition ?? null;
    this.person = init.person ?? null;
    this.decisionTypeId = init?.decisionTypeId ?? null;
    this.decisionNumber = init?.decisionNumber ?? null;
    this.decisionDate = init?.decisionDate ?? null;
    this.decisionFinalDate = init?.decisionFinalDate ?? null;
    this.decidingAuthId = init?.decidingAuthId ?? null;
    this.decisionEcli = init?.decisionEcli ?? null;
    this.caseTypeId = init?.caseTypeId ?? null;
    this.caseNumber = init?.caseNumber ?? null;
    this.caseYear = init?.caseYear ?? null;
    this.caseAuthId = init?.caseAuthId ?? null;
    this.convRemarks = init?.convRemarks ?? null;
    this.noSanction = init?.noSanction ?? null;
    this.prevSuspSent = init?.prevSuspSent ?? null;
    this.prevSuspSentDescr = init?.prevSuspSentDescr ?? null;
    this.locked = init?.locked ?? null;
  }
}
