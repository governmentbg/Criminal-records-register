
export class AdministrationsExtModel {
    public id: string = null;
    public name: string = null;
    public descr: string = null;
  
    constructor(init?: Partial<AdministrationsExtModel>) {
      this.id = init?.id ?? null;
      this.name = init?.name ?? null;
      this.descr = init?.descr ?? null;
    }
  }
    