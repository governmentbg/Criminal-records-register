import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { ExportBulletinModel } from "../../_models/export-bulletin.model";

@Injectable({
  providedIn: "root",
})
export class ReportPersonService extends CaisCrudService<
  ExportBulletinModel,
  string
> {
  constructor(injector: Injector) {
    super(ExportBulletinModel, injector, "inquiry");
  }
}
