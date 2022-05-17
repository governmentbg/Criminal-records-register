import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { forkJoin, Observable, of } from "rxjs";
import { BaseResolverData } from "../../../../@core/models/common/base-resolver.data";
import { AdministrationsExtService } from "../../../administrations-external/administrations-ext-form/_data/administrations-ext.service";
import { AdministrationsExtModel } from "../../../administrations-external/administrations-ext-form/_models/administrations-ext.model";
import { UsersExternalModel } from "../_models/users-external.model";
import { UsersExternalService } from "./users-external.service";


@Injectable({ providedIn: "root" })
export class UsersExternalResolver implements Resolve<any> {
  constructor(
    private administraiontsExtService: AdministrationsExtService,
    private service: UsersExternalService
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {
    let userId = route.params["ID"];
    let isEdit = route.data["edit"];
    let element = isEdit ? this.service.find(userId) : of(null);

    let result: UsersExternalResolverData = {
      element: element,
      administrations: this.administraiontsExtService.getAllNoWrap()
    };
    return forkJoin(result);
  }
}

export class UsersExternalResolverData extends BaseResolverData<UsersExternalModel> {
  public administrations: Observable<AdministrationsExtModel[]>;
}
