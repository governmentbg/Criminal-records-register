import { Injectable, Injector } from "@angular/core";
import { Observable, of } from "rxjs";
import { GenderConstants } from "../../constants/gender.constants";
import { BaseNomenclatureModel } from "../../models/nomenclature/base-nomenclature.model";
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

  public getCaseTypes(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(`${this.url}/b_case_types`);
  }

  public getGenderTypes(): Observable<BaseNomenclatureModel[]> {
    return of(GenderConstants.allData);
  }

  public getNationalities(): Observable<BaseNomenclatureModel[]> {
    return of([]);
  }

  public getIdDocumentCategoryTypes(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/b_id_doc_categories`
    );
  }

  public getIdDocIssuingAuthorities(): Observable<BaseNomenclatureModel[]> {
    return of([]);
  }

  public getDecisionTypes(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/b_decision_types`
    );
  }

  public getDecidingAuthorities(): Observable<BaseNomenclatureModel[]> {
    return of([]);
  }
}
