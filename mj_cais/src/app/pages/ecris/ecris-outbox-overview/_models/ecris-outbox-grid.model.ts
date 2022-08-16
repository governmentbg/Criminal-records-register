import { BaseGridModel } from "../../../../@core/models/common/base-grid.model";

export class EcrisOutboxGridModel extends BaseGridModel {
  public status: string = null;
  public statusName: string = null;
  public operation: string = null;
  public executionDate: Date = null;
  public hasError: boolean = null;
  public attempts: number = null;
  public ecrisMsgId: string = null;

  constructor(init?: Partial<EcrisOutboxGridModel>) {
    super(init);
    this.status = init?.status ?? null;
    this.statusName = init?.statusName ?? null;
    this.operation = init?.operation ?? null;
    this.executionDate = init?.executionDate ?? null;
    this.hasError = init?.hasError ?? null;
    this.attempts = init?.attempts ?? null;
    this.ecrisMsgId = init?.ecrisMsgId ?? null;
  }
}
