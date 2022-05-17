import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { AdministrationsExtModel } from "../_models/administrations-ext.model";

@Injectable({
    providedIn: "root",
  })
  export class AdministrationsExtService extends CaisCrudService<
  AdministrationsExtModel,
  string
> {
  constructor(injector: Injector) {
    super(AdministrationsExtModel, injector, 'ext-administrations');
  }
}