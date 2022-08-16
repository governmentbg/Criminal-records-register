import { Injectable } from "@angular/core";
import { Resolve, ActivatedRouteSnapshot } from "@angular/router";
import { forkJoin, Observable, of } from "rxjs";
import { BaseResolverData } from "../../../../@core/models/common/base-resolver.data";
import { EcrisInboxModel } from "../_models/ecris-inbox.model";
import { EcrisInboxService } from "./ecris-inbox.service";

@Injectable({
  providedIn: "root",
})
export class EcrisInboxResolver implements Resolve<any> {
  constructor(private service: EcrisInboxService) {}

  resolve(route: ActivatedRouteSnapshot): Observable<any> {
    let id = route.params["ID"];
    let isEdit = route.data["edit"];
    let element: any = of(null);

    if (isEdit) {
      element = this.service.find(id);
    } else {
      element = of(null);
    }

    let result: EcrisInboxResolverData = {
      element: element,
    };
    return forkJoin(result);
  }
}

export class EcrisInboxResolverData extends BaseResolverData<EcrisInboxModel> {}
