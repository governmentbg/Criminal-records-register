import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { IsinDataGridModel } from "../_model/isin-data-grid.model";

const currentEndpoint = "isin-data";

@Injectable({
  providedIn: "root",
})
export class IsinDataGridService extends CaisCrudService<
  IsinDataGridModel,
  string
> {
  constructor(injector: Injector) {
    super(IsinDataGridModel, injector, currentEndpoint);
  }

  public updateUrlStatus(status: string) {
    let searchUrl =
      status == null ? currentEndpoint : `${currentEndpoint}?status=${status}`;
    this.updateUrl(searchUrl);
  }
}