import { BaseModel } from "../../../../@core/models/common/base.model";

export class InternalRequestGridModel extends BaseModel {
  public regNumber: string = null;
  public requestDate: string = null;
  public reqStatus: string = null;
  public reqStatusCode: string = null;
  public description: string = null;
  public bulletinNumber: string = null;
  public firstName: Date = null;
  public surName: string = null;
  public familyName: string = null;
  public bulletinId: string = null;

  constructor(init?: Partial<InternalRequestGridModel>) {
    super(init);
    if (init) {
      this.regNumber = init.regNumber ?? null;
      this.requestDate = init.requestDate ?? null;
      this.reqStatus = init.reqStatus ?? null;
      this.reqStatusCode = init.reqStatusCode ?? null;
      this.description = init.description ?? null;
      this.bulletinNumber = init.bulletinNumber ?? null;
      this.firstName = init.firstName ?? null;
      this.surName = init.surName ?? null;
      this.familyName = init.familyName ?? null;
      this.bulletinId = init.bulletinId ?? null;
    }
  }
}
