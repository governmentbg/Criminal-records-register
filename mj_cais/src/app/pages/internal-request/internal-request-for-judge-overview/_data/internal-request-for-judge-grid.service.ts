import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { InternalRequestForJudgeGridModel } from "../_models/internal-request-for-judge-grid.model";

const currentEndpoint = "internal-requests/for-judge";

@Injectable({
  providedIn: "root",
})
export class InternalRequestForJudgeGridService extends CaisCrudService<
  InternalRequestForJudgeGridModel,
  string
> {
  constructor(injector: Injector) {
    super(InternalRequestForJudgeGridModel, injector, currentEndpoint);
  }

  public updateUrlStatus(statuses: string) {
    this.updateUrl(`${currentEndpoint}?statuses=${statuses}`);
  }
}
