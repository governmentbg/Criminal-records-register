import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";

@Injectable({
  providedIn: "root",
})
export class BulletinSearchService extends CaisCrudService<null, string> {
  constructor(injector: Injector) {
    super(null, injector, "bulletins");
  }
}
