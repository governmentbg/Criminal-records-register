import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/cais-crud.service";
import { BulletinGridModel } from "./bulletin-grid.model";

@Injectable({ providedIn: "root" })
export class BulletinGridService extends CaisCrudService<
  BulletinGridModel,
  string
> {
  constructor(injector: Injector) {
    super(BulletinGridModel, injector, "bulletins");
  }
}
