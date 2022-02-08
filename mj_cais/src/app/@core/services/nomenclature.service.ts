import { Injectable, Injector } from "@angular/core";
import { Observable, of } from "rxjs";
import { BaseNomenclatureModel } from "../models/nomenclature/base-nomenclature.model";
import { CaisCrudService } from "./cais-crud.service";

@Injectable({
  providedIn: "root",
})
export class NomenclatureService extends CaisCrudService<
  BaseNomenclatureModel,
  string
> {
  constructor(injector: Injector) {
    super(BaseNomenclatureModel, injector, "nomenclature-details");
  }

  public getCountries(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(`${this.url}/g_countries`);
  }
}
