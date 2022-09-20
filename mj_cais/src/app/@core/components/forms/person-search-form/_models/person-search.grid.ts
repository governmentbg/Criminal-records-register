import { BaseGridModel } from "../../../../models/common/base-grid.model";

export class PersonSearchGridModel extends BaseGridModel {
  public pids: string = null;
  public personNames: string = null;
  public motherNames: string = null;
  public fatherNames: string = null;
  public sex: string = null;
  public birthDate: string = null;
  public bgCitizen: string = null;
  public nonBGCitizen: string = null;
  public isConvicted: string = null;
  public matchText: string = null;
 
  constructor(init?: Partial<PersonSearchGridModel>) {
    super(init);
    this.pids = init?.pids ?? null;
    this.personNames = init?.personNames ?? null;
    this.motherNames = init?.motherNames ?? null;
    this.fatherNames = init?.fatherNames ?? null;
    this.sex = init?.sex ?? null;
    this.birthDate = init?.birthDate ?? null;
    this.bgCitizen = init?.bgCitizen ?? null;
    this.nonBGCitizen = init?.nonBGCitizen ?? null;
    this.isConvicted = init?.isConvicted ?? null;
    this.matchText = init?.matchText ?? null;
  }
}
