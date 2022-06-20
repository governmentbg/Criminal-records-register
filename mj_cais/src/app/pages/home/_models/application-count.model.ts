export class ApplicationCountModel {
  public newId: number = null;
  public checkPayment: number = null;
  public checkTaxFree: number = null;
  public bulletinsCheck: number = null;

  constructor(init?: Partial<ApplicationCountModel>) {
    this.newId = init?.newId ?? null;
    this.checkPayment = init?.checkPayment ?? null;
    this.checkTaxFree = init?.checkTaxFree ?? null;
    this.bulletinsCheck = init?.bulletinsCheck ?? null;
  }
}
