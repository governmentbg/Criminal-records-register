import {
  Component,
  Inject,
  Input,
  LOCALE_ID,
  OnInit,
  ViewChild,
} from "@angular/core";
import {
  IgxDialogComponent,
  IgxGridComponent,
  IgxGridRowComponent,
} from "@infragistics/igniteui-angular";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { BulletinDecisionForm } from "../../models/bulletin-decision.form";
import { formatDate } from "@angular/common";

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

  constructor(
    public dateFormatService: DateFormatService,
    @Inject(LOCALE_ID) private locale: string
  ) {}

  public onOpenEditBulletinDecision(event: IgxGridRowComponent) {
    this.bulletinDecisionForm.group.patchValue(event.rowData);
    this.dialog.open();
  }

  public onDeleteBulletinDecision(event: IgxGridRowComponent) {
    this.decisionsGrid.deleteRow(event.rowData.id);
    this.decisionsGrid.data = this.decisionsGrid.data.filter(
      (d) => d.id != event.rowData.id
    );
  }

  onAddOrUpdateBulletineDecisionRow() {
    if (!this.bulletinDecisionForm.group.valid) {
      this.bulletinDecisionForm.group.markAllAsTouched();
      return;
    }

    this.updateDecisionValues();

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

  private updateDecisionValues() {
    let decisionDescr = [];

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
      decisionDescr.push(decisionTypeName);
    }

    if (this.bulletinDecisionForm.decisionNumber.value) {
      decisionDescr.push(this.bulletinDecisionForm.decisionNumber.value);
    }

    if (this.bulletinDecisionForm.decisionDate.value) {
      var decisionDateStr = formatDate(
        this.bulletinDecisionForm.decisionDate.value,
        "dd.MM.yyyy HH:mm",
        this.locale
      );
      decisionDescr.push(decisionDateStr);
    }

    if (this.bulletinDecisionForm.decisionFinalDate.value) {
      var decisionFinalDateStr = formatDate(
        this.bulletinDecisionForm.decisionFinalDate.value,
        "dd.MM.yyyy HH:mm",
        this.locale
      );
      decisionDescr.push(decisionFinalDateStr);
    }

    if (this.bulletinDecisionForm.decisionAuthId.value) {
      let decisionAuthName = (this.dbData.decidingAuthorities as any).find(
        (x) => x.id === this.bulletinDecisionForm.decisionAuthId.value
      )?.name;
      this.bulletinDecisionForm.decisionAuthName.patchValue(decisionAuthName);
      decisionDescr.push(decisionAuthName);
    }

    if (this.bulletinDecisionForm.decisionEcli.value) {
      decisionDescr.push(this.bulletinDecisionForm.decisionEcli.value);
    }

    if (decisionDescr.length > 0) {
      var decisionDescVal = decisionDescr.join("/");
      this.bulletinDecisionForm.decisionDecrition.patchValue(decisionDescVal);
    }
  }
}
