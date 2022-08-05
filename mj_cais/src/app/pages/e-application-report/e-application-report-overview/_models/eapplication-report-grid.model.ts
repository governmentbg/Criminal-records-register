import { BaseGridModel } from "../../../../@core/models/common/base-grid.model";

export class EApplicationReportGridModel extends BaseGridModel {
  public id: string;
  public registrationNumber: string;
  public pid: string;
  public pidType: string;
  public resultType: string;
  public apiServiceCallId: number;
  public cServiceUri: string;
  public cServiceType: string;
  public cEmplId: string;
  public cEmpNames: string;
  public cEmpAddId: string;
  public cEmpPos: string;
  public cRespPersId: string;
  public cLawReason: string;
  public cRemark: string;
  public cAdministrationOid: string;
  public cAdministrationName: string;
  public resultId: string;

  constructor(init?: Partial<EApplicationReportGridModel>) {
    super(init);
    this.id = init.id ?? null;
    this.registrationNumber = init.registrationNumber ?? null;
    this.pid = init.pid ?? null;
    this.pidType = init.pidType ?? null;
    this.resultType = init.resultType ?? null;
    this.apiServiceCallId = init.apiServiceCallId ?? null;
    this.cServiceUri = init.cServiceUri ?? null;
    this.cServiceType = init.cServiceType ?? null;
    this.cEmplId = init.cEmplId ?? null;
    this.cEmpNames = init.cEmpNames ?? null;
    this.cEmpAddId = init.cEmpAddId ?? null;
    this.cEmpPos = init.cEmpPos ?? null;
    this.cRespPersId = init.cRespPersId ?? null;
    this.cLawReason = init.cLawReason ?? null;
    this.cRemark = init.cRemark ?? null;
    this.cAdministrationOid = init.cAdministrationOid ?? null;
    this.cAdministrationName = init.cAdministrationName ?? null;
    this.resultId = init.resultId ?? null;
  }
}
