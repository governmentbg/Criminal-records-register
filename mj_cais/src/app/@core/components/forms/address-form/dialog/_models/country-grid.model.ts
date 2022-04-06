export class CountryGridModel {
  public id: string = null;
  public iso31662Code: string = null;
  public usedForNationality: boolean = null;
  public remark: string = null;
  public name: string = null;
  public nameEn: string = null;
  public validFrom: Date = null;
  public validTo: Date = null;

  constructor(init?: Partial<CountryGridModel>) {
    if (init) {
      this.id = init.id;
      this.iso31662Code = init.iso31662Code;
      this.usedForNationality = init.usedForNationality;
      this.remark = init.remark;
      this.name = init.name;
      this.nameEn = init.nameEn;
      this.validFrom = init.validFrom;
      this.validTo = init.validTo;
    }
  }
}
