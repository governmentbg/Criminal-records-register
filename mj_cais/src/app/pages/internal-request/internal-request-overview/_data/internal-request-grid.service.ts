import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { InternalRequestGridModel } from "../_models/internal-request-grid.model";

@Injectable({
  providedIn: "root",
})
export class InternalRequestGridService extends CaisCrudService<
  InternalRequestGridModel,
  string
> {
  constructor(injector: Injector) {
    super(InternalRequestGridModel, injector, "internal-requests");
  }
}