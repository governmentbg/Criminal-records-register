import { BaseGridModel } from "../../../../../../@core/models/common/base-grid.model";

export class PersonHistoryGridModel extends BaseGridModel {
  public name: string;
  public nameLat: string;
  public sex: string;
  public birthDate: Date;
  public birthPlaceOther: string;
  public motherName: string;
  public fatherName: Date;
  public birthCity: string;
  public birthCountry: string;
  public pids: string[];
  public citizenShips: string[];
  public tablename: string;
  public tableId: string;
  public descr: string;

  constructor(init?: Partial<PersonHistoryGridModel>) {
    super(init);
    if (init) {
      this.name = init.name ?? null;
      this.nameLat = init.nameLat ?? null;
      this.sex = init.sex ?? null;
      this.birthDate = init.birthDate ?? null;
      this.birthPlaceOther = init.birthPlaceOther ?? null;
      this.birthDate = init.birthDate ?? null;
      this.motherName = init.motherName ?? null;
      this.fatherName = init.fatherName ?? null;
      this.birthCity = init.birthCity ?? null;
      this.birthCountry = init.birthCountry ?? null;
      this.pids = init.pids ?? null;
      this.citizenShips = init.citizenShips ?? null;
      this.tablename = init.tablename ?? null;
      this.tableId = init.tableId ?? null;
      this.descr = init.descr ?? null;
    }
  }
}
