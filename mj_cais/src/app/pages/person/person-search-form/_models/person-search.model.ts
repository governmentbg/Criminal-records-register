import { AddressModel } from "../../../../@core/components/forms/address-form/_model/address.model";
import { DatePrecisionModel } from "../../../../@core/components/forms/inputs/date-precision/_models/date-precision.model";
import { BaseModel } from "../../../../@core/models/common/base.model";

export class PersonSearchModel extends BaseModel {
  public firstname: string = null;
  public surname: string = null;
  public familyname: string = null;
  public fullname: string = null;
  public pid: string = null;
  public birthDate: DatePrecisionModel = null;
  public sex: string = null;

  constructor(init?: Partial<PersonSearchModel>) {
    super(init);
    this.firstname = init?.firstname ?? null;
    this.surname = init?.surname ?? null;
    this.familyname = init?.familyname ?? null;
    this.fullname = init?.fullname ?? null;
    this.pid = init?.pid ?? null;
    this.birthDate = init?.birthDate ?? null;
    this.sex = init?.sex ?? null;
  }
}
