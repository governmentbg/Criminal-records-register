import { BaseModel } from "../../../../../../@core/models/common/base.model";

export class ApplicationCertificateResultModel extends BaseModel {
  public applicationId: string = null;
  public statusCode: string = null;
  public statusName: string = null;
  public docName: string = null;
  public docType: string = null;
  public registrationNumber: string = null;
  public firstSignerId: string = null;
  public secondSignerId: string = null;
  public validFrom: Date = null;
  public validTo: Date = null;
  public currentUserAuthId: Date = null;
  public selectedBulletinsIds: string[] = null;

  constructor(init?: Partial<ApplicationCertificateResultModel>) {
    super(init);
    this.applicationId = init?.applicationId ?? null;
    this.statusCode = init?.statusCode ?? null;
    this.statusName = init?.statusName ?? null;
    this.docName = init?.docName ?? null;
    //this.docType = init?.docType.split('/')[1] ?? null;
    this.registrationNumber = init?.registrationNumber ?? null;
    this.firstSignerId = init?.firstSignerId ?? null;
    this.secondSignerId = init?.secondSignerId ?? null;
    this.validFrom = init?.validFrom ?? null;
    this.validTo = init?.validTo ?? null;
    this.currentUserAuthId = init?.currentUserAuthId ?? null;
    this.selectedBulletinsIds = init?.selectedBulletinsIds ?? null;
  }
}
