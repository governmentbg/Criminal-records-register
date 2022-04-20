import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../../../../@core/services/rest/cais-crud.service";
import { OffenceCategoryGridModel } from "../_models/offence-category-grid.model";

@Injectable({
  providedIn: "root",
})
export class OffenceCategoryGridService extends CaisCrudService<
  OffenceCategoryGridModel,
  string
> {
  constructor(injector: Injector) {
    super(OffenceCategoryGridModel, injector, "offence-categories");
  }
}