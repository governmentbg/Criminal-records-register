import { FbbcForm } from "./models/fbbc.form";
import { Component, Injector, OnInit, ViewChild } from "@angular/core";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { FbbcModel } from "./models/fbbc.model";
import { FbbcService } from "./data/fbbc.service";
import { FbbcResolverData } from "./data/fbbc.resolver";
import { FormGroup } from "@angular/forms";
import { FbbcDocumentFormComponent } from "./grids/fbbc-document-form/fbbc-document-form.component";
import { FbbcStatusTypeEnum } from "../fbbc-overview/_data/fbbc-status-type.constants";
import { CommonConstants } from "../../../@core/constants/common.constants";
import { DocTypeConstants } from "../../../@core/constants/doc-type.constants";
import { NbDialogService } from "@nebular/theme";
import { CountryGridModel } from "../../../@core/components/forms/address-form/dialog/_models/country-grid.model";
import { CountryDialogComponent } from "../../../@core/components/forms/address-form/dialog/country-dialog/country-dialog.component";
import { DateFormatService } from "../../../@core/services/common/date-format.service";
import { PersonContextEnum } from "../../../@core/components/forms/person-form/_models/person-context-enum";
import { EcrisMessageService } from "../../ecris/ecris-message-form/_data/ecris-message.service";
import { EcrisReqPreviewComponent } from "../../ecris/ecris-message-form/ecris-req-preview/ecris-req-preview.component";

@Component({
  selector: "cais-fbbc-form",
  templateUrl: "./fbbc-form.component.html",
  styleUrls: ["./fbbc-form.component.scss"],
})
export class FbbcFormComponent
  extends CrudForm<FbbcModel, FbbcForm, FbbcResolverData, FbbcService>
  implements OnInit
{
  @ViewChild("fbbcDocuments", {
    read: FbbcDocumentFormComponent,
  })
  public fbbcDocumentsForm: FbbcDocumentFormComponent;
  public fbbcService: FbbcService;

  public isForEdit: boolean = false;
  public isForCreate: boolean = false;
  public bgCountryId = CommonConstants.bgCountryId;
  public docType = DocTypeConstants.ECRIS;
  public PersonContextEnum = PersonContextEnum;

  constructor(
    service: FbbcService,
    public injector: Injector,
    private dialogService: NbDialogService,
    private ecrisMessageService: EcrisMessageService,
    public dateFormatService: DateFormatService
  ) {
    super(service, injector);
    this.backUrl = "pages/fbbcs";
    this.setDisplayTitle("Осъдени в чужбина");
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: FbbcModel) {
    return new FbbcModel(object);
  }

  ngOnInit(): void {
    this.fullForm = new FbbcForm();
    let generatedId = this.fullForm.id.value;

    this.fullForm.group.patchValue(this.dbData.element);
    if (!this.fullForm.id.value) {
      this.fullForm.id.patchValue(generatedId);
    }

    this.formFinishedLoading.emit();
    this.isForEdit = this.activatedRoute.snapshot.data["edit"];
    this.isForCreate = this.activatedRoute.snapshot.outlet === "primary";
    if (this.isForEdit) {
      this.fullForm.docTypeId.disable();
    }
  }

  submitFunction = () => {
    this.fullForm.docTypeId.enable();
    if (this.isForCreate || this.isForEdit) {
      this.fullForm.docTypeId.setValue(DocTypeConstants.CBSHandwritten);
      this.fullForm.statusCode.setValue(FbbcStatusTypeEnum.Active);
    }
    this.validateAndSave(this.fullForm);
  };

  deleteFunction = () => {
    let id = this.activatedRoute.snapshot.params["ID"];
    this.service
      .changeStatus(id, FbbcStatusTypeEnum.Deleted)
      .subscribe((res) => {
        this.reloadCurrentRoute();
      });
  };

  public openCountryDialog = () => {
    this.dialogService
      .open(CountryDialogComponent, CommonConstants.defaultDialogConfig)
      .onClose.subscribe(this.onSelectCountry);
  };

  public onSelectCountry = (item: CountryGridModel) => {
    if (item) {
      this.fullForm.countryLookup.setValue(item.id, item.name);
    }
  };

  getDocument(id: string) {
    debugger;
    // this.ecrisMessageService.getEcrisDocument(id).subscribe((x) => {
    //   debugger;
    // });

    this.dialogService
    .open(EcrisReqPreviewComponent, CommonConstants.defaultDialogConfig)
    .onClose.subscribe(x => {});
  }
}
