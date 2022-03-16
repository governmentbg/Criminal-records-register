import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { EcrisIdentificationGridModel } from "../models/ecris-identification-grid.model";

@Injectable({ providedIn: "root" })
export class EcrisIdentificationGridService extends CaisCrudService<
  EcrisIdentificationGridModel,
  string
> {
  constructor(injector: Injector) {
    super(EcrisIdentificationGridModel, injector, "ecris-messages");
  }
}
