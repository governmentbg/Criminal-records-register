import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { BulletinGridModel } from "../models/bulletin-grid.model";

@Injectable({ providedIn: "root" })
export class BulletinGridService extends CaisCrudService<
  BulletinGridModel,
  string
> {
  constructor(injector: Injector) {
    super(BulletinGridModel, injector, "bulletins");
  }
}
