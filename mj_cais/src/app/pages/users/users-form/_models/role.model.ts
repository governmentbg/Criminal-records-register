export class RoleModel {
    public id: string = null;
    public code: string = null;
    public name: string = null;

    constructor(init?: Partial<RoleModel>) {
      this.id = init?.id ?? null;
      this.code = init?.code ?? null;
      this.name = init?.name ?? null;
    }
  }
    