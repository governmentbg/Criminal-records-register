import { BaseModel } from "../../../../../../@core/models/common/base.model";

export class BulletinProbationModel extends BaseModel {
  public sanctionId: string = null;
  public sanctProbCategId: string = null;
  public sanctProbMeasureId: string = null;
  public sanctProbValue: number = null;
  public decisionDurationYears: number = null;
  public decisionDurationMonths: number = null;
  public decisionDurationDays: number = null;
  public decisionDurationHours: number = null;

  constructor(init?: Partial<BulletinProbationModel>) {
    super(init);
    this.sanctionId = init?.sanctionId ?? null;
    this.sanctProbCategId = init?.sanctProbCategId ?? null;
    this.sanctProbMeasureId = init?.sanctProbMeasureId ?? null;
    this.sanctProbValue = init?.sanctProbValue ?? null;
    this.decisionDurationYears = init?.decisionDurationYears ?? null;
    this.decisionDurationMonths = init?.decisionDurationMonths ?? null;
    this.decisionDurationDays = init?.decisionDurationDays ?? null;
    this.decisionDurationHours = init?.decisionDurationHours ?? null;
  }
}
