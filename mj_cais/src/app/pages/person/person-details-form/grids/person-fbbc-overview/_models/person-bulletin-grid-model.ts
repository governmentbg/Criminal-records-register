import { BaseGridModel } from "../../../../../../@core/models/common/base-grid.model";

export class PersonFbbcGridModel extends BaseGridModel {
  public docType: string = null;
  public receiveDate: Date = null;
  public country: string = null;
  public egn: string = null;
  public fullName: string = null;
  public birthDate: Date = null;

  constructor(init?: Partial<PersonFbbcGridModel>) {
    super(init);
    this.docType = init?.docType ?? null;
    this.receiveDate = init?.receiveDate ?? null;
    this.country = init?.country ?? null;
    this.egn = init?.egn ?? null;
    this.fullName = init?.fullName ?? null;
    this.birthDate = init?.birthDate ?? null;
  }
}
