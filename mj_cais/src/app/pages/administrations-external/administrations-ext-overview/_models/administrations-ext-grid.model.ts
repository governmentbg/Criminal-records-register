import { BaseModel } from "../../../../@core/models/common/base.model";

export class AdministrationsExtGridModel extends BaseModel {
  public name: string = null;
  public descr: string = null;

  constructor(init?: Partial<AdministrationsExtGridModel>) {
    super(init);
    this.name = init?.name ?? null;
    this.descr = init?.descr ?? null;
  }
}
