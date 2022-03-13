import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { FbbcGridModel } from "../models/fbbc-grid.model";

@Injectable({ providedIn: "root" })
export class FbbcGridService extends CaisCrudService<FbbcGridModel, string> {
  constructor(injector: Injector) {
    super(FbbcGridModel, injector, "fbbcs");
  }
}
