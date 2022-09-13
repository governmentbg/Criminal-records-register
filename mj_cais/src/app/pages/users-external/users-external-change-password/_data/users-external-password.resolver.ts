import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { forkJoin, Observable, of } from "rxjs";
import { BaseResolverData } from "../../../../@core/models/common/base-resolver.data";
import { UsersExternalService } from "../../users-external-form/_data/users-external.service";
import { UsersExternalModel } from "../../users-external-form/_models/users-external.model";


@Injectable({ providedIn: "root" })
export class UsersExternalChangePasswordResolver implements Resolve<any> {
  constructor(
    private service: UsersExternalService
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {
    let userId = route.params["ID"];
    let element = this.service.find(userId);

    let result: UsersExternalChangePasswordData = {
      element: element
    };
    return forkJoin(result);
  }
}

export class UsersExternalChangePasswordData extends BaseResolverData<UsersExternalModel> {
}
