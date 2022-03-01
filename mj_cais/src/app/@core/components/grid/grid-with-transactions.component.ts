import {
    AfterContentInit,
    Component,
    ContentChild,
    Input,
    TemplateRef,
    ViewChild,
  } from "@angular/core";
  import { FormControl } from "@angular/forms";
  import { ActivatedRoute } from "@angular/router";
  import {
    IgxGridComponent,
    IgxGridTransaction,
    IgxTransactionService,
  } from "@infragistics/igniteui-angular";

  @Component({
    providers: [{ provide: IgxGridTransaction, useClass: IgxTransactionService }],
    selector: "app-grid-with-transactions",
    template: `<ng-template igxRowEditText let-rowChangesCount #edit>
        Промени: {{ rowChangesCount }} извършени промени.
      </ng-template>
      <div [ngClass]="setInvalidContainer()">
        <ng-content></ng-content>

        <cais-validation-message
          [showGridMessage]="
            gridTransactions?.errors?.required && gridHasBeenInteracted()
          "
        ></cais-validation-message>
      </div> `,
  })
  export class GridWithTransactionsComponent implements AfterContentInit {
    @Input() gridTransactions: FormControl;
  
    @ViewChild("edit", { read: TemplateRef }) rowEditTemplate: TemplateRef<any>;
    @ContentChild(IgxGridComponent) grid: IgxGridComponent;
  
    public gridHasBeenInteracted(): boolean {
      if (this.gridTransactions) {
        return this.gridTransactions.touched || this.gridTransactions.dirty;
      } else {
        return false;
      }
    }
  
    ngAfterContentInit() {
      const previewParam = this.activatedRoute.snapshot.data["preview"];
  
      if (previewParam) {
        this.grid.rowEditable = false;
        this.grid.actionStrip = null;
        this.grid.rowEditText;
        this.grid.columns.forEach((column) => {
          column.editable = false;
        });
      }
    }
  
    public setInvalidContainer(): string {
      if (this.gridTransactions) {
        let isTouched =
          this.gridTransactions.touched || this.gridTransactions.dirty;
        return this.gridTransactions.invalid && isTouched ? "ng-invalid" : "";
      } else {
        return "";
      }
    }
  
    ngAfterViewInit() {
      this.grid.rowEditTextDirectives.reset([this.rowEditTemplate]);
    }
  
    constructor(private activatedRoute: ActivatedRoute) {}
  }
  