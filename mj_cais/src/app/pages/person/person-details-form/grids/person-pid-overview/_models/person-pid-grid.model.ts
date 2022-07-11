import { BaseGridModel } from "../../../../../../@core/models/common/base-grid.model";

export class PersonPidGridModel extends BaseGridModel {
  public type: string = null;
  public pid: Date = null;
  public issuer: string = null;

  constructor(init?: Partial<PersonPidGridModel>) {
    super(init);
    this.type = init?.type ?? null;
    this.pid = init?.pid ?? null;
    this.issuer = init?.issuer ?? null;
  }
}
