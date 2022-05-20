export class BaseModel {
  public id: string = null;
  public version: number = null;

  constructor(init?: Partial<BaseModel>) {
    this.id = init?.id ?? null;
    this.version = init?.version ?? null;
  }
}
