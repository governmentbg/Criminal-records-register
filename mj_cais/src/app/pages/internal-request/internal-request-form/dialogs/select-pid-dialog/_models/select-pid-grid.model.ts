import { BaseGridModel } from "../../../../../../@core/models/common/base-grid.model";

export class SelecrPidGridModel extends BaseGridModel {
  public pid: string = null;
  public pidType: string = null;
  public firstname: string = null;
  public surname: string = null;
  public familyname: string = null;
  public personBirthDate: Date = null;

  constructor(init?: Partial<SelecrPidGridModel>) {
    super(init);
    this.pid = init?.pid;
    this.pidType = init?.pidType;
    this.firstname = init?.firstname;
    this.surname = init?.surname;
    this.familyname = init?.familyname;
    this.personBirthDate = init?.personBirthDate;
  }
}
