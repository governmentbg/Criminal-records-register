import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";
import { DocumentTypeEnum } from "./document-type-constants";

export class BulletinDocumentForm {
  public group: FormGroup;
  public id: FormControl;
  public bulletinId: FormControl;
  public name: FormControl;
  public descr: FormControl;
  public docTypeId: FormControl;
  public docTypeName: FormControl;
  public documentContent: FormControl;
  public documentContentId: FormControl;
  public mimeType: FormControl;
  public createdOn: FormControl;

  constructor() {
    var guid = Guid.create().toString();
    var documentContentGuid = Guid.create().toString();
    this.id = new FormControl(guid, [Validators.required]);
    this.name = new FormControl(null, [
      Validators.required,
      Validators.maxLength(200),
    ]);
    this.descr = new FormControl(null);
    this.docTypeId = new FormControl(DocumentTypeEnum.Unstructured, [
      Validators.maxLength(50),
    ]);
    this.docTypeName = new FormControl(null);
    this.documentContent = new FormControl(null, [Validators.required]);
    this.documentContentId = new FormControl(documentContentGuid, [
      Validators.required,
    ]);
    this.mimeType = new FormControl(null);
    this.createdOn = new FormControl(Date.now);
    this.group = new FormGroup({
      id: this.id,
      name: this.name,
      descr: this.descr,
      docTypeId: this.docTypeId,
      docTypeName: this.docTypeName,
      documentContent: this.documentContent,
      documentContentId: this.documentContentId,
      mimeType: this.mimeType,
      createdOn:this.createdOn
    });
  }
}
