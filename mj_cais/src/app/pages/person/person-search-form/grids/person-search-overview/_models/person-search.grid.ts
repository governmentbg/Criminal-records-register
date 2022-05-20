import { BaseModel } from "../../../../../../@core/models/common/base.model";

export class PersonSearchGridModel extends BaseModel {
  public pid: string = null;
  public pidType: string = null;
  public pidTypeName: string = null;
  public firstName: string = null;
  public surName: string = null;
  public familyName: string = null;
  public fullName: string = null;
  public birthDate: Date = null;

  constructor(init?: Partial<PersonSearchGridModel>) {
    super(init);
    this.pid = init?.pid ?? null;
    this.pidType = init?.pidType ?? null;
    this.pidTypeName = init?.pidTypeName ?? null;
    this.firstName = init?.firstName ?? null;
    this.surName = init?.surName ?? null;
    this.familyName = init?.familyName ?? null;
    this.fullName = init?.fullName ?? null;
    this.birthDate = init?.birthDate ?? null;
  }
}
