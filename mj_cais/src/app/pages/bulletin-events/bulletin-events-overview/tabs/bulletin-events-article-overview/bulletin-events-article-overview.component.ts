import { Component, Injector, Input } from "@angular/core";
import { NbDialogService } from "@nebular/theme";
import { ConfirmDialogComponent } from "../../../../../@core/components/dialogs/confirm-dialog-component/confirm-dialog-component.component";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { BulletinEventsGridModel } from "../../_models/bulletin-events-grid.model";
import { BulletinEventsStatusTypeEnum } from "../../_models/bulletin-events-status-type.enum";
import { BulletinEventsArticleGridService } from "./_data/bulletin-events-article-grid.service";

@Component({
  selector: "cais-bulletin-events-article-overview",
  templateUrl: "./bulletin-events-article-overview.component.html",
  styleUrls: ["./bulletin-events-article-overview.component.scss"],
})
export class BulletinEventsArticleOverviewComponent extends RemoteGridWithStatePersistance<
  BulletinEventsGridModel,
  BulletinEventsArticleGridService
> {
  constructor(
    service: BulletinEventsArticleGridService,
    injector: Injector,
    private dialogService: NbDialogService,
    public dateFormatService: DateFormatService
  ) {
    super("bulletins-events-article-search", service, injector);
  }

  @Input() bulletinId: string;
  public BulletinEventsStatusTypeEnum = BulletinEventsStatusTypeEnum;
  public hideStatus: boolean = true;

  ngOnInit() {
    this.service.updateEventStatusUrl(
      BulletinEventsStatusTypeEnum.New,
      this.bulletinId
    );
    super.ngOnInit();
  }

  onShowAllBulletinEventsChange(isChacked: boolean) {
    if (isChacked) {
      this.service.updateEventStatusUrl(null, this.bulletinId);
    } else {
      this.service.updateEventStatusUrl(
        BulletinEventsStatusTypeEnum.New,
        this.bulletinId
      );
    }

    this.hideStatus = !isChacked;
    super.ngOnInit();
  }

  public approveStatus(id: string) {
    buletinStatus: BulletinEventsStatusTypeEnum;
    this.dialogService
      .open(ConfirmDialogComponent, {
        context: {
          color: "success",
        },
        closeOnBackdropClick: false,
      })
      .onClose.subscribe((result) => {
        if (result) {
          this.changeStatus(id, BulletinEventsStatusTypeEnum.Approved);
        }
      });
  }

  public rejectStatus(id: string) {
    status: BulletinEventsStatusTypeEnum.Approved;
    this.dialogService
      .open(ConfirmDialogComponent, {
        context: {
          color: "danger",
        },
        closeOnBackdropClick: false,
      })
      .onClose.subscribe((result) => {
        if (result) {
          this.changeStatus(id, BulletinEventsStatusTypeEnum.Rejected);
        }
      });
  }

  public changeStatus(id: string, status: BulletinEventsStatusTypeEnum) {
    this.service.changeStatus(id, status).subscribe(
      (res) => {
        let message =
          status == BulletinEventsStatusTypeEnum.Approved
            ? "????????????????????"
            : "????????????????????";
        this.toastr.showToast("success", `?????????????? ${message} ??????????????????????????`);
        this.ngOnInit();
      },
      (error) => {
        let title = this.dangerMessage;
        let errorText = error.status + " " + error.statusText;
        if (error.error && error.error.customMessage) {
          title = error.error.customMessage;
          errorText = "";
        }

        this.toastr.showBodyToast("danger", title, errorText);
      }
    );
  }
}
