import { Component, Injector } from "@angular/core";
import { NbDialogService } from "@nebular/theme";
import * as fileSaver from "file-saver";
import { ConfirmTemplateDialogComponent } from "../../../@core/components/dialogs/confirm-template-dialog/confirm-template-dialog.component";
import { RemoteGridWithStatePersistance } from "../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../@core/services/common/date-format.service";
import { LoaderService } from "../../../@core/services/common/loader.service";
import { EcrisOutboxGridService } from "./_data/ecris-outbox-grid.service";
import { EcrisOutboxGridModel } from "./_models/ecris-outbox-grid.model";
import { EcrisOutboxStatusConstants } from "./_models/ecris-outbox-status.constants";

@Component({
  selector: "cais-ecris-outbox-overview",
  templateUrl: "./ecris-outbox-overview.component.html",
  styleUrls: ["./ecris-outbox-overview.component.scss"],
})
export class EcrisOutboxOverviewComponent extends RemoteGridWithStatePersistance<
  EcrisOutboxGridModel,
  EcrisOutboxGridService
> {
  public hideStatus: boolean = true;

  constructor(
    public dateFormatService: DateFormatService,
    service: EcrisOutboxGridService,
    injector: Injector,
    private dialogService: NbDialogService,
    public loaderService: LoaderService
  ) {
    super("ecris-inbox-search", service, injector);
    this.service.updateUrlStatus(EcrisOutboxStatusConstants.Error);
  }

  ngOnInit() {
    super.ngOnInit();
  }

  onShowAllMessageChange(isChacked: boolean) {
    this.loaderService.showSpinner(this.service);
    if (isChacked) {
      //removed filter entirely
      this.service.updateUrlStatus();
    } else {
      this.service.updateUrlStatus(EcrisOutboxStatusConstants.Error);
    }
    this.hideStatus = !isChacked;
    this.ngOnInit();
  }

  downloadXml(id: string) {
    this.loaderService.show();
    this.service.download(id).subscribe(
      (response: any) => {
        let blob = new Blob([response.body]);
        window.URL.createObjectURL(blob);

        let header = response.headers.get("Content-Disposition");
        let filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;

        let fileName = "download";

        var matches = filenameRegex.exec(header);
        if (matches != null && matches[1]) {
          fileName = matches[1].replace(/['"]/g, "");
        }

        fileSaver.saveAs(blob, fileName);
        this.loaderService.hide();
      },
      (error) => {
        this.loaderService.hide();
        var errorText = error.status + " " + error.statusText;
        this.toastr.showBodyToast(
          "danger",
          "Грешка при изтегляне на файла: ",
          errorText
        );
      }
    );
  }

  resend(ecrisMsgId: string) {
    this.dialogService
      .open(ConfirmTemplateDialogComponent, {
        context: {
          color: "success",
          title: "Повторно изпращане на съобщение?",
        },
        closeOnBackdropClick: false,
      })
      .onClose.subscribe((result) => {
        if (result) {
          this.service.resend(ecrisMsgId).subscribe(
            (res) => {
              this.toastr.showToast(
                "success",
                "Успешно повторно изпращане на съобщение"
              );
              this.ngOnInit();
            },
            (error) => {
              let errorText = error.status + " " + error.statusText;

              this.toastr.showBodyToast(
                "danger",
                "Възникна грешка:",
                errorText
              );
            }
          );
        }
      });
  }
}
