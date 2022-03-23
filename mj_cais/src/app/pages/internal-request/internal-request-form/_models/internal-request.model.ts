export class InternalRequestModel {
  public id: string = null;
  public regNumber: string = null;
  public requestDate: Date = null;
  public description: string = null;
  public bulletinId: string = null;

  constructor(init?: Partial<InternalRequestModel>) {
    if (init) {
      this.id = init.id ?? null;
      this.regNumber = init.regNumber ?? null;
      this.requestDate = init.requestDate ?? null;
      this.description = init.description ?? null;
      this.bulletinId = init.bulletinId ?? null;
    }
  }
}