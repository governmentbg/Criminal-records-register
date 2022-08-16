import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { ApplicationSearchModel } from "../_models/application-search.model";

@Injectable({ providedIn: "root" })
export class ApplicationSearchService extends CaisCrudService<
  ApplicationSearchModel,
  string
> {
  constructor(injector: Injector) {
    super(ApplicationSearchModel, injector, "applications/search");
  }
}
