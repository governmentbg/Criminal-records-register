import { Injectable } from "@angular/core";
import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot,
} from "@angular/router";
import { forkJoin, Observable, of } from "rxjs";
import { BaseResolverData } from "../../../../@core/models/common/base-resolver.data";
import { ApplicationSearchModel } from "../_models/application-search.model";
import { ApplicationSearchService } from "./application-search.service";

@Injectable({ providedIn: "root" })
export class ApplicationSearchResolver implements Resolve<any> {
  constructor(private service: ApplicationSearchService) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {
    let id = route.params["ID"];
    let isEdit = route.data["edit"];
    let element = isEdit ? this.service.find(id) : of(null);

    let result: ApplicationSearchResolverData = {
      element: element,
    };
    return forkJoin(result);
  }
}

export class ApplicationSearchResolverData extends BaseResolverData<ApplicationSearchModel> {}
