import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/cais-crud.service";
import { BulletinModel } from "./bulletin.model";

@Injectable({ providedIn: "root" })
export class BulletinGridService extends CaisCrudService<
  BulletinModel,
  string
> {
  constructor(injector: Injector) {
    super(BulletinModel, injector, "bulletins");
  }
}
