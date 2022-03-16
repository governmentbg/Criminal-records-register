export class BulletinSanctionModel {
  public id: string = null;
  public sanctCategoryId: string = null;
  public sanctCategoryName: string = null;
  public sanctProbCategId: string = null;
  public sanctProbCategName: string = null;
  public ecrisSanctCategId: string = null;
  public ecrisSanctCategName: string = null;
  public sanctProbValue: number = null;
  public sanctProbMeasureId: string = null;
  public sanctProbMeasureName: string = null;
  public descr: string = null;
  public specificToMinor: number = null;
  public decisionStartDate: Date = null;
  public decisionEndDate: Date = null;
  public decisionDurationYears: number = null;
  public decisionDurationMonths: number = null;
  public decisionDurationDays: number = null;
  public decisionDurationHours: number = null;
  public executionStartDate: Date = null;
  public executionEndDate: Date = null;
  public executionDurationYears: number = null;
  public executionDurationMonths: number = null;
  public executionDurationDays: number = null;
  public executionDurationHours: number = null;
  public fineAmount: number = null;
  public detenctionDescr: string = null;
  public suspentionDurationYears: number = null;
  public suspentionDurationMonths: number = null;
  public suspentionDurationDays: number = null;
  public suspentionDurationHours: number = null;
  public probationDescr: string = null;
  public sanctActivityId: string = null;
  public sanctActivityName: string = null;
  public sanctActivityDescr: string = null;

  constructor(init?: Partial<BulletinSanctionModel>) {
    if (init) {
      this.id = init.id ?? null;
      this.sanctCategoryId = init.sanctCategoryId ?? null;
      this.sanctCategoryName = init.sanctCategoryName ?? null;
      this.sanctProbCategId = init.sanctProbCategId ?? null;
      this.sanctProbCategName = init.sanctProbCategName ?? null;
      this.ecrisSanctCategId = init.ecrisSanctCategId ?? null;
      this.ecrisSanctCategName = init.ecrisSanctCategName ?? null;
      this.sanctProbValue = init.sanctProbValue ?? null;
      this.sanctProbMeasureId = init.sanctProbMeasureId ?? null;
      this.sanctProbMeasureName = init.sanctProbMeasureName ?? null;
      this.descr = init.descr ?? null;
      this.specificToMinor = init.specificToMinor ?? null;
      this.decisionStartDate = init.decisionStartDate ?? null;
      this.decisionEndDate = init.decisionEndDate ?? null;
      this.decisionDurationYears = init.decisionDurationYears ?? null;
      this.decisionDurationMonths = init.decisionDurationMonths ?? null;
      this.decisionDurationDays = init.decisionDurationDays ?? null;
      this.decisionDurationHours = init.decisionDurationHours ?? null;
      this.executionStartDate = init.executionStartDate ?? null;
      this.executionEndDate = init.executionEndDate ?? null;
      this.executionDurationYears = init.executionDurationYears ?? null;
      this.executionDurationMonths = init.executionDurationMonths ?? null;
      this.executionDurationDays = init.executionDurationDays ?? null;
      this.executionDurationHours = init.executionDurationHours ?? null;
      this.fineAmount = init.fineAmount ?? null;
      this.detenctionDescr = init.detenctionDescr ?? null;
      this.suspentionDurationYears = init.suspentionDurationYears ?? null;
      this.suspentionDurationMonths = init.suspentionDurationMonths ?? null;
      this.suspentionDurationDays = init.suspentionDurationDays ?? null;
      this.suspentionDurationHours = init.suspentionDurationHours ?? null;
      this.probationDescr = init.probationDescr ?? null;
      this.sanctActivityId = init.sanctActivityId ?? null;
      this.sanctActivityName = init.sanctActivityName ?? null;
      this.sanctActivityDescr = init.sanctActivityDescr ?? null;
    }
  }
}
