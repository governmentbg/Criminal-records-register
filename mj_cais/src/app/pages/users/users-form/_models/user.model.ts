import { MultipleChooseModel } from "../../../../@core/components/forms/inputs/multiple-choose/models/multiple-choose.model";
import { BaseModel } from "../../../../@core/models/common/base.model";

export class UserModel extends BaseModel {
  public firstname: string = null;
  public surname: string = null;
  public familyname: string = null;
  public active: boolean = null;
  public email: string = null;
  public egn: string = null;
  public position: string = null;
  public csAuthorityId: string = null;
  public roles: MultipleChooseModel = new MultipleChooseModel();

  constructor(init?: Partial<UserModel>) {
    super(init);
    this.firstname = init?.firstname ?? null;
    this.surname = init?.surname ?? null;
    this.familyname = init?.familyname ?? null;
    this.active = init?.active ?? null;
    this.email = init?.email ?? null;
    this.egn = init?.egn ?? null;
    this.position = init?.position ?? null;
    this.csAuthorityId = init?.csAuthorityId ?? null;
    this.roles = init?.roles ?? null;
  }
}
