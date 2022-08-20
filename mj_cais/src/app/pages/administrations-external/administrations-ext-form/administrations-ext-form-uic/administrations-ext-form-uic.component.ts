import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IgxDialogComponent, IgxGridComponent, IgxGridRowComponent } from '@infragistics/igniteui-angular';
import { EActions } from '@tl/tl-common';
import { AdministrationsExtUicForm } from '../_models/administrations-ext-uic.form';
import { AdministrationsExtUICModel } from '../_models/administrations-ext-uic.model';
import { AdministrationsExtModel } from '../_models/administrations-ext.model';

@Component({
  selector: 'cais-administrations-ext-form-uic',
  templateUrl: './administrations-ext-form-uic.component.html',
  styleUrls: ['./administrations-ext-form-uic.component.scss']
})
export class AdministrationsExtFormUicComponent implements OnInit {
  @Input() transactions: any;
  @Input() administrationUics: string;
  @Input() dbData: any;
  @Input() isForPreview: boolean;

  @ViewChild("uicsGrid", {
    read: IgxGridComponent,
  })
  public uicsGrid: IgxGridComponent;

  @ViewChild("dialogAdd", { read: IgxDialogComponent })
  public dialog: IgxDialogComponent;

  public uicForm = new AdministrationsExtUicForm();
  @Input()
  uics: AdministrationsExtModel;
  
  constructor(
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    if (
      this.activatedRoute.snapshot.routeConfig.path.toUpperCase() ===
      EActions.CREATE
    ) {
      this.uics = new AdministrationsExtModel(
        {
          extAdministrationUics: []
        }
      );
      return;
    }
  }

  public onOpenEditBulletinDecision(event: IgxGridRowComponent) {
    this.uicForm.group.patchValue(event.rowData);
    this.dialog.open();
  }

  public onDeleteBulletinDecision(event: IgxGridRowComponent) {
    this.uicsGrid.deleteRow(event.rowData.id);
    this.uicsGrid.data = this.uicsGrid.data.filter(
      (d) => d.id != event.rowData.id
    );

    // aggregate transaction
    let uicsTransactions =
      this.uicsGrid.transactions.getAggregatedChanges(true);

    this.transactions.setValue(uicsTransactions);
  }

  onAddOrUpdateUicRow() {
    if (!this.uicForm.group.valid) {
      this.uicForm.group.markAllAsTouched();
      return;
    }


    let currentRow = this.uicsGrid.getRowByKey(
      this.uicForm.id.value
    );

    if (currentRow) {
      currentRow.update(this.uicForm.group.value);
    } else {
      this.uicsGrid.addRow(this.uicForm.group.value);
    }

    this.onCloseBulletinDecisionDilog();
    let uicsTransactions =
      this.uicsGrid.transactions.getAggregatedChanges(true);

    this.transactions.setValue(uicsTransactions);
  }

  onCloseBulletinDecisionDilog() {
    this.uicForm = new AdministrationsExtUicForm();
    this.dialog.close();
  }
}
