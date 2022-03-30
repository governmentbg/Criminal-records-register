export class OffenceCategoryGridModel {
  public id: number = null;
  public name: string = null;
  public code: string = null;

  constructor(init?: Partial<OffenceCategoryGridModel>) {
    if (init) {
      this.id = init.id;
      this.name = init.name;
      this.code = init.code;
    }
  }
}
