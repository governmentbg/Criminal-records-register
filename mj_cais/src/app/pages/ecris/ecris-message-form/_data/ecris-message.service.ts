import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { EcrisMessageModel } from "../_models/ecris-message.model";

@Injectable({ providedIn: "root" })
export class EcrisMessageService extends CaisCrudService<
  EcrisMessageModel,
  string
> {
  constructor(injector: Injector) {
    super(EcrisMessageModel, injector, "ecris-messages");
  }
}
