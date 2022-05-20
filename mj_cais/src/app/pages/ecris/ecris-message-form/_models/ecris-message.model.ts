import { BaseModel } from "../../../../@core/models/common/base.model";

export class EcrisMessageModel extends BaseModel {
  public requestMsgId: string = null;
  public fromAuthId: string = null;
  public toAuthId: string = null;
  public identifier: string = null;
  public ecrisIdentifier: string = null;
  public msgTimestamp: Date = null;
  public responseTypeId: string = null;
  public ecrisMsgStatus: string = null;
  public birthDate: Date = null;
  public birthCountry: string = null;
  public birthCity: string = null;
  public fbbcId: string = null;
  public firstname: string = null;
  public surname: string = null;
  public familyname: string = null;
  public sex: number = null;
  public nationality1Code: string = null;
  public nationality2Code: string = null;
  public msgTypeId: string = null;

  constructor(init?: Partial<EcrisMessageModel>) {
    super(init);
    if (init) {
      this.requestMsgId = init.requestMsgId ?? null;
      this.fromAuthId = init.fromAuthId ?? null;
      this.toAuthId = init.toAuthId ?? null;
      this.identifier = init.identifier ?? null;
      this.ecrisIdentifier = init.ecrisIdentifier ?? null;
      this.msgTimestamp = init.msgTimestamp ?? null;
      this.responseTypeId = init.responseTypeId ?? null;
      this.ecrisMsgStatus = init.ecrisMsgStatus ?? null;
      this.birthDate = init.birthDate ?? null;
      this.birthCountry = init.birthCountry ?? null;
      this.birthCity = init.birthCity ?? null;
      this.fbbcId = init.fbbcId ?? null;
      this.firstname = init.firstname ?? null;
      this.surname = init.surname ?? null;
      this.familyname = init.familyname ?? null;
      this.sex = init.sex ?? null;
      this.nationality1Code = init.nationality1Code ?? null;
      this.nationality2Code = init.nationality2Code ?? null;
      this.msgTypeId = init.msgTypeId ?? null;
    }
  }
}
