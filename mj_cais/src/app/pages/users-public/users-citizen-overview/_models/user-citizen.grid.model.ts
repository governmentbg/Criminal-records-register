import { BaseGridModel } from "../../../../@core/models/common/base-grid.model";

export class UserCitizenGridModel extends BaseGridModel {
  public name: string = null;
  public email: string = null;
  public egn: string = null;

  constructor(init?: Partial<UserCitizenGridModel>) {
    super(init);
    this.name = init?.name ?? null;
    this.email = init?.email ?? null;
    this.egn = init?.egn ?? null;
  }
}
