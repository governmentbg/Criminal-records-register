import { BaseModel } from "../../../../@core/models/common/base.model";

export class RoleModel extends BaseModel {
  public code: string = null;
  public name: string = null;

  constructor(init?: Partial<RoleModel>) {
    super(init);
    this.code = init?.code ?? null;
    this.name = init?.name ?? null;
  }
}
