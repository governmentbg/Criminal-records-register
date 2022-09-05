import { BaseModel } from "../../../../@core/models/common/base.model";

export class UsersExternalModel extends BaseModel {
  public name: string = null;
  public active: boolean = null;
  public isAdmin: boolean = null;
  public email: string = null;
  public egn: string = null;
  public userName: string = null;
  public position: string = null;
  public password: string = null;
  public confirmPassword: string = null;
  public administrationId: string = null;
  public regCertSubject: string = null;
  public uic: string = null;
  public ou: string = null;

  constructor(init?: Partial<UsersExternalModel>) {
    super(init);
    this.name = init?.name ?? null;
    this.active = init?.active ?? null;
    this.isAdmin = init?.isAdmin ?? null;
    this.email = init?.email ?? null;
    this.egn = init?.egn ?? null;
    this.userName = init?.userName ?? null;
    this.position = init?.position ?? null;
    this.password = init?.password ?? null;
    this.confirmPassword = init?.confirmPassword ?? null;
    this.administrationId = init?.administrationId ?? null;
    this.regCertSubject = init?.regCertSubject ?? null;
    if (this.regCertSubject){

      const {groups: { ntrbg}} =  /(NTRBG-(?<ntrbg>[0-9]*))/.exec(this.regCertSubject);
      this.uic = ntrbg;
    }
  }
}
