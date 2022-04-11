import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { CaisCrudService } from "../../../@core/services/rest/cais-crud.service";
import { ObjectCountModel } from "../_models/object-count.model";

@Injectable({
  providedIn: "root",
})
export class HomeService extends CaisCrudService<ObjectCountModel, string> {
  constructor(injector: Injector) {
    super(ObjectCountModel, injector, "home");
  }

  public get(): Observable<ObjectCountModel> {
    return this.http.get<ObjectCountModel>(`${this.url}`);
  }
}