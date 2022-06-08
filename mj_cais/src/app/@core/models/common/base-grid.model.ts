import { BaseModel } from "./base.model";

export class BaseGridModel extends BaseModel {
  public createdOn: Date = null;

  constructor(init?: Partial<BaseGridModel>) {
    super(init);
    this.createdOn = init?.createdOn ?? null;
  }
}
