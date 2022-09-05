import { Component, Injector, Input, OnInit, ViewChild } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import {
  ConnectedPositioningStrategy,
  HorizontalAlignment,
  IgxDialogComponent,
  NoOpScrollStrategy,
  VerticalAlignment,
} from "@infragistics/igniteui-angular";
import { NbMenuService } from "@nebular/theme";
import { filter, map } from "rxjs";
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
    private loader: LoaderService,
    private formBuilder: FormBuilder
  ) {
    super("people-search", service, injector);
  }

  @ViewChild("connectPersonDialog", { read: IgxDialogComponent })
  public connectPersonDialog: IgxDialogComponent;

  @Input() searchForm: PersonSearchForm;
  @Input() isRemindPersonForm: boolean;
  @Input() existingPersonId: string;

  public connectPersonFormGroup: FormGroup;

  public overlaySettings = {
    positionStrategy: new ConnectedPositioningStrategy({
      horizontalDirection: HorizontalAlignment.Left,
      horizontalStartPoint: HorizontalAlignment.Right,
      verticalStartPoint: VerticalAlignment.Bottom,
    }),
    scrollStrategy: new NoOpScrollStrategy(),
  };

  ngOnInit() {
    this.service.updateUrl(`people?isPageInit=true`);
    this.connectPersonFormGroup = this.formBuilder.group({
      desc: [{ value: "", disabled: false }, Validators.required],
      id: [{ value: "", disabled: false }],
      personToBeConnected: [{ value: "", disabled: false }],
    });
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
      } else {
        formObj["birthDate"] = null;
      }
    } else {
      formObj["birthDate"] = null;
    }
    let filterQuery = this.service.constructQueryParamsByFilters(formObj, "");
    this.service.updateUrl(`people?${filterQuery}`);

    super.ngOnInit();
    this.loader.hideSpinner(this.service);
  };

  public connectPeople() {
    if (!this.connectPersonFormGroup.valid) {
      this.connectPersonFormGroup.markAllAsTouched();
      return;
    }

    this.loader.show();
    let formValue = this.connectPersonFormGroup.getRawValue();
    this.service.connectPeople(formValue).subscribe({
      next: (response) => {
        this.connectPersonDialog.close();
        this.loader.hide();
        this.router.navigateByUrl("people/preview/" + formValue.id);
      },
      error: (errorResponse) => {
        this.loader.hide();
        this.errorHandler(errorResponse);
      },
    });
  }

  public openConnectPeopleDialog(existingPersonId, personToBeConnected) {
    this.connectPersonFormGroup.controls.id.patchValue(existingPersonId);
    this.connectPersonFormGroup.controls.personToBeConnected.patchValue(
      personToBeConnected
    );
    this.connectPersonDialog.open();
  }
}
