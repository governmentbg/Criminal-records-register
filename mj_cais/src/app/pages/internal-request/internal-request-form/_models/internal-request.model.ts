import { BaseModel } from "../../../../@core/models/common/base.model";

export class InternalRequestModel extends BaseModel {
  public regNumber: string = null;
  public requestDate: Date = null;
  public description: string = null;
  public bulletinId: string = null;
  public reqStatusCode: string = null;
  public responseDescr: string = null;
  public reqStatusName: string = null;

  constructor(init?: Partial<InternalRequestModel>) {
    super(init);
    if (init) {
      this.regNumber = init.regNumber ?? null;
      this.requestDate = init.requestDate ?? null;
      this.description = init.description ?? null;
      this.bulletinId = init.bulletinId ?? null;
      this.reqStatusCode = init.reqStatusCode ?? null;
      this.responseDescr = init.responseDescr ?? null;
      this.reqStatusName = init.reqStatusName ?? null;
    }
  }
}
