export class CentralAuthCountModel {
  public forIdentification: number = null;
  public tcn: number = null;
  public forDestruction: number = null;
  public webCheckTaxFree: number = null;
  
  constructor(init?: Partial<CentralAuthCountModel>) {
    this.forIdentification = init?.forIdentification ?? null;
    this.tcn = init?.tcn ?? null;
    this.forDestruction = init?.forDestruction ?? null;
    this.webCheckTaxFree = init?.webCheckTaxFree ?? null;
  }
}
