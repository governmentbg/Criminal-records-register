import { BaseModel } from "../../../../@core/models/common/base.model";
import { AdministrationsExtUICModel } from "./administrations-ext-uic.model";

export class AdministrationsExtModel extends BaseModel {
  public name: string = null;
  public descr: string = null;
  public role: string = null;
  public extAdministrationUics: AdministrationsExtUICModel[] = [];

  constructor(init?: Partial<AdministrationsExtModel>) {
    super(init);
    this.name = init?.name ?? null;
    this.descr = init?.descr ?? null;
    this.role = init?.role ?? null;
    if (init.extAdministrationUics){
      for(let uic of init.extAdministrationUics){
        this.extAdministrationUics.push(new AdministrationsExtUICModel(uic));
      }
    }
  }
}
