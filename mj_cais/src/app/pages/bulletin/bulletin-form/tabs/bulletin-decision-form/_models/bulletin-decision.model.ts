export class BulletinDecisionModel {
  public id: string = null;
  public decisionChTypeId: string = null;
  public decisionChTypeName: string = null;
  public decisionEcli: string = null;
  public decisionNumber: string = null;
  public decisionDate: Date = null;
  public decisionFinalDate: Date = null;
  public decisionAuthId: string = null;
  public decisionAuthName: string = null;
  public decisionTypeId: string = null;
  public decisionTypeName: string = null;
  public descr: string = null;
  public changeDate: Date = null;

  constructor(init?: Partial<BulletinDecisionModel>) {
    if (init) {
      this.id = init.id ?? null;
      this.decisionChTypeId = init.decisionChTypeId ?? null;
      this.decisionChTypeName = init.decisionChTypeName ?? null;
      this.decisionEcli = init.decisionEcli ?? null;
      this.decisionNumber = init.decisionNumber ?? null;
      this.decisionDate = init.decisionDate ?? null;
      this.decisionFinalDate = init.decisionFinalDate ?? null;
      this.decisionAuthId = init.decisionAuthId ?? null;
      this.decisionAuthName = init.decisionAuthName ?? null;
      this.decisionTypeId = init.decisionTypeId ?? null;
      this.decisionTypeName = init.decisionTypeName ?? null;
      this.descr = init.descr ?? null;
      this.changeDate = init.changeDate ?? null;
    }
  }
}
