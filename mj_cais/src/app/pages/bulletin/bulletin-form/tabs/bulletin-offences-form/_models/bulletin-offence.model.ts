import { AddressModel } from "../../../../../../@core/components/forms/address-form/_model/address.model";
import { LookupModel } from "../../../../../../@core/components/forms/inputs/lookup/models/lookup.model";
import { BaseModel } from "../../../../../../@core/models/common/base.model";

export class BulletinOffenceModel extends BaseModel {
  public offenceCategory: LookupModel;
  public remarks: string = null;
  public ecrisOffCatId: string = null;
  public ecrisOffCatName: string = null;
  public offStartDate: Date = null;
  public offEndDate: Date = null;
  public formOfGuiltId: string = null;
  public formOfGuiltName: string = null;
  public offPlace: AddressModel = null;
  public legalProvisions: string = null;

  constructor(init?: Partial<BulletinOffenceModel>) {
    super(init);
    this.offenceCategory = init?.offenceCategory ?? null;
    this.remarks = init?.remarks ?? null;
    this.ecrisOffCatId = init?.ecrisOffCatId ?? null;
    this.offStartDate = init?.offStartDate ?? null;
    this.offEndDate = init?.offEndDate ?? null;
    this.legalProvisions = init?.legalProvisions ?? null;
    this.ecrisOffCatName = init?.ecrisOffCatName ?? null;
    this.offPlace = init?.offPlace ?? null;
    this.formOfGuiltId = init?.formOfGuiltId ?? null;
    this.formOfGuiltName = init?.formOfGuiltName ?? null;
  }
}
