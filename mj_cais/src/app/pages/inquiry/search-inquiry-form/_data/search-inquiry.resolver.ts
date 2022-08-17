import { Injectable } from "@angular/core";
import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot,
} from "@angular/router";
import { forkJoin, Observable, of } from "rxjs";
import { BaseResolverData } from "../../../../@core/models/common/base-resolver.data";
import { SearchInquiryModel } from "../_models/search-inquiry.model";
import { SearchInquiryService } from "./search-inquiry.service";

@Injectable({ providedIn: "root" })
export class SearchInquiryResolver implements Resolve<any> {
  constructor(private service: SearchInquiryService) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {
    let id = route.params["ID"];
    let isEdit = route.data["edit"];
    let element = isEdit ? this.service.find(id) : of(null);

    let result: SearchInquiryResolverData = {
      element: element,
    };
    return forkJoin(result);
  }
}

export class SearchInquiryResolverData extends BaseResolverData<SearchInquiryModel> {}
