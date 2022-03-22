export class BaseNomenclatureModel {
  public id: string | number = null;
  public code: string = null;
  public name: string = null;
  public nameEn: string = null;

  constructor(init?: Partial<BaseNomenclatureModel>) {
    if (init) {
      this.id = init.id ?? null;
      this.code = init.code ?? null;
      this.name = init.name ?? null;
      this.nameEn = init.nameEn ?? null;
    }
  }
}
