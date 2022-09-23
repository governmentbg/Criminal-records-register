import { Component, Injector, Input, ViewChild } from "@angular/core";
import { IgxDialogComponent } from "@infragistics/igniteui-angular";
import { PersonModel } from "../../../../../@core/components/forms/person-form/_models/person.model";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { BaseNomenclatureModel } from "../../../../../@core/models/nomenclature/base-nomenclature.model";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { LoaderService } from "../../../../../@core/services/common/loader.service";
import { NomenclatureService } from "../../../../../@core/services/rest/nomenclature.service";
import { PersonPidGridService } from "./_data/person-pid-grid.service";
import { PersonPidGridModel } from "./_models/person-pid-grid.model";
import { RemovePidDialogFrom } from "./_models/remove-pid-dialog.form";

@Component({
  selector: "cais-person-pid-overview",
  templateUrl: "./person-pid-overview.component.html",
  styleUrls: ["./person-pid-overview.component.scss"],
})
export class PersonPidOverviewComponent extends RemoteGridWithStatePersistance<
  PersonPidGridModel,
  PersonPidGridService
> {
  public personId: string;
  public countries: BaseNomenclatureModel[];
  public genderTypes: BaseNomenclatureModel[];
  public personForm: RemovePidDialogFrom = new RemovePidDialogFrom();
  public isLoading: boolean = false;

  @Input() personModel: PersonModel;

  @ViewChild("removePidDialog", { read: IgxDialogComponent })
  public dialog: IgxDialogComponent;

  constructor(
    public service: PersonPidGridService,
    public injector: Injector,
    public dateFormatService: DateFormatService,
    public nomenclatureService: NomenclatureService
  ) {
    super("person-pids-search", service, injector);
  }

  ngOnInit() {
    let personIdParams = this.activatedRoute.snapshot.params["ID"];
    this.personId = personIdParams;
    this.service.setPersonId(personIdParams);
    super.ngOnInit();

    this.nomenclatureService.getCountries().subscribe((resp) => {
      this.countries = resp;
    });

    this.nomenclatureService.getGenderTypes().subscribe((resp) => {
      this.genderTypes = resp;
    });
  }

  onOpenDialogForRemovePid(personId, pidId) {
    if (this.personModel.birthDate) {
      this.personModel.birthDate = new Date(this.personModel.birthDate);
    }
    this.personForm.group.patchValue(this.personModel);
    this.personForm.existinPersonId.patchValue(personId);
    this.personForm.pidId.patchValue(pidId);
    this.dialog.open();
  }

  onRemovePid() {
    if (!this.personForm.group.valid) {
      this.personForm.group.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");
      return;
    }

    this.isLoading = true;
    let formObject = this.personForm.group.value;
    this.service.removePid(formObject).subscribe({
      next: (response) => {
        this.isLoading = false;
        this.dialog.close();
        this.toastr.showToast("success", "Успешно премахване на идентификатор");
        this.ngOnInit();
      },
      error: (errorResponse) => {
        this.isLoading = false;
        this.errorHandler(errorResponse);
      },
    });
  }

  onCloseDilog() {
    this.dialog.close();
  }
}
