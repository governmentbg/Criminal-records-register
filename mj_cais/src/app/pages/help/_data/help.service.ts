import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../@core/services/rest/cais-crud.service";

@Injectable({
  providedIn: "root",
})
export class HelpService extends CaisCrudService<any, string> {
  constructor(injector: Injector) {
    super(null, injector, "help");
  }

  public downloadContentCbs() {
    let url = `${this.url}/cbs`;
    return this.http.get(url, { responseType: "blob", observe: "response" });
  }

  public downloadContentEmployee() {
    let url = `${this.url}/employee`;
    return this.http.get(url, { responseType: "blob", observe: "response" });
  }

  public downloadContentAdministration() {
    let url = `${this.url}/administration`;
    return this.http.get(url, { responseType: "blob", observe: "response" });
  }
}
