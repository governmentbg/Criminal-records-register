import { Component, Injector, Input, OnInit } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
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
  ) {
    super("people-search", service, injector);
  }

  @Input() searchForm: PersonSearchForm;
  @Input() isRemindPersonForm: boolean;
  @Input() existingPersonId: string;

  ngOnInit() {
    this.service.updateUrl(`people?isPageInit=true`);
    super.ngOnInit();
  }

  public onSearch = () => {
    if (!this.searchForm.group.valid) {
      this.searchForm.group.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");
      return;
    }

    //$filter=contains(bulletinType,%20%27as%27)%20and%20contains(createdOn,%20%272%27)
    let count = 0;
    let filterQuery = "";

    let formObj = this.searchForm.group.getRawValue();

    if (this.searchForm.birthDate.precision.value) {
      let date = this.searchForm.birthDate.getFullYear();
      if (date) {
        formObj["birthDateDisplay"] = date.toISOString();
        formObj["birthDatePrec"] = this.searchForm.birthDate.precision.value;
      }
    }

    // get birth place value
    if (this.searchForm.birthPlace.group.value) {
      formObj["cityId"] = this.searchForm.birthPlace.cityId.value;
      formObj["countryId"] = this.searchForm.birthPlace.country.id.value;
      formObj["districtId"] = this.searchForm.birthPlace.districtId.value;
      formObj["municipalityId"] =
        this.searchForm.birthPlace.municipalityId.value;
      formObj["foreignCountryAddress"] =
        this.searchForm.birthPlace.foreignCountryAddress.value;
    }

    formObj.birthPlace = null;
    formObj.birthDate = null;
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
