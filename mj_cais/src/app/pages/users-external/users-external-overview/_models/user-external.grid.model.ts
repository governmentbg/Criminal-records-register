export class UserExternalGridModel {
    public id: string = null;
    public name: string = null;
    public active: boolean = null;
    public isAdmin: boolean = null;
    public email: string = null;
    public egn: string = null;
    public position: string = null;
    public administrationName: string = null;
  
    constructor(init?: Partial<UserExternalGridModel>) {
      this.id = init?.id ?? null;
      this.name = init?.name ?? null;
      this.active = init?.active ?? null;
      this.isAdmin = init?.isAdmin ?? null;
      this.email = init?.email ?? null;
      this.egn = init?.egn ?? null;
      this.position = init?.position ?? null;
      this.administrationName = init?.administrationName ?? null;
    }
  }
    