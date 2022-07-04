import { AddressModel } from "../../../../@core/components/forms/address-form/_model/address.model";
import { BaseModel } from "../../../../@core/models/common/base.model";

export class ReportPersonSearchModel extends BaseModel {
  public firstname: string = null;
  public surname: string = null;
  public familyname: string = null;
  public egn: string = null;
  public lnch: string = null;
  public birthDate: Date = null;
  public birthPlace: AddressModel = null;
  public sex: string = null;
  public idDocNumber: string = null;
  public idDocIssuingDate: Date = null;
  public idDocValidDate: Date = null;
  public nationalityTypeCode: string = null;
  public nationalityCountryId: string = null;

  constructor(init?: Partial<ReportPersonSearchModel>) {
    super(init);
    this.firstname = init?.firstname ?? null;
    this.surname = init?.surname ?? null;
    this.familyname = init?.familyname ?? null;
    this.egn = init?.egn ?? null;
    this.lnch = init?.lnch ?? null;
    this.birthDate = init?.birthDate ?? null;
    this.birthPlace = init?.birthPlace ?? null;
    this.sex = init?.sex ?? null;
    this.idDocNumber = init?.idDocNumber ?? null;
    this.idDocIssuingDate = init?.idDocIssuingDate ?? null;
    this.idDocValidDate = init?.idDocValidDate ?? null;
    this.nationalityTypeCode = init?.nationalityTypeCode ?? null;
    this.nationalityCountryId = init?.nationalityCountryId ?? null;
  }
}
