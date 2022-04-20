import { Injectable } from "@angular/core";
import {
  Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot,
} from "@angular/router";
import { forkJoin, Observable, of } from "rxjs";
import { BulletinPersonAliasModel } from "../../../../@core/components/shared/bulletin-person-info/_models/bulletin-person-alias.model";
import { BaseResolverData } from "../../../../@core/models/common/base-resolver.data";
import { BaseNomenclatureModel } from "../../../../@core/models/nomenclature/base-nomenclature.model";
import { NomenclatureService } from "../../../../@core/services/rest/nomenclature.service";
import { BulletinDecisionModel } from "../tabs/bulletin-decision-form/_models/bulletin-decision.model";
import { BulletinDocumentModel } from "../tabs/bulletin-documents-form/_models/bulletin-document.model";
import { BulletinOffenceModel } from "../tabs/bulletin-offences-form/_models/bulletin-offence.model";
import { BulletinSanctionModel } from "../tabs/bulletin-sanctions-form/_models/bulletin-sanction.model";
import { BulletinStatusHistoryModel } from "../tabs/bulletin-status-history-overview/_models/bulletin-status-history.model";
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
    let element = isEdit ? this.service.find(bulletineId) : of(null);

    let result: BulletinResolverData = {
      element: element,
      genderTypes: this.nomenclatureService.getGenderTypes(),
      nationalities: this.nomenclatureService.getNationalities(),
      idDocumentCategoryTypes:
        this.nomenclatureService.getIdDocumentCategoryTypes(),
      decisionTypes: this.nomenclatureService.getDecisionTypes(),
      decidingAuthorities: this.nomenclatureService.getDecidingAuthorities(),
      caseTypes: this.nomenclatureService.getCaseTypes(),
      offences: this.service.getOffences(bulletineId),
      ecrisOffCategories: this.nomenclatureService.getEcrisOffCategories(),
      countries: this.nomenclatureService.getCountries(),
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
      documents:  this.service.getDocuments(bulletineId),
      documentTypes:  this.nomenclatureService.getDocumentTypes(),
      bulletinStatuses: this.nomenclatureService.getBulletinStatuses(),
      personAlias: this.service.getPersonAlias(bulletineId),
      personAliasTypes: this.nomenclatureService.getPersonAliasTypes(),
      bulletinTypes: this.service.getBulletinTypes(),
      bulletinStatusHistoryData: this.service.getBulletinStatusHistoryData(bulletineId),
      formOfGuilts: this.nomenclatureService.getFormOfGuilts()
    };
    return forkJoin(result);
  }
}

export class BulletinResolverData extends BaseResolverData<BulletinModel> {
  public sanctions: Observable<BulletinSanctionModel[]>;
  public offences: Observable<BulletinOffenceModel[]>;
  public decisions: Observable<BulletinDecisionModel[]>;
  public documents: Observable<BulletinDocumentModel[]>;
  public personAlias: Observable<BulletinPersonAliasModel[]>;
  public bulletinStatusHistoryData: Observable<BulletinStatusHistoryModel[]>;

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
  public sanctionActivities: Observable<BaseNomenclatureModel[]>;
  public decisionChTypes: Observable<BaseNomenclatureModel[]>;
  public documentTypes: Observable<BaseNomenclatureModel[]>;
  public bulletinStatuses : Observable<BaseNomenclatureModel[]>;
  public personAliasTypes: Observable<BaseNomenclatureModel[]>;
  public bulletinTypes : Observable<BaseNomenclatureModel[]>;
  public formOfGuilts :  Observable<BaseNomenclatureModel[]>;
}
