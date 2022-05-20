import { BaseModel } from "../../../../@core/models/common/base.model";

export class UsersExternalModel extends BaseModel {
  public name: string = null;
  public active: boolean = null;
  public isAdmin: boolean = null;
  public email: string = null;
  public egn: string = null;
  public position: string = null;
  public administrationId: string = null;

  constructor(init?: Partial<UsersExternalModel>) {
    super(init);
    this.name = init?.name ?? null;
    this.active = init?.active ?? null;
    this.isAdmin = init?.isAdmin ?? null;
    this.email = init?.email ?? null;
    this.egn = init?.egn ?? null;
    this.position = init?.position ?? null;
    this.administrationId = init?.administrationId ?? null;
  }
}
