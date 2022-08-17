import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { SearchInquiryModel } from "../_models/search-inquiry.model";

@Injectable({ providedIn: "root" })
export class SearchInquiryService extends CaisCrudService<
  SearchInquiryModel,
  string
> {
  constructor(injector: Injector) {
    super(SearchInquiryModel, injector, "inquiry/search-inquiry");
  }
}
