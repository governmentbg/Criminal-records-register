import { Component, Injector, OnInit, ViewChild } from "@angular/core";
import { FormGroup, Validators } from "@angular/forms";
import { NbDialogService } from "@nebular/theme";
import { NgxSpinnerService } from "ngx-spinner";
import { CommonConstants } from "../../../@core/constants/common.constants";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { SelectPidDialogComponent } from "./dialogs/select-pid-dialog/select-pid-dialog.component";
import { InternalRequestResolverData } from "./_data/internal-request.resolver";
import { InternalRequestService } from "./_data/internal-request.service";
import { InternalRequestStatusCodeConstants } from "./_models/internal-request-status-code.constants";
import { InternalRequestForm } from "./_models/internal-request.form";
import { InternalRequestModel } from "./_models/internal-request.model";
import { IgxGridComponent } from "@infragistics/igniteui-angular";
import { PersonBulletinsGridModel } from "./_models/person-bulletin-grid-model";
import { Guid } from "guid-typescript";

@Component({
  selector: "cais-internal-request-form",
  templateUrl: "./internal-request-form.component.html",
  styleUrls: ["./internal-request-form.component.scss"],
})
export class InternalRequestFormComponent
  extends CrudForm<
    InternalRequestModel,
    InternalRequestForm,
    InternalRequestResolverData,
    InternalRequestService
  >
  implements OnInit
{
  constructor(
    service: InternalRequestService,
    public injector: Injector,
    private dialogService: NbDialogService,
    private loaderService: NgxSpinnerService
  ) {
    super(service, injector);
  }

  public title: string = "Добавяне на заявка";
  public InternalRequestStatusCodeConstants =
    InternalRequestStatusCodeConstants;
  public requestStatusCode;
  public showReplayBtn: boolean = false;
  public canEditGrid: boolean = false;

  public personBulletinsGridTransactions: string;
  public personBulletins: PersonBulletinsGridModel[];
  @ViewChild("personBulletinsGrid", {
    read: IgxGridComponent,
  })
  public personBulletinsGrid: IgxGridComponent;

  public dbData: any;
  ngOnInit(): void {
    this.fullForm = new InternalRequestForm();
    this.fullForm.group.patchValue(this.dbData.element);
    this.fullForm.regNumberDisplay.patchValue(this.fullForm.regNumber.value);
    this.requestStatusCode = this.fullForm.reqStatusCode.value;
    this.personBulletins = this.dbData.personBulletins;

    // this is replay
    if (this.requestStatusCode == InternalRequestStatusCodeConstants.Sent) {
      this.fullForm.group.disable();
      this.fullForm.responseDescr.enable();
      this.fullForm.responseDescr.setValidators(Validators.required);

      if (this.isEdit()) {
        this.showReplayBtn = true;
        this.title = "Отговор на заявка";
      }

      // check before change prop
      if (this.isForPreview) {
        this.showReplayBtn = false;
        this.title = "Преглед на заявка";
      }

      this.formFinishedLoading.emit();

      this.isForPreview = true;
    } else {
      if (this.isEdit()) {
        this.canEditGrid = true;
        this.title = "Редакция на заявка";
      }else{
        this.canEditGrid = true;
      }

      if (this.isForPreview) {
        this.title = "Преглед на заявка";
      }
      this.formFinishedLoading.emit();
    }
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: InternalRequestModel) {
    return new InternalRequestModel(object);
  }

  submitFunction = () => {
    if (this.personBulletinsGrid) {
      let selectedBulletinsTransaction =
        this.personBulletinsGrid.transactions.getAggregatedChanges(true);

      this.fullForm.selectedBulletinsTransactions.setValue(
        selectedBulletinsTransaction
      );
    } else {
      this.fullForm.selectedBulletinsTransactions.setValue([]);
    }
    this.validateAndSave(this.fullForm);
  };

  replay(accepted) {
    if (!this.fullForm.responseDescr.valid) {
      this.fullForm.responseDescr.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");
      this.scrollToValidationError();
      return;
    }

    let replayObj = {
      accepted: accepted,
      responseDescr: this.fullForm.responseDescr.value,
    };

    this.service.replay(this.fullForm.id.value, replayObj).subscribe(
      (res) => {
        debugger;
        this.loaderService.hide();
        this.toastr.showToast("success", "Успешно изпратена заявка");
        let activeTab = this.activatedRoute.snapshot.queryParams["activeTab"];
        // is opened by judge
        if (activeTab) {
          if (activeTab == "for-judge") {
            this.router.navigateByUrl("pages/internal-requests/for-judge");
            return;
          }
        }

        // is normal employee
        this.router.navigate(["pages/internal-requests"], {
          queryParams: { activeTab: "inbox" },
        });
      },
      (error) => {
        this.onServiceError(error);
      }
    );
  }

  public send() {
    this.service
      .changeStatus(
        this.fullForm.id.value,
        InternalRequestStatusCodeConstants.Sent
      )
      .subscribe(
        (res) => {
          this.loaderService.hide();
          this.toastr.showToast("success", "Успешно изпратена заявка");        
          this.router.navigate(["pages/internal-requests"], {
            queryParams: { activeTab: "draft" },
          });
        },
        (error) => {
          this.onServiceError(error);
        }
      );
  }

  public onCancelFunction = () => {
    let activeTab = this.activatedRoute.snapshot.queryParams["activeTab"];
    let url = "pages/internal-requests?activeTab=draft";

    if (activeTab) {
      if (activeTab == "for-judge") {
        url = "pages/internal-requests/for-judge";
      } else {
        url = `pages/internal-requests?activeTab=${activeTab}`;
      }
    }

    this.router.navigateByUrl(url);
  };

  public openPidDialog = () => {
    this.dialogService
      .open(SelectPidDialogComponent, CommonConstants.defaultDialogConfig)
      .onClose.subscribe(this.onSelectPid);
  };

  public onSelectPid = (item) => {
    if (item) {
      this.loaderService.show();
      let oldSelectedPid = this.fullForm.pPersIdId.id.value;
      this.fullForm.pPersIdId.setValue(item.id, item.pid);
      // call service for bulletins
      this.service.getBulletinForPerson(item.id).subscribe((response) => {
        debugger;
        // if select new person, delete old bulletins
        if (oldSelectedPid != item.id) {
          this.deleteOldBulletins();
        }

        // add or update person bulletins
        this.addOrUpdateBulletins(response);
        this.loaderService.hide();
      });
    }
  };

  public onBulletinDeleted(rowContext) {
    let pk = rowContext.data.id;
    this.personBulletins = this.personBulletinsGrid.data.filter(
      (d) => d.id != pk
    );
  }

  private deleteRow(pk) {
    this.personBulletinsGrid.deleteRow(pk);
    this.personBulletinsGrid.data = this.personBulletinsGrid.data.filter(
      (d) => d.id != pk
    );
  }

  private deleteOldBulletins() {
    this.personBulletinsGrid.groupingFlatResult.forEach((currentBulletins) => {
      let currentRow = this.personBulletinsGrid.getRowByKey(
        currentBulletins.id
      );

      if (currentRow) {
        let pk = currentBulletins.id;
        this.deleteRow(pk);
      }
    });
  }

  private addOrUpdateBulletins(response) {
    response.forEach((bulletin) => {
      let currentRow = this.personBulletinsGrid.getRowByKey(bulletin.id);

      if (currentRow) {
        currentRow.update(bulletin);
      } else {
        var guid = Guid.create().toString();
        bulletin.id = guid;
        this.personBulletinsGrid.addRow(bulletin);
      }
    });
  }
}
