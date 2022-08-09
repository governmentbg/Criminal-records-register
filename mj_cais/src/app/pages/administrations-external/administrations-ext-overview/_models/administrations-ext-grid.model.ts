import { BaseGridModel } from "../../../../@core/models/common/base-grid.model";

export class AdministrationsExtGridModel extends BaseGridModel {
  public name: string = null;
  public descr: string = null;
  public role: string = null;

  constructor(init?: Partial<AdministrationsExtGridModel>) {
    super(init);
    this.name = init?.name ?? null;
    this.descr = init?.descr ?? null;
    this.role = init?.role ?? null;
  }
}
