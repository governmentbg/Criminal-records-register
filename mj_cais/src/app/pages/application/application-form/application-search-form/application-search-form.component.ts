import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { CrudForm } from "../../../../@core/directives/crud-form.directive";
import { DateFormatService } from "../../../../@core/services/common/date-format.service";
import { ApplicationSearchResolverData } from "../_data/application-search.resolver";
import { ApplicationSearchService } from "../_data/application-search.service";
import { ApplicatonSearchForm } from "../_models/application-search.form";
import { ApplicationSearchModel } from "../_models/application-search.model";

@Component({
  selector: "cais-application-search-form",
  templateUrl: "./application-search-form.component.html",
  styleUrls: ["./application-search-form.component.scss"],
})
export class ApplicationSearchFormComponent extends CrudForm<
  ApplicationSearchModel,
  ApplicatonSearchForm,
  ApplicationSearchResolverData,
  ApplicationSearchService
> {
  constructor(
    service: ApplicationSearchService,
    public injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super(service, injector);
    this.backUrl = "pages/applications/search";
  }

  displayTitle: string = "Преглед";

  ngOnInit(): void {
    this.fullForm = new ApplicatonSearchForm();
    let generatedId = this.fullForm.id.value;

    this.fullForm.group.patchValue(this.dbData.element);
    if (!this.fullForm.id.value) {
      this.fullForm.id.patchValue(generatedId);
    }
    this.formFinishedLoading.emit();
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: ApplicationSearchModel) {
    return new ApplicationSearchModel(object);
  }
}
