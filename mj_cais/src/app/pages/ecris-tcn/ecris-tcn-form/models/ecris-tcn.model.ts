export class EcrisTcnModel {
  public id: string = null;
  public action: string = null;
  public status: string = null;
  public bulletinId: string = null;

  constructor(init?: Partial<EcrisTcnModel>) {
    if (init) {
      this.id = init.id ?? null;
      this.action = init.action ?? null;
      this.status = init.status ?? null;
      this.bulletinId = init.bulletinId ?? null;
    }
  }
}
