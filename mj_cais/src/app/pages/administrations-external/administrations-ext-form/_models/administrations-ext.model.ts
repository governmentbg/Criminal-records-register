import { BaseModel } from "../../../../@core/models/common/base.model";

export class AdministrationsExtModel extends BaseModel {
  public name: string = null;
  public descr: string = null;
  public role: string = null;

  constructor(init?: Partial<AdministrationsExtModel>) {
    super(init);
    this.name = init?.name ?? null;
    this.descr = init?.descr ?? null;
    this.role = init?.role ?? null;
  }
}
