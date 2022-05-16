import { MultipleChooseModel } from "../../../../@core/components/forms/inputs/multiple-choose/models/multiple-choose.model";

export class UserModel {
    public id: string = null;
    public firstname: string = null;
    public surname: string = null;
    public familyname: string = null;
    public active: boolean = null;
    public email: string = null;
    public egn: string = null;
    public position: string = null;
    public csAuthorityId: string = null;
    public roles: MultipleChooseModel = new MultipleChooseModel();
    public version: number = null;
  
    constructor(init?: Partial<UserModel>) {
      this.id = init?.id ?? null;
      this.firstname = init?.firstname ?? null;
      this.surname = init?.surname ?? null;
      this.familyname = init?.familyname ?? null;
      this.active = init?.active ?? null;
      this.email = init?.email ?? null;
      this.egn = init?.egn ?? null;
      this.position = init?.position ?? null;
      this.csAuthorityId = init?.csAuthorityId ?? null;
      this.roles = init?.roles ?? null;
      this.version = init?.version ?? null;
    }
  }
    