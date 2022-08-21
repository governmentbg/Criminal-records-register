import { BaseModel } from "../../../../../../../@core/models/common/base.model";

export class EApplicationCertificateResultModel extends BaseModel {
  public accessCode1: string = null;
  // public docName: string = null;
  // public docType: string = null;
  public registrationNumber: string = null;

  public validFrom: Date = null;
  public validTo: Date = null;

  constructor(init?: Partial<EApplicationCertificateResultModel>) {
    super(init);

    // this.docName = init?.docName ?? null;
    // debugger;
    // this.docType = init?.docType?.split('/')[1] ?? null;
    this.registrationNumber = init?.registrationNumber ?? null;
    this.accessCode1 = init?.accessCode1 ?? null;
    this.validFrom = init?.validFrom ?? null;
    this.validTo = init?.validTo ?? null;
  }
}
