import { HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CrudService } from "@tl/tl-common";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root",
})
export abstract class CaisCrudService<T, ID> extends CrudService<T, ID> {
  protected orderByDefaultPropName: string = "createdOn";
  protected sortOrder: string = "desc";
  protected url: string;

  // override
  public addOrderBy(params?: HttpParams): HttpParams {
    if (!params) {
      params = new HttpParams();
    }

    if (!params.has("$orderby")) {
      params = params.append("$orderby", `${this.orderByDefaultPropName} ${this.sortOrder}`);
    }

    return params;
  }

  public updateUrl(endpoint: string) {
    this.endpoint = endpoint;
    this.url = this.baseUrl + "/api/" + this.endpoint;
  }

  public deleteDocument(aId: number, documentId: string): Observable<any> {
    return this.http.delete(
      this.baseUrl + `/api/${this.endpoint}/${aId}/documents/${documentId}`
    );
  }
}
