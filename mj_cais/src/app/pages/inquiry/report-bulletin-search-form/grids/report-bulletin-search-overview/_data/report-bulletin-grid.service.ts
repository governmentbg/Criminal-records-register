import { Injectable, Injector } from "@angular/core";
import { map } from "rxjs";
import { CaisCrudService } from "../../../../../../@core/services/rest/cais-crud.service";
import { ExportBulletinModel } from "../_models/export-bulletin.model";

@Injectable({
  providedIn: "root",
})
export class ReportBulletinGridService extends CaisCrudService<
  ExportBulletinModel,
  string
> {
  constructor(injector: Injector) {
    super(ExportBulletinModel, injector, "inquiry");
  }

  public excelExportBulletins(queryParams) {
    debugger;
    let urlResult = `${this.baseUrl}/api/inquiry/export-bulletins?`;
    if (queryParams) {
      urlResult += queryParams;
    }

    return this.http.get<ExportBulletinModel[]>(urlResult).pipe(
      map((items: ExportBulletinModel[]) => {
        this.isLoading = false;
        return items.map((item) => {
          const newObj = new this.createT(item);
          return newObj;
        });
      })
    );
  }
}
