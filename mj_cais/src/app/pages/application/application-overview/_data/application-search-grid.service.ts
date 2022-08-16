import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { ApplicationSearchGridModel } from "../_models/application-search-overview/application-search-grid.model";

@Injectable({ providedIn: "root" })
export class ApplicationSearchGridService extends CaisCrudService<
  ApplicationSearchGridModel,
  string
> {
  constructor(injector: Injector) {
    super(ApplicationSearchGridModel, injector, "applications/search");	
  }
}