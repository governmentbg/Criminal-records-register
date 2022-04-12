import { FbbcForm } from "./models/fbbc.form";
import { Component, Injector, OnInit, ViewChild } from "@angular/core";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { FbbcModel } from "./models/fbbc.model";
import { FbbcService } from "./data/fbbc.service";
import { FbbcResolverData } from "./data/fbbc.resolver";
import { FormGroup } from "@angular/forms";
import { FbbcDocumentFormComponent } from "./grids/fbbc-document-form/fbbc-document-form.component";
import { FbbcStatusTypeEnum } from "../fbbc-overview/data/fbbc-status-type.constants";
import { CommonConstants } from "../../../@core/constants/common.constants";
import { DocTypeConstants } from "../../../@core/constants/doc-type.constants";
import { NbDialogService } from "@nebular/theme";
import { CountryGridModel } from "../../../@core/components/forms/address-form/dialog/_models/country-grid.model";
import { CountryDialogComponent } from "../../../@core/components/forms/address-form/dialog/country-dialog/country-dialog.component";

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
  public bgCountryId = CommonConstants.bgCountryId;
  public docType = DocTypeConstants.ecris;

  constructor(
    service: FbbcService,
    public injector: Injector,
    private dialogService: NbDialogService
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
    this.fullForm.docTypeId.disable();
    this.fullForm.group.patchValue(this.dbData.element);
    this.formFinishedLoading.emit();
    this.isForEdit = this.activatedRoute.snapshot.data["edit"];
  }

  submitFunction = () => {
    this.validateAndSave(this.fullForm);
  };

  deleteFunction = () => {
    debugger;
    let id = this.activatedRoute.snapshot.params["ID"];
    this.service.changeStatus(id, FbbcStatusTypeEnum.Deleted);
  };

  public openCountryDialog = () => {
    this.dialogService
      .open(CountryDialogComponent, CommonConstants.defaultDialogConfig)
      .onClose.subscribe(this.onSelectCountry);
  };

  public onSelectCountry = (item: CountryGridModel) => {
    if (item) {
      this.fullForm.country.setValue(item.id, item.name);
    }
  };
}
