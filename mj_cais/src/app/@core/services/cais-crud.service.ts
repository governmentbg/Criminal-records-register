import { Injectable } from "@angular/core";
import { CrudService } from "@tl/tl-common";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root",
})
export abstract class CaisCrudService<T, ID> extends CrudService<T, ID> {
  protected url: string;

  public updateUrl(endpoint: string) {
    this.endpoint = endpoint;
    this.url = this.baseUrl + "/api/" + this.endpoint;
  }

  public deleteDocument(aId: number, uId: string): Observable<any> {
    return this.http.delete(
      this.baseUrl + `/api/${this.endpoint}/${aId}/documents/${uId}`
    );
  }
}
