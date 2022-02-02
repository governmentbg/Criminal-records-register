import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/cais-crud.service";
import { BulletinGridModel } from "./bulletin-grid.model";

@Injectable({
  providedIn: "root",
})
export class BulletinService extends CaisCrudService<
  BulletinGridModel,
  number
> {
  constructor(injector: Injector) {
    super(BulletinGridModel, injector, "bulletins");
  }
}
