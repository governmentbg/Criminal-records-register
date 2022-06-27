import { Byte } from "@angular/compiler/src/util";

export class ApplicationCertificateDocumentModel {
  public documentContent: Byte[] = [];

  constructor(init?: Partial<ApplicationCertificateDocumentModel>) {
    if (init) {
      this.documentContent = init.documentContent ?? null;
    }
  }
}
