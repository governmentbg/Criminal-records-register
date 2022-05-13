export class UserCitizenGridModel {
    public id: string = null;
    public name: string = null;
    public email: string = null;
    public egn: string = null;
  
    constructor(init?: Partial<UserCitizenGridModel>) {
      this.id = init?.id ?? null;
      this.name = init?.name ?? null;
      this.email = init?.email ?? null;
      this.egn = init?.egn ?? null;
    }
  }
    