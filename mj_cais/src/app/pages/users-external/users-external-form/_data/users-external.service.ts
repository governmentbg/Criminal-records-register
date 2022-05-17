import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { UsersExternalModel } from "../_models/users-external.model";

@Injectable({ providedIn: "root" })
export class UsersExternalService extends CaisCrudService<
  UsersExternalModel,
  string
> {
  constructor(injector: Injector) {
    super(UsersExternalModel, injector, "users-external");
  }
}
