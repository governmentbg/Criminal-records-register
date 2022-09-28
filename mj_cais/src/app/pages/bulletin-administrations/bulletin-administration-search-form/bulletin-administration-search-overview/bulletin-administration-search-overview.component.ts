import { Component, Injector, Input, ViewChild } from "@angular/core";
import { IgxDialogComponent } from "@infragistics/igniteui-angular";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { BaseNomenclatureModel } from "../../../../@core/models/nomenclature/base-nomenclature.model";
import { DateFormatService } from "../../../../@core/services/common/date-format.service";
import { BulletinAdministrationSearchForm } from "../_models/bulletin-administration-search.form";
import { BulletinAdministrationSearchGridService } from "./_data/bulletin-administration-search-grid.service";
import { BulletinAdministrationGridModel } from "./_models/bulletin-administration-grid.model";
import { DeleteBulletinForm } from "./_models/delete-bulletin-form";
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
    public dateFormatService: DateFormatService
  ) {
    super("bulletins-administration-search", service, injector);
  }

  @Input() searchForm: BulletinAdministrationSearchForm;
  @ViewChild("dialogAdd", { read: IgxDialogComponent })
  public dialog: IgxDialogComponent;

  @ViewChild("deleteDialog", { read: IgxDialogComponent })
  public deleteDialog: IgxDialogComponent;

  public ulockBulletinForm: UnlockBulletinForm;
  public deleteBulletinForm: DeleteBulletinForm;
  public statuses: BaseNomenclatureModel[] = [];
  public isLoading: boolean = false;

  ngOnInit() {
    this.ulockBulletinForm = new UnlockBulletinForm();
    this.deleteBulletinForm = new DeleteBulletinForm();
  }

  public onSearch = () => {
    if (!this.searchForm.group.valid) {
      this.searchForm.group.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");
      return;
    }

    let formObj = this.searchForm.group.getRawValue();

    let filterQuery = this.service.constructQueryParamsByFilters(formObj, "");
    this.service.updateUrl(`bulletins-administration?${filterQuery}`);
    super.ngOnInit();
  };

  onOpenDialog(bulletinId: string, currentStatus: string, version: number) {
    this.service.getBulletinStatusHistory(bulletinId).subscribe({
      next: (data) => {
        this.statuses = data;
        this.ulockBulletinForm.status.patchValue(currentStatus);
        this.ulockBulletinForm.version.patchValue(version);
        this.ulockBulletinForm.bulletinId.patchValue(bulletinId);

        this.dialog.open();
      },
      error: (errorResponse) => {
        this.errorHandler(errorResponse);
      },
    });
  }

  onOpenDialogForDelete(bulletinId: string) {
    this.deleteBulletinForm.id.patchValue(bulletinId);
    this.deleteDialog.open();
  }

  onUnlockBulletin() {
    if (!this.ulockBulletinForm.group.valid) {
      this.ulockBulletinForm.group.markAllAsTouched();
      return;
    }

    let bulletinId = this.ulockBulletinForm.bulletinId.value;
    let model = this.ulockBulletinForm.group.value;

    this.isLoading = true;
    this.service.unlockBulletin(bulletinId, model).subscribe({
      next: (data) => {
        this.isLoading = false;
        this.toastr.showToast(
          "success",
          "Успешно отключен бюлетин за редакция!"
        );
        this.onCloseDilog();
        setTimeout(() => {
          this.grid.deleteRow(bulletinId);
        }, 500);
      },
      error: (errorResponse) => {
        this.isLoading = false;
        this.errorHandler(errorResponse);
      },
    });
  }

  onDeleteBulletin() {
    if (!this.deleteBulletinForm.group.valid) {
      this.deleteBulletinForm.group.markAllAsTouched();
      return;
    }

    let model = this.deleteBulletinForm.group.value;
    this.isLoading = true;
    this.service.deleteBulletin(model.id, model).subscribe({
      next: (data) => {
        this.isLoading = false;
        this.toastr.showToast("success", "Успешно изтрит бюлетин!");
        this.onCloseDilogForDelete();
        setTimeout(() => {
          this.grid.deleteRow(model.id);
        }, 500);
      },
      error: (errorResponse) => {
        this.isLoading = false;
        this.errorHandler(errorResponse);
      },
    });
  }

  onCloseDilog() {
    this.ulockBulletinForm = new UnlockBulletinForm();
    this.dialog.close();
  }

  onCloseDilogForDelete() {
    this.deleteBulletinForm = new DeleteBulletinForm();
    this.deleteDialog.close();
  }
}
