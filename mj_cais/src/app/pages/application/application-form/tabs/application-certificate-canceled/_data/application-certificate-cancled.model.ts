import { BaseModel } from "../../../../../../@core/models/common/base.model";

export class ApplicationCertificateCanceldModel extends BaseModel {
  public registrationNumber: string = null;
  public statusCode: string = null;
  public FirstSigner: string = null;
  public SecondSigner: string = null;

  constructor(init?: Partial<ApplicationCertificateCanceldModel>) {
    super(init);
    this.registrationNumber = init?.registrationNumber ?? null;
    this.statusCode = init?.statusCode ?? null;
    this.FirstSigner = init?.FirstSigner ?? null;
    this.SecondSigner = init?.SecondSigner ?? null;
  }
}
