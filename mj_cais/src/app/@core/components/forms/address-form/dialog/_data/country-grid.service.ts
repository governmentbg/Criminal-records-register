import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../../services/rest/cais-crud.service";
import { CountryGridModel } from "../_models/country-grid.model";

@Injectable({
  providedIn: "root",
})
export class CountryGridService extends CaisCrudService<
  CountryGridModel,
  string
> {
  constructor(injector: Injector) {
    super(CountryGridModel, injector, "nomenclature-details/countries");
  }
}
