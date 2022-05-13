import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { RoleModel } from "../_models/role.model";

@Injectable({ providedIn: "root" })
export class RoleService extends CaisCrudService<
  RoleModel,
  string
> {
  constructor(injector: Injector) {
    super(RoleModel, injector, "roles");
  }
}
