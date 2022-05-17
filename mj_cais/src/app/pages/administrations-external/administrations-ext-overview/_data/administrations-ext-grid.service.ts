import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { AdministrationsExtGridModel } from "../_models/administrations-ext-grid.model";

@Injectable({
    providedIn: "root",
  })
  export class AdministrationsExtGridService extends CaisCrudService<
  AdministrationsExtGridModel,
  string
> {
  constructor(injector: Injector) {
    super(AdministrationsExtGridModel, injector, 'ext-administrations');
  }
}