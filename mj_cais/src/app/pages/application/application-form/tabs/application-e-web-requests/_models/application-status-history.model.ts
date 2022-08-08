import { BaseModel } from "../../../../../../@core/models/common/base.model";


export class ApplicationEWebRequestsModel extends BaseModel {
  
  public webServiceName: string;
  public webServiceXslt?: any;
  public executionDate: Date;
  public hasError?: any;
  public error?: any;
  public responseXml: string;
  public applicationId: string;
  public createdOn: Date;
  public apiServiceCallId: string = null;

  constructor(init?: Partial<ApplicationEWebRequestsModel>) {
    super(init);
    this.webServiceName = init?.apiServiceCallId ?? null;
    this.webServiceName = init?.webServiceName ?? null;
    this.executionDate = init?.executionDate ?? null;
    this.createdOn = init?.createdOn ?? null;
  }
}
