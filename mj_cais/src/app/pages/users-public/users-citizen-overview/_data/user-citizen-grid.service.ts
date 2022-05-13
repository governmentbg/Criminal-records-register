import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { UserCitizenGridModel } from "../_models/user-citizen.grid.model";

@Injectable({
    providedIn: "root",
  })
  export class UserCitizenGridService extends CaisCrudService<
  UserCitizenGridModel,
  string
> {
  constructor(injector: Injector) {
    super(UserCitizenGridModel, injector, 'users-citizen');
  }
}