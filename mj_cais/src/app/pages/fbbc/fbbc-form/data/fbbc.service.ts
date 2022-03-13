import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { FbbcModel } from "../models/fbbc.model";

@Injectable({ providedIn: "root" })
export class FbbcService extends CaisCrudService<FbbcModel, string> {
  constructor(injector: Injector) {
    super(FbbcModel, injector, "fbbcs");
  }
}
