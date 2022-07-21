import { Injectable } from "@angular/core";
import {
  Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot,
} from "@angular/router";
import { forkJoin, Observable, of } from "rxjs";
import { BaseResolverData } from "../../../../@core/models/common/base-resolver.data";
import { PersonAliasModel } from "../../../../@core/models/common/person-alias.model";
import { BaseNomenclatureModel } from "../../../../@core/models/nomenclature/base-nomenclature.model";
import { NomenclatureService } from "../../../../@core/services/rest/nomenclature.service";
import { BulletinModel } from "../_models/bulletin.model";
import { BulletinService } from "./bulletin.service";

@Injectable({
  providedIn: "root",
})
export class BulletinResolver implements Resolve<any> {
  constructor(
    private nomenclatureService: NomenclatureService,
    private service: BulletinService
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {
    let bulletineId = route.params["ID"];
    let isEdit = route.data["edit"];
    let personId = route.queryParams["personId"];
    let element: any = of(null);

    if (isEdit) {
      element = this.service.find(bulletineId);
    } else if (personId) {
      element = this.service.getWithPersonData(personId);
    } else {
      element = of(null);
    }

    let result: BulletinResolverData = {
      element: element,
      genderTypes: this.nomenclatureService.getGenderTypes(),
      nationalities: this.nomenclatureService.getNationalities(),
      idDocumentCategoryTypes:
        this.nomenclatureService.getIdDocumentCategoryTypes(),
      decisionTypes: this.nomenclatureService.getDecisionTypes(),
      decidingAuthorities: this.nomenclatureService.getDecidingAuthoritiesForBulletins(),
      caseTypes: this.nomenclatureService.getCaseTypes(),
      ecrisOffCategories: this.nomenclatureService.getEcrisOffCategories(),
      countries: this.nomenclatureService.getCountries(),
      sanctionCategories: this.nomenclatureService.getSanctionCategories(),
      sanctionProbCategories:
        this.nomenclatureService.getSanctionPobCategories(),
      ecrisSanctionCategories:
        this.nomenclatureService.getEcrisSanctionCategories(),
      sanctionProbMeasures: this.nomenclatureService.getSanctionProbMeasures(),
      decisionChTypes: this.nomenclatureService.getDecisionChTypes(),
      documentTypes: this.nomenclatureService.getDocumentTypes(),
      bulletinStatuses: this.nomenclatureService.getBulletinStatuses(),
      personAlias: this.service.getPersonAlias(bulletineId),
      personAliasTypes: this.nomenclatureService.getPersonAliasTypes(),
      bulletinTypes: this.service.getBulletinTypes(),
      formOfGuilts: this.nomenclatureService.getFormOfGuilts(),
    };
    return forkJoin(result);
  }
}

export class BulletinResolverData extends BaseResolverData<BulletinModel> {
  public personAlias: Observable<PersonAliasModel[]>;

  public genderTypes: Observable<BaseNomenclatureModel[]>;
  public nationalities: Observable<BaseNomenclatureModel[]>;
  public idDocumentCategoryTypes: Observable<BaseNomenclatureModel[]>;
  public decisionTypes: Observable<BaseNomenclatureModel[]>;
  public decidingAuthorities: Observable<BaseNomenclatureModel[]>;
  public caseTypes: Observable<BaseNomenclatureModel[]>;
  public ecrisOffCategories: Observable<BaseNomenclatureModel[]>;
  public countries: Observable<BaseNomenclatureModel[]>;
  public sanctionCategories: Observable<BaseNomenclatureModel[]>;
  public sanctionProbCategories: Observable<BaseNomenclatureModel[]>;
  public ecrisSanctionCategories: Observable<BaseNomenclatureModel[]>;
  public sanctionProbMeasures: Observable<BaseNomenclatureModel[]>;
  public decisionChTypes: Observable<BaseNomenclatureModel[]>;
  public documentTypes: Observable<BaseNomenclatureModel[]>;
  public bulletinStatuses: Observable<BaseNomenclatureModel[]>;
  public personAliasTypes: Observable<BaseNomenclatureModel[]>;
  public bulletinTypes: Observable<BaseNomenclatureModel[]>;
  public formOfGuilts: Observable<BaseNomenclatureModel[]>;
}
