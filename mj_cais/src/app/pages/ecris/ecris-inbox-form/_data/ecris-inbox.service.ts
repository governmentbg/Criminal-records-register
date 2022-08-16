import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { EcrisInboxModel } from "../_model/ecris-inbox.model";

@Injectable({
  providedIn: "root",
})
export class EcrisInboxService extends CaisCrudService<
  EcrisInboxModel,
  string
> {
  constructor(injector: Injector) {
    super(EcrisInboxModel, injector, "ecris-inboxes");
  }
}
