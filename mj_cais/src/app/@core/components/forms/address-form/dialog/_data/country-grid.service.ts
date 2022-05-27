import { HttpParams } from "@angular/common/http";
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

  // override
  public addOrderBy(params?: HttpParams): HttpParams {
    if (!params) {
      params = new HttpParams();
    }

    if (!params.has("$orderby")) {
      params = params.append("$orderby", "iso31662Code asc");
    }

    return params;
  }
}
