import { BaseModel } from "../../../../@core/models/common/base.model";

export class EcrisInboxModel extends BaseModel {
  public status: string = null;
  public importedOn: string = null;
  public ecrisMsgId: string = null;
  public hasError: string = null;
  public error: string = null;
  public stackTrace: string = null;
  public xmlMessage: string = null;
  public xmlMessageTraits: string = null;

  constructor(init?: Partial<EcrisInboxModel>) {
    super(init);
    this.status = init?.status ?? null;
    this.importedOn = init?.importedOn ?? null;
    this.ecrisMsgId = init?.ecrisMsgId ?? null;
    this.hasError = init?.hasError ?? null;
    this.error = init?.error ?? null;
    this.stackTrace = init?.stackTrace ?? null;
    this.xmlMessage = init?.xmlMessage ?? null;
    this.xmlMessageTraits = init?.xmlMessageTraits ?? null;
  }
}
