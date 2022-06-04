import { HttpParams } from "@angular/common/http";
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

   // override
   public addOrderBy(params?: HttpParams): HttpParams {
    if (!params) {
      params = new HttpParams();
    }

    if (!params.has('$orderby')) {
      params = params.append('$orderby', 'createdOn desc');
    }

    return params;
  }
}
