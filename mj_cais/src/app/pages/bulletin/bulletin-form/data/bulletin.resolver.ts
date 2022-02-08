import { Injectable } from "@angular/core";
import {
  Router,
  Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot,
} from "@angular/router";
import { forkJoin, Observable, of } from "rxjs";
import { NomenclatureService } from "../../../../@core/services/nomenclature.service";

@Injectable({
  providedIn: "root",
})
export class BulletinResolver implements Resolve<any> {
  constructor(private nomenclatureService: NomenclatureService) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {
    return forkJoin({
      countries: this.nomenclatureService.getCountries(),
      countries2: this.nomenclatureService.getCountries(),
    });
  }
}
