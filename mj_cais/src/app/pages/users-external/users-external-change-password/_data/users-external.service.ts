import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { UsersExternalService } from "../../users-external-form/_data/users-external.service";
import { UsersExternalModel } from "../../users-external-form/_models/users-external.model";

@Injectable({ providedIn: "root" })
export class UsersExternalPasswordService extends UsersExternalService {
  constructor(injector: Injector) {
    super(injector);
  }

  override update(id: string, t: UsersExternalModel): Observable<UsersExternalModel> {
    return this.http.post<UsersExternalModel>(
      this.url + '/change-password', 
      {userName: t.userName, password: t.password}, 
      {}
    );
  }
}
