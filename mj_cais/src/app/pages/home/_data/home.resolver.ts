import { Injectable } from "@angular/core";
import {
  Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot,
} from "@angular/router";
import { NgxPermissionsService } from "ngx-permissions";
import { forkJoin, Observable, of } from "rxjs";
import { RoleNameEnum } from "../../../@core/constants/role-name.enum";
import { BaseResolverData } from "../../../@core/models/common/base-resolver.data";
import { ApplicationCountModel } from "../_models/application-count.model";
import { BulletinCountModel } from "../_models/bulletin-count.model";
import { CentralAuthCountModel } from "../_models/central-auth-count.model";
import { InternalRequestCountModel } from "../_models/internal-request-count.model";
import { HomeService } from "./home.service";

@Injectable({
  providedIn: "root",
})
export class HomeResolver implements Resolve<any> {
  constructor(
    private service: HomeService,
    private permissionsService: NgxPermissionsService
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {
    let element = of(null);

    let result: HomeResolverData = {
      element: element,
      applications: of(null),
      bulletins: of(null),
      centralAuth: of(null),
      internalRequests: of(null),
    };

    this.permissionsService.permissions$.subscribe((perm) => {
      var roles = Object.keys(perm);
      if (
        roles.indexOf(RoleNameEnum.Normal) > -1 ||
        roles.indexOf(RoleNameEnum.Judge) > -1
      ) {
        result.bulletins = this.service.getBulletinsCount();
        result.applications = this.service.getApplicationsCount();
        result.internalRequests = this.service.getInternalRequestCount();
      }

      if (roles.indexOf(RoleNameEnum.CentralAuth) > -1) {
        result.centralAuth = this.service.getCentralAuthCount();
      }
    });

    return forkJoin(result);
  }
}

export class HomeResolverData extends BaseResolverData<any> {
  public applications: Observable<ApplicationCountModel>;
  public bulletins: Observable<BulletinCountModel>;
  public centralAuth: Observable<CentralAuthCountModel>;
  public internalRequests: Observable<InternalRequestCountModel>;
}
