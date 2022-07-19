import { Injectable } from "@angular/core";
import { CrudService } from "@tl/tl-common";
import { Directive, Injector } from "@angular/core";
import { map } from "rxjs/operators";
import { Observable } from "rxjs";
import { HttpClient, HttpParams } from "@angular/common/http";
// import { environment } from 'src/environments/environment';

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
      params = params.append(
        "$orderby",
        `${this.orderByDefaultPropName} ${this.sortOrder}`
      );
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

  getAllNoWrap(aParams?: HttpParams): Observable<T[]> {
    this.isLoading = true;
    //api/bulletins?statusId=Active
    let paramIndex = this.url.indexOf("?");
    let urlResult = `${this.url}/getAll`;

    if (paramIndex > -1) {
      let url = this.url.substring(0, paramIndex);
      let appliedParams = this.url.substring(paramIndex);
      urlResult = `${url}/getAll${appliedParams}`;
    }

    return this.http.get<T[]>(urlResult, { params: aParams }).pipe(
      map((items: T[]) => {
        this.isLoading = false;
        return items.map((item) => {
          const newObj = new this.createT(item);
          return newObj;
        });
      })
    );
  }

  // used only for flat object !!!
  constructQueryParamsByFilters(formObj, filterQuery = ''): string {
    for (let key in formObj) {
      if (key && formObj[key]) {
        let value = formObj[key];

        if (typeof formObj[key] == "object") {
          let date = new Date(formObj[key] );
          value = date.toISOString();
        }

        filterQuery += `&${key}=${value}`;
      }
    }

    return filterQuery;
  }
}
