import { Injectable, Injector } from "@angular/core";
import { map, Observable } from "rxjs";
import {
  PersonAliasCodeConstants,
  PersonAliasNameConstants,
} from "../../../../@core/constants/person-alias-type.constants";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { PersonModel } from "../../../../@core/components/forms/person-form/_models/person.model";
import { PersonAliasModel } from "../../../../@core/models/common/person-alias.model";

@Injectable({
  providedIn: "root",
})
export class PersonService extends CaisCrudService<PersonModel, string> {
  constructor(injector: Injector) {
    super(PersonModel, injector, "people");
  }

  public getPersonAlias(id: string): Observable<PersonAliasModel[]> {
    return this.http
      .get<PersonAliasModel[]>(`${this.url}/${id}/person-alias`)
      .pipe(
        map((items: PersonAliasModel[]) => {
          return items.map((item) => {
            switch (item.typeCode) {
              case PersonAliasCodeConstants.Nickname:
                item.typeId = 1;
                item.typeName = PersonAliasNameConstants.Nickname;
                break;
              case PersonAliasCodeConstants.Previous:
                item.typeId = 2;
                item.typeName = PersonAliasNameConstants.Previous;
                break;
              case PersonAliasCodeConstants.Maiden:
                item.typeId = 3;
                item.typeName = PersonAliasNameConstants.Maiden;
                break;
            }

            return item;
          });
        })
      );
  }
}
