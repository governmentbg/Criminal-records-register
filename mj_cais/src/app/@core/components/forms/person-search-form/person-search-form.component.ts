import { Component, Injector, Input, OnInit, ViewChild } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import {
  ConnectedPositioningStrategy,
  GridSelectionMode,
  HorizontalAlignment,
  IgxDialogComponent,
  IgxGridComponent,
  NoOpScrollStrategy,
  VerticalAlignment,
} from "@infragistics/igniteui-angular";
import { CrudForm } from "../../../directives/crud-form.directive";
import { DateFormatService } from "../../../services/common/date-format.service";
import { LoaderService } from "../../../services/common/loader.service";
import { FormUtils } from "../../../utils/form.utils";
import { PidTypeEnum } from "../person-form/_models/pid-type-enum";
import { PersonSearchService } from "./_data/person-search.service";
import { PersonSearchForm } from "./_models/person-search.form";
import { PersonSearchGridModel } from "./_models/person-search.grid";
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
    null,
    PersonSearchService
  >
  implements OnInit
{
  grid: IgxGridComponent;
  constructor(
    service: PersonSearchService,
    public injector: Injector,
    public dateFormatService: DateFormatService,
    private loader: LoaderService,
    private formBuilder: FormBuilder
  ) {
    super(service, injector);
  }

  @Input() title: string = "Търсене на лице";
  @Input() existingPersonId: string;
  @Input() isRemindPersonForm: boolean = false;
  @Input() personContextData: PersonSearchModel;

  public connectPersonFormGroup: FormGroup;
  public isLoadingGrid: boolean = false;
  public searchIsLoading = false;
  public isLoadingPersonData = false;
  public enableButtons = false;
  public people: PersonSearchGridModel[];

  public isPersonSelection = false;

  public selectedItem: any;
  public selectionMode: GridSelectionMode = GridSelectionMode.none;

  @ViewChild("connectPersonDialog", { read: IgxDialogComponent })
  public connectPersonDialog: IgxDialogComponent;

  public overlaySettings = {
    positionStrategy: new ConnectedPositioningStrategy({
      horizontalDirection: HorizontalAlignment.Left,
      horizontalStartPoint: HorizontalAlignment.Right,
      verticalStartPoint: VerticalAlignment.Bottom,
      verticalDirection: VerticalAlignment.Middle,
    }),
    scrollStrategy: new NoOpScrollStrategy(),
  };

  ngOnInit(): void {
    this.fullForm = new PersonSearchForm();
    // load data from app or report
    if (this.personContextData) {
      this.fullForm.group.patchValue(this.personContextData);
      this.enableButtons = true;
      this.isPersonSelection = true;
      this.selectionMode = GridSelectionMode.single;
    }

    this.connectPersonFormGroup = this.formBuilder.group({
      desc: [{ value: "", disabled: false }, Validators.required],
      id: [{ value: "", disabled: false }],
      personToBeConnected: [{ value: "", disabled: false }],
    });

    this.formFinishedLoading.emit();
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: PersonSearchModel) {
    return new PersonSearchModel(object);
  }

  public onLoadPersonData() {
    let pidType;
    let pid;
    if (this.fullForm.egn.value) {
      pid = this.fullForm.egn.value;
      pidType = PidTypeEnum.Egn;
    } else {
      pid = this.fullForm.lnch.value;
      pidType = PidTypeEnum.Lnch;
    }

    if (pid && pidType) {
      this.isLoadingPersonData = true;
      this.service.loadPersonDataByPid(pid, pidType).subscribe({
        next: (response) => {
          this.isLoadingPersonData = false;
          this.fullForm.firstname.patchValue(response.firstname);
          this.fullForm.surname.patchValue(response.surname);
          this.fullForm.familyname.patchValue(response.familyname);
          this.fullForm.fullname.patchValue(response.fullname);
          this.fullForm.egn.patchValue(response.egn);
          this.fullForm.lnch.patchValue(response.lnch);
          if (response.birthDate) {
            this.fullForm.birthDate.patchValue(new Date(response.birthDate));
          } else {
            this.fullForm.birthDate.patchValue(null);
          }
        },
        error: (errorResponse) => {
          this.isLoadingPersonData = false;
          this.errorHandler(errorResponse);
        },
      });
    }
  }

  public onSearch = () => {
    if (!this.fullForm.group.valid) {
      this.fullForm.group.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");
      return;
    }

    let formObj = this.fullForm.group.getRawValue();

    if (this.fullForm.birthDate.value) {
      formObj["birthDate"] = this.fullForm.birthDate.value.toISOString();
    }

    let filterQuery = this.service.constructQueryParamsByFilters(formObj, "");
    this.isLoadingGrid = true;
    this.service.searchPerson(filterQuery).subscribe({
      next: (response) => {
        this.isLoadingGrid = false;
        this.people = response;
      },
      error: (errorResponse) => {
        this.isLoadingGrid = false;

        this.errorHandler(errorResponse);
      },
    });
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

  public handleRowSelection(event) {
    this.selectedItem = event.newSelection[0];
  }
}
