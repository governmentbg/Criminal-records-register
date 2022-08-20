import { BaseModel } from "../../../../@core/models/common/base.model";

export class AdministrationsExtUICModel extends BaseModel {
  public name: string = null;
  public value: string = null;

  constructor(init?: Partial<AdministrationsExtUICModel>) {
    super(init);
    this.name = init?.name ?? null;
    this.value = init?.value ?? null;
  }
}
