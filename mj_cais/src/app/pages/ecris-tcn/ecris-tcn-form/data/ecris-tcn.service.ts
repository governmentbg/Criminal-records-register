import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { EcrisTcnModel } from "../models/ecris-tcn.model";

@Injectable({ providedIn: "root" })
export class EcrisTcnService extends CaisCrudService<EcrisTcnModel, string> {
  constructor(injector: Injector) {
    super(EcrisTcnModel, injector, "ecris-tcns");
  }
}
