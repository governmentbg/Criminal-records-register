import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { forkJoin, Observable, of } from "rxjs";
import { BaseResolverData } from "../../../../@core/models/common/base-resolver.data";
import { BaseNomenclatureModel } from "../../../../@core/models/nomenclature/base-nomenclature.model";
import { NomenclatureService } from "../../../../@core/services/rest/nomenclature.service";
import { RoleModel } from "../_models/role.model";
import { UserModel } from "../_models/user.model";
import { RoleService } from "./roles.service";
import { UserService } from "./user.service";



@Injectable({ providedIn: "root" })
export class UserResolver implements Resolve<any> {
  constructor(
    private nomenclatureService: NomenclatureService,
    private rolesService: RoleService,
    private service: UserService
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {
    let userId = route.params["ID"];
    let isEdit = route.data["edit"];
    let element = isEdit ? this.service.find(userId) : of(null);

    let result: UserResolverData = {
      element: element,
      csAuthorities: this.nomenclatureService.getCsAuthorities(),
      roles:  this.rolesService.getAllNoWrap() 
    };
    return forkJoin(result);
  }
}

export class UserResolverData extends BaseResolverData<UserModel> {
  public csAuthorities: Observable<BaseNomenclatureModel[]>;
  public roles: Observable<RoleModel[]>;
}
