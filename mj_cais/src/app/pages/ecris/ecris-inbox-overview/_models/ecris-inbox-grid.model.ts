import { BaseGridModel } from "../../../../@core/models/common/base-grid.model";

export class EcrisInboxGridModel extends BaseGridModel {
  public status: string = null;
  public statusName: string = null;
  public importedOn: Date = null;
  public ecrisMsgId: string = null;
  public hasError: boolean = null;

  constructor(init?: Partial<EcrisInboxGridModel>) {
    super(init);
    this.status = init?.status ?? null;
    this.statusName = init?.statusName ?? null;
    this.importedOn = init?.importedOn ?? null;
    this.ecrisMsgId = init?.ecrisMsgId ?? null;
    this.hasError = init?.hasError ?? null;
  }
}
