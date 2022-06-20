export class EcrisCountModel {
  public forIdentification: number = null;
  public waitingForCSAuthority: number = null;

  constructor(init?: Partial<EcrisCountModel>) {
    this.forIdentification = init?.forIdentification ?? null;
    this.waitingForCSAuthority = init?.waitingForCSAuthority ?? null;
  }
}
