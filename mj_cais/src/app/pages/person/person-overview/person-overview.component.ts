import { Component, Injector } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../@core/services/common/date-format.service";
import { PersonGridService } from "./_data/person-grid.service";
import { PersonSearchForm } from "./_models/person-search.form";
import { PersonGridModel } from "./_models/person.grid.model";

@Component({
  selector: "cais-person-overview",
  templateUrl: "./person-overview.component.html",
  styleUrls: ["./person-overview.component.scss"],
})
export class PersonOverviewComponent extends RemoteGridWithStatePersistance<
  PersonGridModel,
  PersonGridService
> {
  constructor(
    service: PersonGridService,
    injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("people-search", service, injector);
  }

  public fullForm: PersonSearchForm;
  ngOnInit() {
    this.service.updateUrl(`people?isPageInit=true`);

    this.fullForm = new PersonSearchForm();
    super.ngOnInit();
  }

  onSearch = () => {
    if (!this.fullForm.group.valid) {
      this.fullForm.group.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");
      return;
    } 

    //$filter=contains(bulletinType,%20%27as%27)%20and%20contains(createdOn,%20%272%27)
    let count = 0;
    let filterQuery = "";

    let formObj = this.fullForm.group.getRawValue();
    if (this.fullForm.birthDate.precision.value) {
      let date = this.fullForm.birthDate.getFullYear();
      if (date) {
        formObj["birthDateDisplay"] = date.toISOString();
        formObj["birthDatePrec"] = this.fullForm.birthDate.precision.value;
      }
    }

    for (let key in formObj) {
      if (formObj[key] && key && key != "birthDate") {
        count++;
        if (count == 1) {
          filterQuery += `$filter=contains(${key},'${formObj[key]}')`;
        } else {
          filterQuery += ` and contains(${key},'${formObj[key]}')`;
        }
      }
    }

    this.service.updateUrl(`people?${filterQuery}`);
    super.ngOnInit();
  };
  
}
