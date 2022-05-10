import { Injectable } from "@angular/core";
import {
  Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot,
} from "@angular/router";
import { forkJoin, Observable, of } from "rxjs";
import { PersonModel } from "../../../../@core/components/forms/person-form/_models/person.model";
import { BaseResolverData } from "../../../../@core/models/common/base-resolver.data";
import { NomenclatureService } from "../../../../@core/services/rest/nomenclature.service";
import { PersonDetailsService } from "./person-details.service";

@Injectable({
  providedIn: "root",
})
export class PersonDetailsResolver implements Resolve<any> {
  constructor(
    private service: PersonDetailsService,
    private nomenclatureService: NomenclatureService
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {
    let personId = route.params["ID"];
    let isEdit = route.data["edit"];
    let element = isEdit ? this.service.find(personId) : of(null);

    let result: PersonDetailsResolverData = {
      element: element,
    };
    return forkJoin(result);
  }
}

export class PersonDetailsResolverData extends BaseResolverData<PersonModel> {}
