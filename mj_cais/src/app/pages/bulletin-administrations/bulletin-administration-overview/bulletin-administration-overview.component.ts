import { Component, Injector, OnInit, ViewChild } from "@angular/core";
import { IgxDialogComponent } from "@infragistics/igniteui-angular";
import { RemoteGridWithStatePersistance } from "../../../@core/directives/remote-grid-with-state-persistance.directive";
import { BaseNomenclatureModel } from "../../../@core/models/nomenclature/base-nomenclature.model";
import { DateFormatService } from "../../../@core/services/common/date-format.service";
import { LoaderService } from "../../../@core/services/common/loader.service";
import { NomenclatureService } from "../../../@core/services/rest/nomenclature.service";
import { BulletinAdministrationGridService } from "./_data/bulletin-administration-grid.service";
import { BulletinAdministrationGridModel } from "./_models/bulletin-administration-grid.model";
import { UnlockBulletinForm } from "./_models/unlock-bulletin.form";

@Component({
  selector: "cais-bulletin-administration-overview",
  templateUrl: "./bulletin-administration-overview.component.html",
  styleUrls: ["./bulletin-administration-overview.component.scss"],
})
export class BulletinAdministrationOverviewComponent extends RemoteGridWithStatePersistance<
  BulletinAdministrationGridModel,
  BulletinAdministrationGridService
> {
  @ViewChild("dialogAdd", { read: IgxDialogComponent })
  public dialog: IgxDialogComponent;
  public ulockBulletinForm: UnlockBulletinForm;
  public statuses: BaseNomenclatureModel[] = [];

  constructor(
    service: BulletinAdministrationGridService,
    injector: Injector,
    public dateFormatService: DateFormatService,
    public nomenclatureService: NomenclatureService,
    public loader: LoaderService
  ) {
    super("bulletins-administration-search", service, injector);
  }

  public hideStatus: boolean = true;

  ngOnInit() {
    this.ulockBulletinForm = new UnlockBulletinForm();
    super.ngOnInit();
  }

  onOpenDialog(bulletinId: string, currentStatus: string, version: number) {
    this.loader.show();
    this.service.getBulletinStatusHistory(bulletinId).subscribe(
      (response) => {
        this.statuses = response;
        this.ulockBulletinForm.status.patchValue(currentStatus);
        this.ulockBulletinForm.version.patchValue(version);
        this.ulockBulletinForm.bulletinId.patchValue(bulletinId);
        this.loader.hide();
        this.dialog.open();
      },
      (error) => {
        this.loader.hide();
        this.errorHandler(error);
      }
    );
  }

  onUnlockBulletin() {
    if (!this.ulockBulletinForm.group.valid) {
      this.ulockBulletinForm.group.markAllAsTouched();
      return;
    }

    this.loader.show();
    let bulletinId = this.ulockBulletinForm.bulletinId.value;
    let model = this.ulockBulletinForm.group.value;

    this.service.unlockBulletin(bulletinId, model).subscribe({
      next: (data) => {
        this.loader.hide();
        this.toastr.showToast("success", this.successMessage);
        this.onCloseDilog();
        setTimeout(() => {
          this.grid.deleteRow(bulletinId);
        }, 500);
      },
      error: (errorResponse) => {
        this.loader.hide();
        let title = this.dangerMessage;
        let errorText = errorResponse.status + " " + errorResponse.statusText;

        if (errorResponse.error && errorResponse.error.customMessage) {
          title = errorResponse.error.customMessage;
          errorText = "";
        }

        this.toastr.showBodyToast("danger", title, errorText);
      },
    });
  }

  onCloseDilog() {
    this.ulockBulletinForm = new UnlockBulletinForm();
    this.dialog.close();
  }
}
