import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../services/rest/cais-crud.service";
import { SelecrPidGridModel } from "../_models/select-pid-grid.model";

@Injectable({
  providedIn: "root",
})
export class SelectPidGridService extends CaisCrudService<
  SelecrPidGridModel,
  string
> {
  constructor(injector: Injector) {
    super(SelecrPidGridModel, injector, "people/get-pids-for-selection-dialog");
  }
}
