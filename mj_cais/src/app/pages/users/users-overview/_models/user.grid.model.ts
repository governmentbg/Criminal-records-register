export class UserGridModel {
  public id: string = null;
  public firstname: string = null;
  public surname: string = null;
  public familyname: string = null;
  public active: boolean = null;
  public email: string = null;
  public egn: string = null;
  public position: string = null;
  public csAuthorityId: string = null;
  public authorityName: string = null;

  constructor(init?: Partial<UserGridModel>) {
    this.id = init?.id ?? null;
    this.firstname = init?.firstname ?? null;
    this.surname = init?.surname ?? null;
    this.familyname = init?.familyname ?? null;
    this.active = init?.active ?? null;
    this.email = init?.email ?? null;
    this.egn = init?.egn ?? null;
    this.position = init?.position ?? null;
    this.csAuthorityId = init?.csAuthorityId ?? null;
    this.authorityName = init?.authorityName ?? null;
  }
}
  