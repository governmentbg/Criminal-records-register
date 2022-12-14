import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { GeneratedReportModel } from "../tabs/generated-report-overview/_models/generated-report-grid.model";
import { ReportAppStatusHistoryModel } from "../tabs/report-app-history-overview/_models/report-app-status-history.model";
import { ReportApplicationModel } from "../_models/report-application.model";

@Injectable({
  providedIn: "root",
})
export class ReportApplicationService extends CaisCrudService<
  ReportApplicationModel,
  string
> {
  constructor(injector: Injector) {
    super(ReportApplicationModel, injector, "a-report-applications");
  }

  public getWithPersonData(
    personId: string
  ): Observable<ReportApplicationModel> {
    return this.http.get<ReportApplicationModel>(
      `${this.url}/create?personId=${personId}`
    );
  }

  public cancel(id: string, description: any): Observable<any[]> {
    return this.http.put<any>(`${this.url}/cancel/${id}`, description);
  }

  public cancelReport(model: any): Observable<any[]> {
    return this.http.put<any>(`${this.url}/cancel-report/`, model);
  }

  public updateFinal(
    id: string,
    model: ReportApplicationModel
  ): Observable<ReportApplicationModel> {
    return this.http.put<ReportApplicationModel>(
      this.url + `/final-edit/${id}`,
      model,
      {}
    );
  }

  public getStatusHistoryData(
    id: string
  ): Observable<ReportAppStatusHistoryModel[]> {
    return this.http.get<ReportAppStatusHistoryModel[]>(
      `${this.url}/${id}/status-history`
    );
  }

  public getReportsData(id: string): Observable<GeneratedReportModel[]> {
    return this.http.get<GeneratedReportModel[]>(`${this.url}/${id}/reports`);
  }

  public printReport(reportId: string) {
    let url = `${this.url}/print-report/` + reportId;
    return this.http.get(url, { responseType: "blob", observe: "response" });
  }

  public generateReport(model: any): Observable<any[]> {
    return this.http.put<any>(`${this.url}/generate-report/`, model);
  }

  public deliver(id: string): Observable<any> {
    return this.http.put(`${this.url}/deliver/${id}`, {});
  }
}
