import { Injectable } from "@angular/core";
import { Resolve, ActivatedRouteSnapshot } from "@angular/router";
import { forkJoin, Observable, of } from "rxjs";
import { BaseResolverData } from "../../../../@core/models/common/base-resolver.data";
import { EcrisOutboxModel } from "../_models/ecris.outbox.model";
import { EcrisOutboxService } from "./ecris-outbox.service";

@Injectable({
  providedIn: "root",
})
export class EcrisOutboxResolver implements Resolve<any> {
  constructor(private service: EcrisOutboxService) {}

  resolve(route: ActivatedRouteSnapshot): Observable<any> {
    let id = route.params["ID"];
    let isEdit = route.data["edit"];
    let element: any = of(null);

    if (isEdit) {
      element = this.service.find(id);
    } else {
      element = of(null);
    }

    let result: EcrisOutboxResolverData = {
      element: element,
    };
    return forkJoin(result);
  }
}

export class EcrisOutboxResolverData extends BaseResolverData<EcrisOutboxModel> {}
