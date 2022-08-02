import { BaseGridModel } from "../../../../@core/models/common/base-grid.model";

export class EApplicationReportSearchPersGridModel extends BaseGridModel {
  public id: string;
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
  public fullname: string;
  public birthdate: Date;
  public birthdatePrec: string;
  public birthplace: string;
  public firstname: string;
  public surname: string;
  public familyname: string;
  public apiServiceCallId: number;

  constructor(init?: Partial<EApplicationReportSearchPersGridModel>) {
    super(init);
    this.id = init.id ?? null;
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
    this.fullname = init.fullname ?? null;
    this.birthdate = init.birthdate ?? null;
    this.birthdatePrec = init.birthdatePrec ?? null;
    this.birthplace = init.birthplace ?? null;
    this.firstname = init.firstname ?? null;
    this.surname = init.surname ?? null;
    this.familyname = init.familyname ?? null;
    this.apiServiceCallId = init.apiServiceCallId ?? null;
  }
}
