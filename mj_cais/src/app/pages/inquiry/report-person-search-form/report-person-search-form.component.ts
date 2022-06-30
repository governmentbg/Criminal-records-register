import { Component, Injector, Input, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { NbDialogService } from "@nebular/theme";
import { NgxSpinnerService } from "ngx-spinner";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { ReportPersonResolverData } from "./_data/report-person.resolver";
import { ReportPersonService } from "./_data/report-person.service";
import { ReportPersonSearchForm } from "./_models/report-person-search.form";
import { ReportPersonSearchModel } from "./_models/report-person-search.model";

@Component({
  selector: "cais-report-person-search-form",
  templateUrl: "./report-person-search-form.component.html",
  styleUrls: ["./report-person-search-form.component.scss"],
})
export class ReportPersonSearchFormComponent
  extends CrudForm<
    ReportPersonSearchModel,
    ReportPersonSearchForm,
    ReportPersonResolverData,
    ReportPersonService
  >
  implements OnInit
{
  constructor(
    service: ReportPersonService,
    public injector: Injector,
    private spinner: NgxSpinnerService,
    private dialogService: NbDialogService
  ) {
    super(service, injector);
  }

  @Input() title: string = "Справка по характеристики на лице";

  ngOnInit(): void {
    this.fullForm = new ReportPersonSearchForm();
    // this.fullForm.group.patchValue(this.dbData.element);
    this.formFinishedLoading.emit();
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: ReportPersonSearchModel) {
    return new ReportPersonSearchModel(object);
  }

  public onSearch = () => {
    if (!this.fullForm.group.valid) {
      this.fullForm.group.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");
      return;
    }

    this.spinner.show();
    setTimeout(() => {
      /** spinner ends after 5 seconds */
      this.spinner.hide();
    }, 500);

    let categoryIsAdded = false;
    let filterQuery = "";
    let formObj = this.fullForm.group.getRawValue();
    for (let key in formObj) {
      if (key && formObj[key]) {
        if (typeof formObj[key] == "object" && formObj[key]._isUTC != null) {
          let date = new Date(formObj[key]);
          let result = date.toISOString();
          filterQuery += `&${key}=${result}`;
        } else {
          filterQuery += `&${key}=${formObj[key]}`;
        }
      }
    }

    //this.personReportOverview.onSearch(filterQuery);
  };
}
