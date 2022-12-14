import { Component, Input, ViewChild } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import {
  ColumnPinningPosition,
  IgxDialogComponent,
  IgxGridComponent,
  IgxGridRowComponent,
  IPinningConfig,
} from "@infragistics/igniteui-angular";
import { EActions } from "@tl/tl-common";
import { CommonErrorHandlerService } from "../../../../../@core/services/common/common-error-handler.service";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { BulletinService } from "../../_data/bulletin.service";
import { BulletinDecisionForm } from "./_models/bulletin-decision.form";
import { BulletinDecisionModel } from "./_models/bulletin-decision.model";

@Component({
  selector: "cais-bulletin-decision-form",
  templateUrl: "./bulletin-decision-form.component.html",
  styleUrls: ["./bulletin-decision-form.component.scss"],
})
export class BulletinDecisionFormComponent {
  @Input() bulletinDecisionsTransactions: string;
  @Input() dbData: any;
  @Input() isForPreview: boolean;

  @ViewChild("decisionsGrid", {
    read: IgxGridComponent,
  })
  public decisionsGrid: IgxGridComponent;

  @ViewChild("dialogAdd", { read: IgxDialogComponent })
  public dialog: IgxDialogComponent;

  public bulletinDecisionForm = new BulletinDecisionForm();
  public decisions: BulletinDecisionModel[];
  public isDecisionPreview: boolean = false;
  public pinningConfig: IPinningConfig = { columns: ColumnPinningPosition.End };

  constructor(
    public dateFormatService: DateFormatService,
    private bulletinService: BulletinService,
    private activatedRoute: ActivatedRoute,
    private errorService: CommonErrorHandlerService
  ) {}

  ngOnInit(): void {
    if (
      this.activatedRoute.snapshot.routeConfig.path.toUpperCase() ===
      EActions.CREATE
    ) {
      this.decisions = [];
      return;
    }

    let bulletinId = this.activatedRoute.snapshot.params["ID"];
    this.bulletinService.getDecisions(bulletinId).subscribe({
      next: (response) => {
        this.decisions = response;
      },
      error: (errorResponse) => {
        this.errorService.errorHandler(errorResponse);
      },
    });
  }

  public onOpenEditBulletinDecision(event: IgxGridRowComponent) {
    this.isDecisionPreview = false;

    this.dateFormatService.parseDatesFromGridRow(event, [
      "decisionDate",
      "decisionFinalDate",
      "changeDate",
    ]);

    this.bulletinDecisionForm.group.patchValue(event.rowData);
    this.dialog.open();
  }

  public onDeleteBulletinDecision(event: IgxGridRowComponent) {
    this.decisionsGrid.deleteRow(event.rowData.id);
    this.decisionsGrid.data = this.decisionsGrid.data.filter(
      (d) => d.id != event.rowData.id
    );
  }

  onAddOrUpdateBulletinDecisionRow() {
    if (!this.bulletinDecisionForm.group.valid) {
      this.bulletinDecisionForm.group.markAllAsTouched();
      return;
    }

    if (this.bulletinDecisionForm.decisionChTypeId.value) {
      let decisionChTypeName = (this.dbData.decisionChTypes as any).find(
        (x) => x.id === this.bulletinDecisionForm.decisionChTypeId.value
      )?.name;
      this.bulletinDecisionForm.decisionChTypeName.patchValue(
        decisionChTypeName
      );
    }

    if (this.bulletinDecisionForm.decisionTypeId.value) {
      let decisionTypeName = (this.dbData.decisionTypes as any).find(
        (x) => x.id === this.bulletinDecisionForm.decisionTypeId.value
      )?.name;
      this.bulletinDecisionForm.decisionTypeName.patchValue(decisionTypeName);
    }

    if (this.bulletinDecisionForm.decisionAuthId.value) {
      let decisionAuthName = (this.dbData.decidingAuthorities as any).find(
        (x) => x.id === this.bulletinDecisionForm.decisionAuthId.value
      )?.name;
      this.bulletinDecisionForm.decisionAuthName.patchValue(decisionAuthName);
    }

    let currentRow = this.decisionsGrid.getRowByKey(
      this.bulletinDecisionForm.id.value
    );

    if (currentRow) {
      currentRow.update(this.bulletinDecisionForm.group.value);
    } else {
      this.decisionsGrid.addRow(this.bulletinDecisionForm.group.value);
    }

    this.onCloseBulletinDecisionDilog();
  }

  onCloseBulletinDecisionDilog() {
    this.bulletinDecisionForm = new BulletinDecisionForm();
    this.dialog.close();
  }

  public onOpenPreviewDecision(event: IgxGridRowComponent) {
    this.bulletinDecisionForm.group.disable();
    this.isDecisionPreview = true;
    this.dateFormatService.parseDatesFromGridRow(event, [
      "decisionDate",
      "decisionFinalDate",
      "changeDate",
    ]);

    this.bulletinDecisionForm.group.patchValue(event.rowData);
    this.dialog.open();
  }
}
