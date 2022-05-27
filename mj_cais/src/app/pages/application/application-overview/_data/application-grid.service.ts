import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { ApplicationGridModel } from "../_models/application-overview/application-grid.model";

const currentEndpoint = "applications";

@Injectable({ providedIn: "root" })
export class ApplicationGridService extends CaisCrudService<
  ApplicationGridModel,
  string
> {
  constructor(injector: Injector) {
    super(ApplicationGridModel, injector, currentEndpoint);
  }

  public updateUrlStatus(statusId?: string) {
    if(statusId){
      this.updateUrl(`${currentEndpoint}?statusId=${statusId}`);
    }else{
      this.updateUrl(`${currentEndpoint}`);
    }
  }

  
}
