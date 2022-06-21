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
import { BulletinEventCountModel } from "../_models/bulletin-event-count.model";
import { EcrisCountModel } from "../_models/ecris-count.model";
import { FbbcCountModel } from "../_models/fbbc-count.model";
import { ForJudgeCountModel } from "../_models/for-judge-count.model";
import { IsinCountModel } from "../_models/isin-count.model";
import { ObjectCountModel } from "../_models/object-count.model";
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
      bulletinEvents: of(null),
      ecris: of(null),
      isin: of(null),
      forJudge: of(null),
      fbbc: of(null),
    };

    this.permissionsService.permissions$.subscribe((perm) => {
      var roles = Object.keys(perm);
      if (
        roles.indexOf(RoleNameEnum.Normal) > -1 ||
        roles.indexOf(RoleNameEnum.Judge) > -1
      ) {
        result.bulletins = this.service.getBulletinsCount();
        result.bulletinEvents = this.service.getBulletinEventsCount();
        result.applications = this.service.getApplicationsCount();
      }

      if (
        roles.indexOf(RoleNameEnum.Normal) > -1 ||
        roles.indexOf(RoleNameEnum.CentralAuth) > -1 ||
        roles.indexOf(RoleNameEnum.Judge) > -1
      ) {
        result.isin = this.service.getIsinCount();
      }

      if (roles.indexOf(RoleNameEnum.CentralAuth) > -1) {
        result.ecris = this.service.getEcrisCount();
        result.fbbc = this.service.getFbbcCount();
      }

      if (roles.indexOf(RoleNameEnum.Judge) > -1) {
        result.forJudge = this.service.getForJudgeCount();
      }
    });

    return forkJoin(result);
  }
}

export class HomeResolverData extends BaseResolverData<ObjectCountModel> {
  public applications: Observable<ApplicationCountModel>;
  public bulletins: Observable<BulletinCountModel>;
  public bulletinEvents: Observable<BulletinEventCountModel>;
  public ecris: Observable<EcrisCountModel>;
  public isin: Observable<IsinCountModel>;
  public forJudge: Observable<ForJudgeCountModel>;
  public fbbc: Observable<FbbcCountModel>;
}
