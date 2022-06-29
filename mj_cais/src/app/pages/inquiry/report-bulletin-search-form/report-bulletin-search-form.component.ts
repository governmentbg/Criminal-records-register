import { Component, Injector, Input, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { ReportBulletinResolverData } from "./_data/report-bulletin.resolver";
import { ReportBulletinService } from "./_data/report-bulletin.service";
import { ReportBulletinSearchForm } from "./_models/report-bulletin-search.form";
import { ReportBulletinSearchModel } from "./_models/report-bulletin-search.model";

@Component({
  selector: "cais-report-bulletin-search-form",
  templateUrl: "./report-bulletin-search-form.component.html",
  styleUrls: ["./report-bulletin-search-form.component.scss"],
})
export class ReportBulletinSearchFormComponent
  extends CrudForm<
    ReportBulletinSearchModel,
    ReportBulletinSearchForm,
    ReportBulletinResolverData,
    ReportBulletinService
  >
  implements OnInit
{
  constructor(service: ReportBulletinService, public injector: Injector) {
    super(service, injector);
  }

  @Input() title: string = "Справка по характеристики на бюлетини";

  ngOnInit(): void {
    this.fullForm = new ReportBulletinSearchForm();
    this.fullForm.group.patchValue(this.dbData.element);
    this.formFinishedLoading.emit();
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: ReportBulletinSearchModel) {
    return new ReportBulletinSearchModel(object);
  }

  // onSearch = () => {
  //   this.searchPersonGridForm.onSearch();
  // };
}
