import { LookupModel } from "../../../../@core/components/forms/inputs/lookup/models/lookup.model";
import { BaseModel } from "../../../../@core/models/common/base.model";

export class InternalRequestModel extends BaseModel {
  public regNumber: string = null;
  public regNumberDisplay: Date = null;
  public description: string = null;
  public reqStatusCode: string = null;
  public responseDescr: string = null;
  public requestDate: string = null;
  public fromAuthorityId: number = null;
  public toAuthorityId: string = null;
  public nIntReqTypeId: string = null;
  public pPersIdId: LookupModel;

  constructor(init?: Partial<InternalRequestModel>) {
    super(init);
    this.regNumber = init?.regNumber ?? null;
    this.regNumberDisplay = init?.regNumberDisplay ?? null;
    this.description = init?.description ?? null;
    this.reqStatusCode = init?.reqStatusCode ?? null;
    this.responseDescr = init?.responseDescr ?? null;
    this.requestDate = init?.requestDate ?? null;
    this.pPersIdId = init?.pPersIdId ?? null;
    this.fromAuthorityId = init?.fromAuthorityId ?? null;
    this.toAuthorityId = init?.toAuthorityId ?? null;
    this.nIntReqTypeId = init?.nIntReqTypeId ?? null;
  }
}
