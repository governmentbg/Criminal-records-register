import { PersonModel } from "../../../../@core/components/forms/person-form/_models/person.model";
import { BaseModel } from "../../../../@core/models/common/base.model";

export class ReportApplicationModel extends BaseModel {
  public id: string = null;
  public registrationNumber: string = null;
  public purpose: string = null;
  public personData: PersonModel = null;
  public applicantName: string = null;
  public addrName: string = null;
  public addrStr: string = null;
  public addrDistrict: string = null;
  public addrTown: string = null;
  public addrState: string = null;
  public addrPhone: string = null;
  public addrEmail: string = null;
  public statusCode: string = null;
  public statusName: string = null;
  public csAuthorityId: string = null;
  public csAuthorityName: string = null;
  public applicantDescr: string = null;
  public purposeId: string = null;
  public registrationNumberDisplay: string = null;

  constructor(init?: Partial<ReportApplicationModel>) {
    super(init);
    this.id = init?.id ?? null;
    this.registrationNumber = init?.registrationNumber ?? null;
    this.registrationNumberDisplay = init?.registrationNumberDisplay ?? null;
    this.purpose = init?.purpose ?? null;
    this.personData = init?.personData ?? null;
    this.applicantName = init?.applicantName ?? null;
    this.addrName = init?.addrName ?? null;
    this.addrStr = init?.addrStr ?? null;
    this.addrDistrict = init?.addrDistrict ?? null;
    this.addrTown = init?.addrTown ?? null;
    this.addrState = init?.addrState ?? null;
    this.addrPhone = init?.addrPhone ?? null;
    this.addrEmail = init?.addrEmail ?? null;
    this.csAuthorityId = init?.csAuthorityId ?? null;
    this.csAuthorityName = init?.csAuthorityName ?? null;
    this.applicantDescr = init?.applicantDescr ?? null;
    this.purposeId = init?.purposeId ?? null;
    this.statusName = init?.statusName ?? null;
  }
}
