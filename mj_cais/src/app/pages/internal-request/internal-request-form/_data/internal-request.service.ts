import { Injectable, Injector } from "@angular/core";
import { Observable, of } from "rxjs";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { InternalRequestModel } from "../_models/internal-request.model";
import { PersonBulletinsGridModel } from "../_models/person-bulletin-grid-model";

@Injectable({
  providedIn: "root",
})
export class InternalRequestService extends CaisCrudService<
  InternalRequestModel,
  string
> {
  constructor(injector: Injector) {
    super(InternalRequestModel, injector, "internal-requests");
  }

  public changeStatus(aId: string, statusId: string): Observable<any> {
    return this.http.put(`${this.url}/${aId}/change-status/${statusId}`, {});
  }

  public replay(aId, aInDto: any): Observable<any> {
    return this.http.put(`${this.url}/${aId}/replay`, aInDto);
  }

  public markAsRead(ids: string[]): Observable<any> {
    return this.http.put(`${this.url}/mark-as-read`, ids);
  }

  public getRequestsCount() {
    return this.http.get<any>(`${this.url}/requests-count`);
  }

  public getBulletinForPerson(
    id: string
  ): Observable<PersonBulletinsGridModel[]> {
    return this.http.get<PersonBulletinsGridModel[]>(
      `${this.url}/person-bulletins/${id}`
    );
  }

  public getSelectedBulltins(id: string){
    if(id){
      return this.http.get<PersonBulletinsGridModel[]>(
        `${this.url}/selected-bulletins/${id}/`
      );
    }

    return of([]);
  }

  public getBulletinWithPidData(bulletinId){
    if(bulletinId){
      return this.http.get<PersonBulletinsGridModel>(
        `${this.url}/bulletin-with-pid/${bulletinId}/`
      );
    }

    return of(null);
  }
}
