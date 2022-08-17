import { BaseModel } from "../../../../@core/models/common/base.model";

export class EcrisOutboxModel extends BaseModel {
  public status: string = null;
  public xmlObject: string = null;
  public operation: string = null;
  public executionDate: string = null;
  public hasError: string = null;
  public error: string = null;
  public stackTrace: string = null;
  public ecrisMsgId: string = null;
  public attempts: number = null;

  constructor(init?: Partial<EcrisOutboxModel>) {
    super(init);
    this.status = init?.status ?? null;
    this.xmlObject = init?.xmlObject ?? null;
    this.ecrisMsgId = init?.ecrisMsgId ?? null;
    this.hasError = init?.hasError ?? null;
    this.error = init?.error ?? null;
    this.stackTrace = init?.stackTrace ?? null;
    this.operation = init?.operation ?? null;
    this.executionDate = init?.executionDate ?? null;
    this.attempts = init?.attempts ?? null;
  }
}
