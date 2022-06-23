import { BaseGridModel } from "../../../../@core/models/common/base-grid.model";

export class EApplicationGridModel extends BaseGridModel {
  public createdOn: Date;
  public registrationNumber: string;
  public egn: string;
  public purpose: string;
  public purposeDesc: string;
  public paymentMethodName: string;
  public statusName: string;

  constructor(init?: Partial<EApplicationGridModel>) {
    super(init);
    this.createdOn = init?.createdOn ?? null;
    this.registrationNumber = init?.registrationNumber ?? null;
    this.egn = init?.egn ?? null;
    this.purpose = init?.purpose ?? null;
    this.purposeDesc = init?.purposeDesc ?? null;
    this.paymentMethodName = init?.paymentMethodName ?? null;
    this.statusName = init?.statusName ?? null;
  }
}
