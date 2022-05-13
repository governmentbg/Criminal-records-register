import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { UserExternalGridModel } from "../_models/user-external.grid.model";

@Injectable({
    providedIn: "root",
  })
  export class UserExternalGridService extends CaisCrudService<
  UserExternalGridModel,
  string
> {
  constructor(injector: Injector) {
    super(UserExternalGridModel, injector, 'users-external');
  }
}