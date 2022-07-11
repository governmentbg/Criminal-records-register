import { BaseModel } from "../../../../@core/models/common/base.model";

export class EcrisMsgNationalityModel extends BaseModel {
  public name: string = null;

  constructor(init?: Partial<EcrisMsgNationalityModel>) {
    super(init);
    if (init) {
      this.name = init.name ?? null;
    }
  }
}
