import { BaseModel } from "../../../../../../@core/models/common/base.model";

export class PersonApplicationGridModel extends BaseModel {
  public registrationNumber: string = null;
  public firstName: string = null;
  public surName: string = null;
  public familyName: string = null;

  constructor(init?: Partial<PersonApplicationGridModel>) {
    super(init);
    this.registrationNumber = init?.registrationNumber ?? null;
    this.firstName = init?.firstName ?? null;
    this.surName = init?.surName ?? null;
    this.familyName = init?.familyName ?? null;
  }
}
