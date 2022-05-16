export class BaseNomenclatureModel {
  public id: string | number = null;
  public code: string = null;
  public name: string = null;
  public nameEn: string = null;
  public type: string = null;
  public validFrom: Date = null;
  public validTo: Date = null;

  constructor(init?: Partial<BaseNomenclatureModel>) {
    this.id = init?.id ?? null;
    this.code = init?.code ?? null;
    this.name = init?.name ?? null;
    this.nameEn = init?.nameEn ?? null;
    this.type = init?.type ?? null;
    this.validFrom = init?.validFrom ?? null;
    this.validTo = init?.validTo ?? null;
  }
}
