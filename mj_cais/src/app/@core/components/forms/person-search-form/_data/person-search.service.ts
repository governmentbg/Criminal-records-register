import { Injectable, Injector } from '@angular/core';
import { Observable } from 'rxjs';
import { CaisCrudService } from '../../../../services/rest/cais-crud.service';
import { PersonSearchGridModel } from '../_models/person-search.grid';
import { PersonSearchModel } from '../_models/person-search.model';

@Injectable({
  providedIn: 'root'
})
export class PersonSearchService extends CaisCrudService<
PersonSearchModel,
string
> {
constructor(injector: Injector) {
  super(PersonSearchModel, injector, "people");
}

public loadPersonDataByPid(pid, pidType): Observable<PersonSearchModel> {
  return this.http.get<PersonSearchModel>(
    `${this.url}/person-data-by-pid?pid=${pid}&pidType=${pidType}`
  );
}

public connectPeople(model: any): Observable<any[]> {
  return this.http.post<any>(`${this.baseUrl}/api/people/connect`, model);
}

public searchPerson(query: any): Observable<PersonSearchGridModel[]> {
  return this.http.get<PersonSearchGridModel[]>(
    `${this.url}/search?${query}`
  );
}
}
