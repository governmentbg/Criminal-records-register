import { BaseGridModel } from "../../../../../../@core/models/common/base-grid.model";

export class PersonApplicationGridModel extends BaseGridModel {
  public registrationNumber: string = null;
  public firstname: string = null;
  public surname: string = null;
  public familyname: string = null;

  constructor(init?: Partial<PersonApplicationGridModel>) {
    super(init);
    this.registrationNumber = init?.registrationNumber ?? null;
    this.firstname = init?.firstname ?? null;
    this.surname = init?.surname ?? null;
    this.familyname = init?.familyname ?? null;
  }
}
