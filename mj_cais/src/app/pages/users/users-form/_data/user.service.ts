import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { UserModel } from "../_models/user.model";

@Injectable({ providedIn: "root" })
export class UserService extends CaisCrudService<
  UserModel,
  string
> {
  constructor(injector: Injector) {
    super(UserModel, injector, "users");
  }
}
