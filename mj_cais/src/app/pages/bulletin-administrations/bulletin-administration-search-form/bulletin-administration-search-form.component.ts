import { Component, Injector, OnInit, ViewChild } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { BulletinAdministrationSearchOverviewComponent } from "./bulletin-administration-search-overview/bulletin-administration-search-overview.component";
import { BulletinAdministrationSearchResolverData } from "./_data/bulletin-administration-search.resolver";
import { BulletinAdministrationSearchService } from "./_data/bulletin-administration-search.service";
import { BulletinAdministrationSearchForm } from "./_models/bulletin-administration-search.form";

@Component({
  selector: "cais-bulletin-administration-search-form",
  templateUrl: "./bulletin-administration-search-form.component.html",
  styleUrls: ["./bulletin-administration-search-form.component.scss"],
})
export class BulletinAdministrationSearchFormComponent
  extends CrudForm<
    null,
    BulletinAdministrationSearchForm,
    BulletinAdministrationSearchResolverData,
    BulletinAdministrationSearchService
  >
  implements OnInit
{
  constructor(
    service: BulletinAdministrationSearchService,
    public injector: Injector
  ) {
    super(service, injector);
  }

  @ViewChild("searchBulletinGrid")
  searchBulletinGrid: BulletinAdministrationSearchOverviewComponent;

  ngOnInit(): void {
    this.fullForm = new BulletinAdministrationSearchForm();
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
