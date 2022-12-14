import { Injectable, Injector } from "@angular/core";
import { map, Observable, of } from "rxjs";
import { BulletinTypeConstants } from "../../../pages/bulletin/bulletin-form/_models/bulletin-type-constants";
import { DailyStatisticsConstants } from "../../../pages/statistics/daily-statistics-form/_models/daily-statistics-constants";
import { GenderConstants } from "../../constants/gender.constants";
import { NationalityTypeConstants } from "../../constants/nationality-type.constants";
import { PersonAliasConstants } from "../../constants/person-alias-type.constants";
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

  private cachedCountries: BaseNomenclatureModel[];
  private cachedDistricts: BaseNomenclatureModel[];

  public getCountries(): Observable<BaseNomenclatureModel[]> {
    if (this.cachedCountries) {
      return of(this.cachedCountries);
    }

    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/countries-ordered`
    );
  }

  public saveCountries(countries: BaseNomenclatureModel[]) {
    this.cachedCountries = countries;
  }

  public getDistricts(): Observable<BaseNomenclatureModel[]> {
    if (this.cachedDistricts) {
      return of(this.cachedDistricts);
    }

    return this.http.get<BaseNomenclatureModel[]>(`${this.url}/g_bg_districts`);
  }

  public saveDistricts(districts: BaseNomenclatureModel[]) {
    this.cachedDistricts = districts;
  }

  public getCountriesSubdivisions(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/g_country_subdivisions`
    );
  }

  public getMunicipalities(
    districtId: string
  ): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/municipalities/${districtId}`
    );
  }

  public getCities(
    municipalityId: string
  ): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/cities/${municipalityId}`
    );
  }

  public getBulletinStatuses(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/bulletin-statuses`
    );
  }

  public getBulletinTypes(): Observable<BaseNomenclatureModel[]> {
    return of(BulletinTypeConstants.allData);
  }

  public getDailyStatisticsTypes(): Observable<BaseNomenclatureModel[]> {
    return of(DailyStatisticsConstants.allData);
  }

  public getInternalRequestTypes(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/internal-request-types`
    );
  }

  public getCaseTypes(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(`${this.url}/b_case_types`);
  }

  public getGenderTypes(): Observable<BaseNomenclatureModel[]> {
    return of(GenderConstants.allData);
  }

  public getNationalityTypes(): Observable<BaseNomenclatureModel[]> {
    return of(NationalityTypeConstants.allData);
  }

  public getNationalities(): Observable<BaseNomenclatureModel[]> {
    return of([]);
  }

  public getIdDocumentCategoryTypes(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/b_id_doc_categories`
    );
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

  public getDecidingAuthoritiesForBulletins(): Observable<
    BaseNomenclatureModel[]
  > {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/deciding-authorities`
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

  public getSanctionCategories(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/sanction-categories`
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

  public getFormOfGuilts(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/b_form_of_guilts`
    );
  }

  public getDecisionChTypes(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/b_decision_ch_types`
    );
  }

  public getCsAuthorities(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/g_cs_authorities`
    );
  }

  public getEcrisAuthorities(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/e_ecris_authorities`
    );
  }

  public getEcrisRequestTypes(
    ecrisMsgId: boolean
  ): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/ecris_request_types/${ecrisMsgId}`
    );
  }

  public getDocumentTypes(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(`${this.url}/d_doc_types`);
  }

  public getPersonAliasTypes(): Observable<BaseNomenclatureModel[]> {
    return of(PersonAliasConstants.allData);
  }

  public getFbbcDocTypes(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(`${this.url}/fbbc-doc-types`);
  }

  public getFbbcSanctTypes(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/fbbc-sanct-types`
    );
  }

  public getAllCities(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(`${this.url}/g_cities`);
  }

  public getPurposes(): Observable<BaseNomenclatureModel[]> {
    return this.http
      .get<BaseNomenclatureModel[]>(`${this.url}/a-purposes`)
      .pipe(
        map((items: any[]) => {
          let test = items
            .sort((a, b) => a.name.localeCompare(b.name))
            .map((item) => {
              return item;
            });
          return test;
        })
      );
  }

  public getPaymentMethods(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/a-payment-methods`
    );
  }

  public getSrvcResRcptMethods(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/a-srvc-res-rcpt-methods`
    );
  }

  public getPidTypes(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(`${this.url}/pid-types`);
  }

  // todo: !!! remove this, use grid with pagination
  public getUsers(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(`${this.url}/g-users`).pipe(
      map((items: any[]) => {
        let test = items
          .sort((a, b) => a.name.localeCompare(b.name))
          .map((item) => {
            return item;
          });
        return test;
      })
    );
  }

  public getApplicationStatues(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/application-statuses`
    );
  }
  public getReportStatues(): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.url}/report-statuses`
    );
  }
}
