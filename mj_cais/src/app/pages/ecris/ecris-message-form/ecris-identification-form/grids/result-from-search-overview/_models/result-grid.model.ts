import { BaseGridModel } from "../../../../../../../@core/models/common/base-grid.model";

export class ResultGridModel {
  public id: number = null;
  public firstname: string = null;
  public surname: string = null;
  public familyname: string = null;
  public birthDate: Date = null;
  public createdOn: Date = null;

  constructor(init?: Partial<ResultGridModel>) {
    this.id = init.id ?? null;
    this.firstname = init?.firstname ?? null;
    this.surname = init?.surname ?? null;
    this.familyname = init?.familyname ?? null;
    this.birthDate = init?.birthDate ?? null;
  }
}
