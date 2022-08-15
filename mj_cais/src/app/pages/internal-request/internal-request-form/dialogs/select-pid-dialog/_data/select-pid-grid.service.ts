import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../../../@core/services/rest/cais-crud.service";
import { SelecrPidGridModel } from "../_models/select-pid-grid.model";

@Injectable({
  providedIn: "root",
})
export class SelectPidGridService extends CaisCrudService<
  SelecrPidGridModel,
  string
> {
  constructor(injector: Injector) {
    super(SelecrPidGridModel, injector, "internal-requests/get-pids-for-selection-dialog");
  }
}
