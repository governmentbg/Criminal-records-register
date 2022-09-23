export class InternalRequestCountModel {
  public inboxCount: number = null;
  public outboxCount: number = null;

  constructor(init?: Partial<InternalRequestCountModel>) {
    this.inboxCount = init?.inboxCount ?? null;
    this.outboxCount = init?.outboxCount ?? null;
  }
}
