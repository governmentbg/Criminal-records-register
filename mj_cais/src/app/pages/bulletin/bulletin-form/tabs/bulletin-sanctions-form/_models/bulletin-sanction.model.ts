import { Transaction } from "@infragistics/igniteui-angular";
import { BaseModel } from "../../../../../../@core/models/common/base.model";
import { BulletinProbationModel } from "./bulletin-probation.model";

export class BulletinSanctionModel extends BaseModel {
  public sanctCategoryId: string = null;
  public sanctCategoryName: string = null;
  public ecrisSanctCategId: string = null;
  public ecrisSanctCategName: string = null;
  public descr: string = null;
  public decisionDurationYears: number = null;
  public decisionDurationMonths: number = null;
  public decisionDurationDays: number = null;
  public decisionDurationHours: number = null;
  public fineAmount: number = null;
  public detenctionDescr: string = null;
  public suspentionDurationYears: number = null;
  public suspentionDurationMonths: number = null;
  public suspentionDurationDays: number = null;
  public probations: BulletinProbationModel[];
  public probationsTransactions: Transaction[];

  constructor(init?: Partial<BulletinSanctionModel>) {
    super(init);
    this.sanctCategoryId = init?.sanctCategoryId ?? null;
    this.sanctCategoryName = init?.sanctCategoryName ?? null;
    this.ecrisSanctCategId = init?.ecrisSanctCategId ?? null;
    this.ecrisSanctCategName = init?.ecrisSanctCategName ?? null;
    this.descr = init?.descr ?? null;
    this.decisionDurationYears = init?.decisionDurationYears ?? null;
    this.decisionDurationMonths = init?.decisionDurationMonths ?? null;
    this.decisionDurationDays = init?.decisionDurationDays ?? null;
    this.decisionDurationHours = init?.decisionDurationHours ?? null;
    this.fineAmount = init?.fineAmount ?? null;
    this.detenctionDescr = init?.detenctionDescr ?? null;
    this.suspentionDurationYears = init?.suspentionDurationYears ?? null;
    this.suspentionDurationMonths = init?.suspentionDurationMonths ?? null;
    this.suspentionDurationDays = init?.suspentionDurationDays ?? null;
    this.probations = init?.probations ?? null;
    this.probationsTransactions = init?.probationsTransactions ?? null;
  }
}
