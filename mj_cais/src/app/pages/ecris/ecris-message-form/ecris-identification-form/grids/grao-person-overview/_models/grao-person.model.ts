import { BaseModel } from "../../../../../../../@core/models/common/base.model";

export class GraoPersonModel {
  id: number = null;
  public egn: string = null;
  public firstName: string = null;
  public surname: string = null;
  public familyName: string = null;
  public birthDate: Date = null;
  public createdOn: Date = null;

  constructor(init?: Partial<GraoPersonModel>) {
    this.id = init.id ?? null;
    this.egn = init.egn ?? null;
    this.firstName = init.firstName ?? null;
    this.surname = init.surname ?? null;
    this.familyName = init.familyName ?? null;
    this.birthDate = init.birthDate ?? null;
  }
}
