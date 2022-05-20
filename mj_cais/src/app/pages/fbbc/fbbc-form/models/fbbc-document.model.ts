import { Byte } from "@angular/compiler/src/util";
import { BaseModel } from "../../../../@core/models/common/base.model";

export class FbbcDocumentModel extends BaseModel {
  public name: string = null;
  public descr: string = null;
  public docTypeId: string = null;
  public docTypeName: string = null;
  public documentContent: Byte[] = [];
  public documentContentId: string = null;

  constructor(init?: Partial<FbbcDocumentModel>) {
    super(init);
    if (init) {
      this.name = init.name ?? null;
      this.descr = init.descr ?? null;
      this.docTypeId = init.docTypeId ?? null;
      this.docTypeName = init.docTypeName ?? null;
      this.documentContent = init.documentContent ?? null;
      this.documentContentId = init.documentContentId ?? null;
    }
  }
}
