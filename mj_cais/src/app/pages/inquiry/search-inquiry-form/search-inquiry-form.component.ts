import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { DateFormatService } from "../../../@core/services/common/date-format.service";
import { SearchInquiryResolverData } from "./_data/search-inquiry.resolver";
import { SearchInquiryService } from "./_data/search-inquiry.service";
import { SearchInquiryForm } from "./_models/search-inquiry.form";
import { SearchInquiryModel } from "./_models/search-inquiry.model";

@Component({
  selector: "cais-search-inquiry-form",
  templateUrl: "./search-inquiry-form.component.html",
  styleUrls: ["./search-inquiry-form.component.scss"],
})
export class SearchInquiryFormComponent extends CrudForm<
  SearchInquiryModel,
  SearchInquiryForm,
  SearchInquiryResolverData,
  SearchInquiryService
> {
  constructor(
    service: SearchInquiryService,
    public injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super(service, injector);
    this.backUrl = "pages/inquiry/search-inquiry";
  }

  displayTitle: string = "Преглед";

  ngOnInit(): void {
    this.fullForm = new SearchInquiryForm();
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

  createInputObject(object: SearchInquiryModel) {
    return new SearchInquiryModel(object);
  }
}
