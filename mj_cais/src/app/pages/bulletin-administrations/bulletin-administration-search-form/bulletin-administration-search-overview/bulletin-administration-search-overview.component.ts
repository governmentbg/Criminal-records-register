import { Component, Injector, Input, ViewChild } from "@angular/core";
import { IgxDialogComponent } from "@infragistics/igniteui-angular";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { BaseNomenclatureModel } from "../../../../@core/models/nomenclature/base-nomenclature.model";
import { DateFormatService } from "../../../../@core/services/common/date-format.service";
import { LoaderService } from "../../../../@core/services/common/loader.service";
import { BulletinAdministrationSearchForm } from "../_models/bulletin-administration-search.form";
import { BulletinAdministrationSearchGridService } from "./_data/bulletin-administration-search-grid.service";
import { BulletinAdministrationGridModel } from "./_models/bulletin-administration-grid.model";
import { UnlockBulletinForm } from "./_models/unlock-bulletin.form";

@Component({
  selector: "cais-bulletin-administration-search-overview",
  templateUrl: "./bulletin-administration-search-overview.component.html",
  styleUrls: ["./bulletin-administration-search-overview.component.scss"],
})
export class BulletinAdministrationSearchOverviewComponent extends RemoteGridWithStatePersistance<
  BulletinAdministrationGridModel,
  BulletinAdministrationSearchGridService
> {
  constructor(
    service: BulletinAdministrationSearchGridService,
    injector: Injector,
    public dateFormatService: DateFormatService,
    public loaderService: LoaderService
  ) {
    super("bulletins-administration-search", service, injector);
  }

  @Input() searchForm: BulletinAdministrationSearchForm;
  @ViewChild("dialogAdd", { read: IgxDialogComponent })
  public dialog: IgxDialogComponent;
  public ulockBulletinForm: UnlockBulletinForm;
  public statuses: BaseNomenclatureModel[] = [];

  ngOnInit() {
    this.ulockBulletinForm = new UnlockBulletinForm();
  }

  public onSearch = () => {
    if (!this.searchForm.group.valid) {
      this.searchForm.group.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");
      return;
    }

    this.loaderService.showSpinner(this.service);

    let formObj = this.searchForm.group.getRawValue();

    let filterQuery = this.service.constructQueryParamsByFilters(formObj, "");
    this.service.updateUrl(`bulletins-administration?${filterQuery}`);
    super.ngOnInit();
    this.loaderService.hideSpinner(this.service);
  };

  onOpenDialog(bulletinId: string, currentStatus: string, version: number) {
    this.loaderService.show();
    this.service.getBulletinStatusHistory(bulletinId).subscribe({
      next: (data) => {
        this.statuses = data;
        this.ulockBulletinForm.status.patchValue(currentStatus);
        this.ulockBulletinForm.version.patchValue(version);
        this.ulockBulletinForm.bulletinId.patchValue(bulletinId);
        this.loaderService.hide();
        this.dialog.open();
      },
      error: (errorResponse) => {
        this.loaderService.hide();
        this.errorHandler(errorResponse);
      },
    });
  }

  onUnlockBulletin() {
    if (!this.ulockBulletinForm.group.valid) {
      this.ulockBulletinForm.group.markAllAsTouched();
      return;
    }

    this.loaderService.show();
    let bulletinId = this.ulockBulletinForm.bulletinId.value;
    let model = this.ulockBulletinForm.group.value;

    this.service.unlockBulletin(bulletinId, model).subscribe({
      next: (data) => {
        this.loaderService.hide();
        this.toastr.showToast("success", "Успешно отключен бюлетин за редакция!");
        this.onCloseDilog();
        setTimeout(() => {
          this.grid.deleteRow(bulletinId);
        }, 500);
      },
      error: (errorResponse) => {
        this.loaderService.hide();
        this.errorHandler(errorResponse);
      },
    });
  }

  onCloseDilog() {
    this.ulockBulletinForm = new UnlockBulletinForm();
    this.dialog.close();
  }
}
