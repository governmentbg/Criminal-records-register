import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { UserAuthorityService } from "../../../@core/services/common/user-authority.service";
import { StatisticsResolverData } from "../_data/statistics.resolver";
import { StatisticsService } from "../_data/statistics.service";
import { StatisticsSearchForm } from "../_models/statistics-search.form";
import { StatisticsSearchModel } from "../_models/statistics-search.model";

@Component({
  selector: "cais-application-statistics-form",
  templateUrl: "./application-statistics-form.component.html",
  styleUrls: ["./application-statistics-form.component.scss"],
})
export class ApplicationStatisticsFormComponent
  extends CrudForm<
    StatisticsSearchModel,
    StatisticsSearchForm,
    StatisticsResolverData,
    StatisticsService
  >
  implements OnInit
{
  constructor(
    service: StatisticsService,
    public injector: Injector,
    private userAuthService: UserAuthorityService
  ) {
    super(service, injector);
  }

  ngOnInit(): void {
    this.fullForm = new StatisticsSearchForm();
    this.fullForm.group.patchValue(this.dbData.element);
    this.fullForm.authority.patchValue(this.userAuthService.csAuthorityId);
    this.formFinishedLoading.emit();
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: StatisticsSearchModel) {
    return new StatisticsSearchModel(object);
  }
}