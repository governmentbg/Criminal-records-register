import { BaseGridModel } from "../../../../@core/models/common/base-grid.model";

export class UserExternalGridModel extends BaseGridModel {
  public name: string = null;
  public active: boolean = null;
  public isAdmin: boolean = null;
  public email: string = null;
  public userName: string = null;
  public egn: string = null;
  public position: string = null;
  public administrationName: string = null;
  public hasRegRegCertSubject: boolean = null;

  constructor(init?: Partial<UserExternalGridModel>) {
    super(init);
    this.name = init?.name ?? null;
    this.active = init?.active ?? null;
    this.isAdmin = init?.isAdmin ?? null;
    this.email = init?.email ?? null;
    this.egn = init?.egn ?? null;
    this.position = init?.position ?? null;
    this.administrationName = init?.administrationName ?? null;
    this.hasRegRegCertSubject = init?.hasRegRegCertSubject ?? null;
    this.userName = init?.userName ?? null;
  }
}
