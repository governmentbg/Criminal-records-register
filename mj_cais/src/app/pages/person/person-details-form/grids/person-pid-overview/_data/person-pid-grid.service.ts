import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { CaisCrudService } from "../../../../../../@core/services/rest/cais-crud.service";
import { PersonPidGridModel } from "../_models/person-pid-grid.model";
import { RemovePidDialogFrom } from "../_models/remove-pid-dialog.form";

const currentEndpoint = "people/pids";

@Injectable({
  providedIn: "root",
})
export class PersonPidGridService extends CaisCrudService<
  PersonPidGridModel,
  string
> {
  constructor(injector: Injector) {
    super(PersonPidGridModel, injector, currentEndpoint);
  }

  public setPersonId(personId: string) {
    this.updateUrl(`${currentEndpoint}?personId=${personId}`);
  }

  removePid(model: RemovePidDialogFrom): Observable<RemovePidDialogFrom> {
    return this.http.post<RemovePidDialogFrom>(`${this.baseUrl}/api/people/remove-pid`, model);
  }
}
