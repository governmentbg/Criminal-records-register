import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { EcrisOutboxModel } from "../_models/ecris.outbox.model";

@Injectable({
  providedIn: "root",
})
export class EcrisOutboxService extends CaisCrudService<
  EcrisOutboxModel,
  string
> {
  constructor(injector: Injector) {
    super(EcrisOutboxModel, injector, "ecris-outboxes");
  }
}
