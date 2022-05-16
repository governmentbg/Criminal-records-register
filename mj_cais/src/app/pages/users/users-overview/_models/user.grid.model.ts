export class UserGridModel {
  public id: string = null;
  public firstname: string = null;
  public surname: string = null;
  public familyname: string = null;
  public active: boolean = null;
  public email: string = null;
  public egn: string = null;
  public position: string = null;
  public authorityName: string = null;
  public roles: string[] = [];

  constructor(init?: Partial<UserGridModel>) {
    this.id = init?.id ?? null;
    this.firstname = init?.firstname ?? null;
    this.surname = init?.surname ?? null;
    this.familyname = init?.familyname ?? null;
    this.active = init?.active ?? null;
    this.email = init?.email ?? null;
    this.egn = init?.egn ?? null;
    this.position = init?.position ?? null;
    this.authorityName = init?.authorityName ?? null;
    if(init?.roles){
      this.roles.push(...init.roles);
    }
  }
}
  