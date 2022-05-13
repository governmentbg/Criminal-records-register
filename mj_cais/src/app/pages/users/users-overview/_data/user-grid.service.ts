import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { UserGridModel } from "../_models/user.grid.model";

@Injectable({
    providedIn: "root",
  })
  export class UserGridService extends CaisCrudService<
  UserGridModel,
  string
> {
  constructor(injector: Injector) {
    super(UserGridModel, injector, 'users');
  }
}