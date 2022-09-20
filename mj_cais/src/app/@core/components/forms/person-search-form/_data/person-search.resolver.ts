// import { Injectable } from "@angular/core";
// import {
//   Router,
//   Resolve,
//   RouterStateSnapshot,
//   ActivatedRouteSnapshot,
// } from "@angular/router";
// import { forkJoin, Observable, of } from "rxjs";
// import { BaseResolverData } from "../../../../models/common/base-resolver.data";
// import { BaseNomenclatureModel } from "../../../../models/nomenclature/base-nomenclature.model";
// import { NomenclatureService } from "../../../../services/rest/nomenclature.service";
// import { PersonSearchModel } from "../_models/person-search.model";
// import { PersonSearchService } from "./person-search.service";

// @Injectable({
//   providedIn: "root",
// })
// export class PersonSearchResolver implements Resolve<any> {
//   constructor(
//     private service: PersonSearchService,
//     private nomenclatureService: NomenclatureService
//   ) {}

//   resolve(
//     route: ActivatedRouteSnapshot,
//     state: RouterStateSnapshot
//   ): Observable<any> {
//     let personId = route.params["ID"];
//     let isEdit = route.data["edit"];
//     let element = isEdit ? this.service.find(personId) : of(null);

//     let result: PersonSearchResolverData = {
//       element: element,
//       genderTypes: this.nomenclatureService.getGenderTypes(),
//       pidTypes: this.nomenclatureService.getPidTypes(),
//     };
//     return forkJoin(result);
//   }
// }

// export class PersonSearchResolverData extends BaseResolverData<PersonSearchModel> {
//   public genderTypes: Observable<BaseNomenclatureModel[]>;
//   public pidTypes: Observable<BaseNomenclatureModel[]>;
// }
