import { BaseModel } from "../../../../@core/models/common/base.model";

export class EcrisTcnGridModel extends BaseModel {
  public action: string = null;
  public createdOn: Date = null;
  public registrationNumber: string = null;
  public status: string = null;
  public identifier: string = null;
  public firstname: string = null;
  public surname: string = null;
  public familyname: string = null;
  public birthDate: Date = null;
  public birthPlace: string = null;
  public bulletinId: string = null;

  constructor(init?: Partial<EcrisTcnGridModel>) {
    super(init);
    if (init) {
      this.action = init.action ?? null;
      this.createdOn = init.createdOn ?? null;
      this.registrationNumber = init.registrationNumber ?? null;
      this.status = init.status ?? null;
      this.identifier = init.identifier ?? null;
      this.firstname = init.firstname ?? null;
      this.surname = init.surname ?? null;
      this.familyname = init.familyname ?? null;
      this.birthDate = init.birthDate ?? null;
      this.birthPlace = init.birthPlace ?? null;
      this.bulletinId = init.bulletinId ?? null;
    }
  }
}
