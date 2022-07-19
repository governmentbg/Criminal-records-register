import { Component, Injector, Input, OnInit } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { LoaderService } from "../../../../../@core/services/common/loader.service";
import { PersonSearchForm } from "../../_models/person-search.form";
import { PersonSearchGridService } from "./_data/person-search-grid.service";
import { PersonSearchGridModel } from "./_models/person-search.grid";

@Component({
  selector: "cais-person-search-overview",
  templateUrl: "./person-search-overview.component.html",
  styleUrls: ["./person-search-overview.component.scss"],
})
export class PersonSearchOverviewComponent extends RemoteGridWithStatePersistance<
  PersonSearchGridModel,
  PersonSearchGridService
> {
  constructor(
    service: PersonSearchGridService,
    injector: Injector,
    public dateFormatService: DateFormatService,
    private loader: LoaderService
  ) {
    super("people-search", service, injector);
  }

  @Input() searchForm: PersonSearchForm;
  @Input() isRemindPersonForm: boolean;
  @Input() existingPersonId: string;

  ngOnInit() {
    this.service.updateUrl(`people?isPageInit=true`);
  }

  public onSearch = () => {
    if (!this.searchForm.group.valid) {
      this.searchForm.group.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");
      return;
    }

    this.loader.showSpinner(this.service);
    let formObj = this.searchForm.group.getRawValue();
    if (this.searchForm.birthDate.precision.value) {
      let date = this.searchForm.birthDate.getFullYear();
      if (date) {
        formObj["birthDatePrec"] = this.searchForm.birthDate.precision.value;
        formObj["birthDate"] = date.toISOString();
      }else{
        formObj["birthDate"] = null;
      }
    }else{
      formObj["birthDate"] = null;
    }
    let filterQuery = this.service.constructQueryParamsByFilters(formObj, "");
    this.service.updateUrl(`people?${filterQuery}`);

    super.ngOnInit();
    this.loader.hideSpinner(this.service);
  };

  public connectPeople(existingPersonId, personToBeConnected) {
    this.service.connectPeople(existingPersonId, personToBeConnected).subscribe(
      (res) => {
        this.router.navigateByUrl("people/preview/" + existingPersonId);
      },

      (error) => {
        var errorText = error.status + " " + error.statusText;
        this.toastService.showMessage(
          "Възникна грешка при сливането на данни за лицата:" + errorText
        );
      }
    );
  }
}
