import { Component, Injector, OnInit, ViewChild } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { BulletinStatusTypeEnum } from "../bulletin-overview/_models/bulletin-status-type.enum";
import { BulletinSearchOverviewComponent } from "./bulletin-search-overview/bulletin-search-overview.component";
import { BulletinSearchResolverData } from "./_data/bulletin-search.resolver";
import { BulletinSearchService } from "./_data/bulletin-search.service";
import { BulletinSearchForm } from "./_models/bulletin-search.form";

@Component({
  selector: "cais-bulletin-search-form",
  templateUrl: "./bulletin-search-form.component.html",
  styleUrls: ["./bulletin-search-form.component.scss"],
})
export class BulletinSearchFormComponent
  extends CrudForm<
    null,
    BulletinSearchForm,
    BulletinSearchResolverData,
    BulletinSearchService
  >
  implements OnInit
{
  constructor(service: BulletinSearchService, public injector: Injector) {
    super(service, injector);
  }

  @ViewChild("searchBulletinGrid")
  searchBulletinGrid: BulletinSearchOverviewComponent;

  ngOnInit(): void {
    this.fullForm = new BulletinSearchForm();
    this.fullForm.statusId.patchValue(BulletinStatusTypeEnum.Active);
    this.fullForm.group.patchValue(this.dbData.element);
    this.formFinishedLoading.emit();
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: any) {
    return null;
  }

  public onSearch = () => {
    this.searchBulletinGrid.onSearch();
  };
}
