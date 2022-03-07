import { Injectable } from "@angular/core";
import {
  Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot,
} from "@angular/router";
import { forkJoin, Observable, of } from "rxjs";
import { BaseResolverData } from "../../../../@core/models/common/base-resolver.data";
import { BaseNomenclatureModel } from "../../../../@core/models/nomenclature/base-nomenclature.model";
import { NomenclatureService } from "../../../../@core/services/rest/nomenclature.service";
import { BulletinDecisionModel } from "../models/bulletin-decision.model";
import { BulletinOffenceModel } from "../models/bulletin-offence.model";
import { BulletinSanctionModel } from "../models/bulletin-sanction.model";
import { BulletinModel } from "../models/bulletin.model";
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
    let element = isEdit ? this.service.find(bulletineId) : of(null);

    let result: BulletinResolverData = {
      element: element,
      genderTypes: this.nomenclatureService.getGenderTypes(),
      nationalities: this.nomenclatureService.getNationalities(),
      idDocumentCategoryTypes:
        this.nomenclatureService.getIdDocumentCategoryTypes(),
      idDocIssuingAuthorities:
        this.nomenclatureService.getIdDocIssuingAuthorities(),
      decisionTypes: this.nomenclatureService.getDecisionTypes(),
      decidingAuthorities: this.nomenclatureService.getDecidingAuthorities(),
      caseTypes: this.nomenclatureService.getCaseTypes(),
      offences: this.service.getOffences(bulletineId),
      offencesCategories: this.nomenclatureService.getOffenceCategories(),
      ecrisOffCategories: this.nomenclatureService.getEcrisOffCategories(),
      countries: this.nomenclatureService.getCountries(),
      countriesSubdivisions:
        this.nomenclatureService.getCountriesSubdivisions(),
      cities: this.nomenclatureService.getCities(),
      completions: this.nomenclatureService.getLvlCompletions(),
      parts: this.nomenclatureService.getExrisOffLevelParts(),
      sanctions: this.service.getSanctions(bulletineId),
      sanctionCategories: this.nomenclatureService.getSanctionCategories(),
      sanctionProbCategories:
        this.nomenclatureService.getSanctionPobCategories(),
      ecrisSanctionCategories:
        this.nomenclatureService.getEcrisSanctionCategories(),
      sanctionProbMeasures: this.nomenclatureService.getSanctionProbMeasures(),
      sanctionActivities: this.nomenclatureService.getSanctionActivities(),
      decisionChTypes: this.nomenclatureService.getDecisionChTypes(),
      decisions: this.service.getDecisions(bulletineId),
    };
    return forkJoin(result);
  }
}

export class BulletinResolverData extends BaseResolverData<BulletinModel> {
  public sanctions: Observable<BulletinSanctionModel[]>;
  public offences: Observable<BulletinOffenceModel[]>;
  public decisions: Observable<BulletinDecisionModel[]>;

  public genderTypes: Observable<BaseNomenclatureModel[]>;
  public nationalities: Observable<BaseNomenclatureModel[]>;
  public idDocumentCategoryTypes: Observable<BaseNomenclatureModel[]>;
  public idDocIssuingAuthorities: Observable<BaseNomenclatureModel[]>;
  public decisionTypes: Observable<BaseNomenclatureModel[]>;
  public decidingAuthorities: Observable<BaseNomenclatureModel[]>;
  public caseTypes: Observable<BaseNomenclatureModel[]>;
  public offencesCategories: Observable<BaseNomenclatureModel[]>;
  public ecrisOffCategories: Observable<BaseNomenclatureModel[]>;
  public countries: Observable<BaseNomenclatureModel[]>;
  public countriesSubdivisions: Observable<BaseNomenclatureModel[]>;
  public cities: Observable<BaseNomenclatureModel[]>;
  public completions: Observable<BaseNomenclatureModel[]>;
  public parts: Observable<BaseNomenclatureModel[]>;
  public sanctionCategories: Observable<BaseNomenclatureModel[]>;
  public sanctionProbCategories: Observable<BaseNomenclatureModel[]>;
  public ecrisSanctionCategories: Observable<BaseNomenclatureModel[]>;
  public sanctionProbMeasures: Observable<BaseNomenclatureModel[]>;
  public sanctionActivities: Observable<BaseNomenclatureModel[]>;
  public decisionChTypes: Observable<BaseNomenclatureModel[]>;
}
