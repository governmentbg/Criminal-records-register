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

  public getCountriesSubdivisions(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/g_country_subdivisions`
    );
  }

  public getCities(): Observable<BaseNomenclatureModel[]> {
    return of([]);
    // todo:
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/g_cities`
    );
  }

  public getBulletinStatuses(): Observable<BaseNomenclatureModel[]> {
    return of([]);
    // todo:
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/b_bulletin_statuses`
    );
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
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/g_deciding_authorities`
    );
  }

  public getOffenceCategories(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/b_offence_categories`
    );
  }

  public getEcrisOffCategories(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/b_ecris_off_categories`
    );
  }

  public getLvlCompletions(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/b_offence_lvl_completions`
    );
  }

  public getExrisOffLevelParts(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/b_ecris_off_lvl_parts`
    );
  }

  public getSanctionCategories(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/b_sanction_categories`
    );
  }

  public getSanctionPobCategories(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/b_sanct_prob_categories`
    );
  }

  public getEcrisSanctionCategories(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/b_ecris_stanct_categs`
    );
  }
  
  public getSanctionProbMeasures(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/b_sanct_prob_measures`
    );
  }

  public getSanctionActivities(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/b_sanction_activities`
    );
  }

  public getDecisionChTypes(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/b_decision_ch_types`
    );
  }

  public getCsAuthorities(): Observable<BaseNomenclatureModel[]> {
    return of([]);
    // todo: 
    // return this.http.get<BaseNomenclatureModel[]>(
    //   `${this.url}/g_cs_authorities`
    // );
  }

  public getDocumentTypes(): Observable<BaseNomenclatureModel[]> {
    return of([]);
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/d_doc_types`
    );
  }

  public getFbbcDocTypes(): Observable<BaseNomenclatureModel[]> {
    return of([]);
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/fbbc_doc_types`
    );
  }

  public getFbbcSanctTypes(): Observable<BaseNomenclatureModel[]> {
    return of([]);
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/fbbc_sanct_types`
    );
  }
}
