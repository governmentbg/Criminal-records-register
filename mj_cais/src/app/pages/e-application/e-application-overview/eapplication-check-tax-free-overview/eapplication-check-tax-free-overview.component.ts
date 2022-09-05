import { Component, Injector, OnInit } from "@angular/core";
import { NbDialogService } from "@nebular/theme";
import { ConfirmTemplateDialogComponent } from "../../../../@core/components/dialogs/confirm-template-dialog/confirm-template-dialog.component";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../@core/services/common/date-format.service";
import { EApplicationGridService } from "../_data/eapplication-grid.service";
import { EApplicationGridModel } from "../_models/e-application-grid.model";
import { EApplicationTypeStatusEnum } from "../_models/e-application-type-status.enum";

@Component({
  selector: "cais-eapplication-check-tax-free-overview",
  templateUrl: "./eapplication-check-tax-free-overview.component.html",
  styleUrls: ["./eapplication-check-tax-free-overview.component.scss"],
})
export class EApplicationCheckTaxFreeOverviewComponent extends RemoteGridWithStatePersistance<
  EApplicationGridModel,
  EApplicationGridService
> {
  constructor(
    private dialogService: NbDialogService,
    service: EApplicationGridService,
    injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("e-application-check-tax-free-search", service, injector);
    this.service.updateUrlStatus(EApplicationTypeStatusEnum.CheckTaxFree);
  }

  ngOnInit(): void {
    super.ngOnInit();
  }

  public approve(id: any): void {
    let rowId = id;
    this.dialogService
      .open(ConfirmTemplateDialogComponent, {
        context: {
          color: "success",
          title: "Потвърди освобождаване от плащане?",
        },
        closeOnBackdropClick: false,
      })
      .onClose.subscribe((result) => {
        if (result) {
          this.service.processTaxFree(rowId, true).subscribe(
            (res) => {
              this.deleteRowHandler(
                rowId,
                "Потвърдено освободждаване от плащане."
              );
            },
            (error) => this.errorHandler(error)
          );
        }
      });
  }

  public reject(id: any): void {
    let rowId = id;
    this.dialogService
      .open(ConfirmTemplateDialogComponent, {
        context: {
          color: "danger",
          title: "Отхвърли освобождаване от плащане?",
        },
        closeOnBackdropClick: false,
      })
      .onClose.subscribe((result) => {
        if (result) {
          this.service.processTaxFree(rowId, false).subscribe(
            (res) => {
              this.deleteRowHandler(
                rowId,
                "Отхвърлено освободждаване от плащане."
              );
            },
            (error) => this.errorHandler(error)
          );
        }
      });
  }
}
