import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { SearchInquiryGridModel } from "../_models/search-inquiry-grid.model";

@Injectable({ providedIn: "root" })
export class SearchInquiryGridService extends CaisCrudService<
  SearchInquiryGridModel,
  string
> {
  constructor(injector: Injector) {
    super(SearchInquiryGridModel, injector, "inquiry/search-inquiry");
  }
}
