import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../../../../@core/services/rest/cais-crud.service";
import { ResultGridModel } from "../_models/result-grid.model";

@Injectable({
  providedIn: "root",
})
export class ResultGridService extends CaisCrudService<
  ResultGridModel,
  string
> {
  constructor(injector: Injector) {
    super(ResultGridModel, injector, "people");
  }
}
