import { Injectable } from "@angular/core";
import {
  Router,
  Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot,
} from "@angular/router";
import { forkJoin, Observable, of } from "rxjs";
import { BaseResolverData } from "../../../../@core/models/common/base-resolver.data";
import { BaseNomenclatureModel } from "../../../../@core/models/nomenclature/base-nomenclature.model";
import { NomenclatureService } from "../../../../@core/services/rest/nomenclature.service";
import { PersonModel } from "../../../../@core/components/forms/person-form/_models/person.model";
import { PersonService } from "./person.service";

@Injectable({
  providedIn: "root",
})
export class PersonResolver implements Resolve<any> {
  constructor(
    private nomenclatureService: NomenclatureService,
    private service: PersonService
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {
    debugger;
    let personId = route.params["ID"];
    let isEdit = route.data["edit"];
    let element = isEdit ? this.service.find(personId) : of(null);

    let result: PersonResolverData = {
      element: element,
      authorities: this.nomenclatureService.getCsAuthorities(),
      genderTypes: this.nomenclatureService.getGenderTypes(),
      countries: this.nomenclatureService.getCountries(),
      idDocumentCategoryTypes:
        this.nomenclatureService.getIdDocumentCategoryTypes(), // todo:
      personAliasTypes: this.nomenclatureService.getPersonAliasTypes(),
    };
    return forkJoin(result);
  }
}

export class PersonResolverData extends BaseResolverData<PersonModel> {
  public authorities: Observable<BaseNomenclatureModel[]>;
  public genderTypes: Observable<BaseNomenclatureModel[]>;
  public countries: Observable<BaseNomenclatureModel[]>;
  public idDocumentCategoryTypes: Observable<BaseNomenclatureModel[]>;
  public personAliasTypes: Observable<BaseNomenclatureModel[]>;
}
