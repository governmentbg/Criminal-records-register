import { BaseNomenclatureModel } from "../../../../@core/models/nomenclature/base-nomenclature.model";
import { PersonBulletinsGridModel } from "./person-bulletin-grid-model";

export class PersonBulletinsGridExtendedModel {
  public pids: BaseNomenclatureModel[] = null;
  public bulletins: PersonBulletinsGridModel[] = null;

  constructor(init?: Partial<PersonBulletinsGridExtendedModel>) {
    this.pids = init?.pids ?? null;
    this.bulletins = init?.bulletins ?? null;
  }
}
