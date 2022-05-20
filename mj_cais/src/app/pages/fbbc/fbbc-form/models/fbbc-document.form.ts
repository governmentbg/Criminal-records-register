import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";
import { BaseForm } from "../../../../@core/models/common/base.form";

export class FbbcDocumentForm extends BaseForm {
  public group: FormGroup;

  public fbbcId: FormControl;
  public name: FormControl;
  public descr: FormControl;
  public docTypeId: FormControl;
  public docTypeName: FormControl;
  public documentContent: FormControl;
  public documentContentId: FormControl;
  public mimeType: FormControl;

  constructor() {
    super();
    var documentContentGuid = Guid.create().toString();
    this.name = new FormControl(null, [Validators.required]);
    this.descr = new FormControl(null);
    this.docTypeId = new FormControl(null);
    this.docTypeName = new FormControl(null);
    this.documentContent = new FormControl(null, [Validators.required]);
    this.documentContentId = new FormControl(documentContentGuid, [
      Validators.required,
    ]);
    this.mimeType = new FormControl(null);

    this.group = new FormGroup({
      id: this.id,
      version: this.version,
      name: this.name,
      descr: this.descr,
      docTypeId: this.docTypeId,
      docTypeName: this.docTypeName,
      documentContent: this.documentContent,
      documentContentId: this.documentContentId,
      mimeType: this.mimeType,
    });
  }
}
