import { Component, Injector, Input, OnInit, ViewChild } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { PersonSearchOverviewComponent } from "./grids/person-search-overview/person-search-overview.component";
import { PersonSearchResolverData } from "./_data/person-search.resolver";
import { PersonSearchService } from "./_data/person-search.service";
import { PersonSearchForm } from "./_models/person-search.form";
import { PersonSearchModel } from "./_models/person-search.model";

@Component({
  selector: "cais-person-search-form",
  templateUrl: "./person-search-form.component.html",
  styleUrls: ["./person-search-form.component.scss"],
})
export class PersonSearchFormComponent
  extends CrudForm<
    PersonSearchModel,
    PersonSearchForm,
    PersonSearchResolverData,
    PersonSearchService
  >
  implements OnInit
{
  constructor(service: PersonSearchService, public injector: Injector) {
    super(service, injector);
  }

  @Input() title: string = 'Търсене на лице';
  @Input() existingPersonId: string;
  @Input() isRemindPersonForm: boolean = false;

  @ViewChild("personGridForm")
  searchPersonGridForm: PersonSearchOverviewComponent;

  ngOnInit(): void {
    this.fullForm = new PersonSearchForm();
    this.fullForm.group.patchValue(this.dbData.element);
    this.formFinishedLoading.emit();
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: PersonSearchModel) {
    return new PersonSearchModel(object);
  }

  onSearch = () => {
    this.searchPersonGridForm.onSearch();
  };
}
