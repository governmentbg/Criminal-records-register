export class ApplicationCountModel {
  public newId: number = null;
  public checkPayment: number = null;
  public checkTaxFree: number = null;
  public bulletinsCheck: number = null;
  public taxFree: number = null;
  public forSigningByJudge: number = null;
  public bulletinSelection: number = null;
  
  constructor(init?: Partial<ApplicationCountModel>) {
    this.newId = init?.newId ?? null;
    this.checkPayment = init?.checkPayment ?? null;
    this.checkTaxFree = init?.checkTaxFree ?? null;
    this.bulletinsCheck = init?.bulletinsCheck ?? null;
    this.taxFree = init?.taxFree ?? null;
    this.forSigningByJudge = init?.forSigningByJudge ?? null;
    this.bulletinSelection = init?.bulletinSelection ?? null;
  }
}
