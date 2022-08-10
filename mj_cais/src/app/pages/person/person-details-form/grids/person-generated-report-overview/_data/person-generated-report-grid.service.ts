import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../../../@core/services/rest/cais-crud.service";
import { PersonGeneratedReportGridModel } from "../_models/person-generated-report-grid.model";

const currentEndpoint = "people/report-applications";

@Injectable({
  providedIn: "root",
})
export class PersonGeneratedReportGridService extends CaisCrudService<
  PersonGeneratedReportGridModel,
  string
> {
  constructor(injector: Injector) {
    super(PersonGeneratedReportGridModel, injector, currentEndpoint);
  }

  public setPersonId(personId: string) {
    this.updateUrl(`${currentEndpoint}?personId=${personId}`);
  }
}
