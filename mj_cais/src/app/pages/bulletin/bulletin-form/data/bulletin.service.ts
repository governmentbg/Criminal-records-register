import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { BulletinModel } from "../models/bulletin.model";

@Injectable({ providedIn: "root" })
export class BulletinService extends CaisCrudService<BulletinModel, string> {
  constructor(injector: Injector) {
    super(BulletinModel, injector, "bulletins");
  }
}
