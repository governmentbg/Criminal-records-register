import { FbbcForm } from "./models/fbbc.form";
import { Component, Injector, OnInit, ViewChild } from "@angular/core";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { FbbcModel } from "./models/fbbc.model";
import { FbbcService } from "./data/fbbc.service";
import { FbbcResolverData } from "./data/fbbc.resolver";
import { FormGroup } from "@angular/forms";
import { FbbcDocumentFormComponent } from "./grids/fbbc-document-form/fbbc-document-form.component";

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

  constructor(service: FbbcService, public injector: Injector) {
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
    this.fullForm.group.patchValue(this.dbData.element);
    this.formFinishedLoading.emit();
  }

  submitFunction = () => {
    this.validateAndSave(this.fullForm);
  };
}
