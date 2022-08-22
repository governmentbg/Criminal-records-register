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
import { InternalRequestTypeCodeConstants } from "./_models/internal-request-type-code.constants";
import { BaseNomenclatureModel } from "../../../@core/models/nomenclature/base-nomenclature.model";

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
  public isSend: boolean = false;
  public personIds: BaseNomenclatureModel[] = [];

  ngOnInit(): void {
    this.fullForm = new InternalRequestForm();
    this.fullForm.group.patchValue(this.dbData.element);
    this.fullForm.regNumberDisplay.patchValue(this.fullForm.regNumber.value);
    this.requestStatusCode = this.fullForm.reqStatusCode.value;
    this.personBulletins = this.dbData.personBulletins;

    debugger;
    // when has create from bulletin
    if (this.dbData.bulletinInfo) {
      this.fullForm.nIntReqTypeId.patchValue(
        InternalRequestTypeCodeConstants.Rehabilitation
      );
      this.setPidData(this.dbData.bulletinInfo.pids);
    }
    // create from person
    if (this.dbData.bulletinsInfoByPerson) {
      this.setPidData(this.dbData.bulletinsInfoByPerson.pids);
    }

    // this is replay
    if (this.requestStatusCode == InternalRequestStatusCodeConstants.Sent) {
      this.initDataForSend();
    } else {
      if (this.isEdit()) {
        this.canEditGrid = true;
        this.title = "Редакция на заявка";
      } else {
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
    this.isSend = true;
    this.fullForm.reqStatusCode.patchValue(
      InternalRequestStatusCodeConstants.Sent
    );
    this.submitFunction();
  }

  protected onSubmitSuccess(data: any) {
    if (this.isSend) {
      this.router.navigateByUrl("pages/internal-requests?activeTab=draft");
      return;
    }

    let currentUrl = this.router.url.toLocaleLowerCase();
    let index = currentUrl.indexOf(this.CREATE_ACTION);
    let editUrl = currentUrl.substr(0, index) + this.EDIT_ACTION;
    if (data?.id) {
      let newUrl = editUrl + "/" + data.id;
      this.router.navigateByUrl(newUrl);
    } else {
      // Important, if we modify some navigation properties they must refresh
      this.reloadCurrentRoute();
    }
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

  // public openPidDialog = () => {
  //   this.dialogService
  //     .open(SelectPidDialogComponent, CommonConstants.defaultDialogConfig)
  //     .onClose.subscribe(this.onSelectPid);
  // };

  // public onSelectPid = (item) => {
  //   if (item) {
  //     this.loaderService.show();
  //     let oldSelectedPid = this.fullForm.pPersIdId.id.value;
  //     this.fullForm.pPersIdId.setValue(item.id, item.pid);
  //     // call service for bulletins
  //     this.service.getBulletinForPerson(item.id).subscribe((response) => {
  //       // if select new person, delete old bulletins
  //       if (oldSelectedPid != item.id) {
  //         this.deleteOldBulletins();
  //       }

  //       // add or update person bulletins
  //       this.addOrUpdateBulletins(response);
  //       this.loaderService.hide();
  //     });
  //   }
  // };

  private initDataForSend() {
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
  }

  public onBulletinDeleted(rowContext) {
    let pk = rowContext.data.id;
    this.personBulletins = this.personBulletinsGrid.data.filter(
      (d) => d.id != pk
    );
  }

  public onBulletinGridRendered() {
    let bulletins = [];
    if (this.dbData.bulletinInfo) {
      bulletins = this.dbData.bulletinInfo.bulletins;
    } else if (this.dbData.bulletinsInfoByPerson) {
      bulletins = this.dbData.bulletinsInfoByPerson.bulletins;
    }

    bulletins.forEach((element) => {
      var guid = Guid.create().toString();
      let newBulletin = element;
      newBulletin.id = guid;
      this.personBulletinsGrid.addRow(newBulletin);
    });
  }

  private setPidData(pids) {
    if (pids && pids.length > 0) {
      this.personIds = pids;
      this.setDeffPid();
    }
  }

  private setDeffPid() {
    let egn = this.personIds.filter((x) => x.code == "EGN")[0]?.id;

    if (egn) {
      this.fullForm.pPersIdId.patchValue(egn);
      return;
    }

    let lnch = this.personIds.filter((x) => x.code == "LNCH")[0]?.id;
    if (lnch) {
      this.fullForm.pPersIdId.patchValue(lnch);
      return;
    }

    let ln = this.personIds.filter((x) => x.code == "LN")[0]?.id;
    if (lnch) {
      this.fullForm.pPersIdId.patchValue(ln);
      return;
    }

    let docId = this.personIds.filter((x) => x.code == "DOC_ID")[0]?.id;
    if (docId) {
      this.fullForm.pPersIdId.patchValue(docId);
      return;
    }

    let suidId = this.personIds.filter((x) => x.code == "SYS")[0]?.id;
    if (suidId) {
      this.fullForm.pPersIdId.patchValue(suidId);
      return;
    }
  }

  // private deleteRow(pk) {
  //   this.personBulletinsGrid.deleteRow(pk);
  //   this.personBulletinsGrid.data = this.personBulletinsGrid.data.filter(
  //     (d) => d.id != pk
  //   );
  // }

  // private deleteOldBulletins() {
  //   this.personBulletinsGrid.groupingFlatResult.forEach((currentBulletins) => {
  //     let currentRow = this.personBulletinsGrid.getRowByKey(
  //       currentBulletins.id
  //     );

  //     if (currentRow) {
  //       let pk = currentBulletins.id;
  //       this.deleteRow(pk);
  //     }
  //   });
  // }

  // private addOrUpdateBulletins(response) {
  //   response.forEach((bulletin) => {
  //     let currentRow = this.personBulletinsGrid.getRowByKey(bulletin.id);

  //     if (currentRow) {
  //       currentRow.update(bulletin);
  //     } else {
  //       var guid = Guid.create().toString();
  //       bulletin.id = guid;
  //       this.personBulletinsGrid.addRow(bulletin);
  //     }
  //   });
  // }
}
