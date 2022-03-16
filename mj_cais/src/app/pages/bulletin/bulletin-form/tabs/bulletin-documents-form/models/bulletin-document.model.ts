import { Byte } from "@angular/compiler/src/util";

export class BulletinDocumentModel {
  public id: string = null;
  public name: string = null;
  public descr: string = null;
  public docTypeId: string = null;
  public docTypeName: string = null;
  public documentContent: Byte[] = [];
  public documentContentId: string = null;

  constructor(init?: Partial<BulletinDocumentModel>) {
    if (init) {
      this.id = init.id ?? null;
      this.name = init.name ?? null;
      this.descr = init.descr ?? null;
      this.docTypeId = init.docTypeId ?? null;
      this.docTypeName = init.docTypeName ?? null;
      this.documentContent = init.documentContent ?? null;
      this.documentContentId = init.documentContentId ?? null
    }
  }
}
