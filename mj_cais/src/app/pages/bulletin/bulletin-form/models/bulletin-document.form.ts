import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";

export class BulletinDocumentForm {
  public group: FormGroup;
  public id: FormControl;
  public name: FormControl;
  public descr: FormControl;
  public docTypeId: FormControl;
  public docTypeName: FormControl;
  public documentContent: FormControl;
  public documentContentId: FormControl;
  public mimeType: FormControl;

  constructor() {
    var guid = Guid.create().toString();
    var documentContentGuid = Guid.create().toString();
    this.id = new FormControl(guid, [Validators.required]);
    this.name = new FormControl(null);
    this.descr = new FormControl(null);
    this.docTypeId = new FormControl(null);
    this.docTypeName = new FormControl(null);
    this.documentContent = new FormControl(null);
    this.documentContentId = new FormControl(documentContentGuid, [
      Validators.required,
    ]);
    this.mimeType = new FormControl(null);

    this.group = new FormGroup({
      id: this.id,
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
