import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../../../../environments/environment";
import { BaseNomenclatureModel } from "../../../../@core/models/nomenclature/base-nomenclature.model";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { ApplicationModel } from "../models/application.model";

@Injectable({ providedIn: "root" })
export class ApplicationService extends CaisCrudService<
  ApplicationModel,
  string
> {
  constructor(injector: Injector) {
    super(ApplicationModel, injector, "applications");
  }
}
