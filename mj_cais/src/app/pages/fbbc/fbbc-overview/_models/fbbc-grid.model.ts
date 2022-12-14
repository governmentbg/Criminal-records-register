import { BaseGridModel } from "../../../../@core/models/common/base-grid.model";

export class FbbcGridModel extends BaseGridModel {
  public receiveDate: Date = null;
  public egn: string = null;
  public birthDate: Date = null;
  public birthCityId: string = null;
  public birthCountryId: string = null;
  public firstname: string = null;
  public surname: string = null;
  public familyname: string = null;
  public docType: string = null;
  public destroyedDate: Date = null;

  constructor(init?: Partial<FbbcGridModel>) {
    super(init);
    if (init) {
      this.receiveDate = init.receiveDate ?? null;
      this.egn = init.egn ?? null;
      this.birthDate = init.birthDate ?? null;
      this.birthCityId = init.birthCityId ?? null;
      this.birthCountryId = init.birthCountryId ?? null;
      this.firstname = init.firstname ?? null;
      this.surname = init.surname ?? null;
      this.familyname = init.familyname ?? null;
      this.docType = init.docType ?? null;
      this.destroyedDate = init.destroyedDate ?? null;
    }
  }
}
