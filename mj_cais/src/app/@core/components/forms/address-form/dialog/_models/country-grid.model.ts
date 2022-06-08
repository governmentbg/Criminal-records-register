import { BaseGridModel } from "../../../../../models/common/base-grid.model";

export class CountryGridModel extends BaseGridModel {
  public iso31662Code: string = null;
  public usedForNationality: boolean = null;
  public remark: string = null;
  public name: string = null;
  public nameEn: string = null;
  public validFrom: Date = null;
  public validTo: Date = null;

  constructor(init?: Partial<CountryGridModel>) {
    super(init);
    if (init) {
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
