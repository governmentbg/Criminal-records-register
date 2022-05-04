import { Injectable, Injector } from "@angular/core";
import { map, Observable } from "rxjs";
import {
  PersonAliasCodeConstants,
  PersonAliasNameConstants,
} from "../../../../constants/person-alias-type.constants";
import { CaisCrudService } from "../../../../services/rest/cais-crud.service";
import { BulletinPersonAliasModel } from "../../../shared/bulletin-person-info/_models/bulletin-person-alias.model";
import { PersonModel } from "../_models/person.model";

@Injectable({
  providedIn: "root",
})
export class PersonService extends CaisCrudService<PersonModel, string> {
  constructor(injector: Injector) {
    super(PersonModel, injector, "people");
  }

  public getPersonAlias(id: string): Observable<BulletinPersonAliasModel[]> {
    return this.http
      .get<BulletinPersonAliasModel[]>(`${this.url}/${id}/person-alias`)
      .pipe(
        map((items: BulletinPersonAliasModel[]) => {
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
