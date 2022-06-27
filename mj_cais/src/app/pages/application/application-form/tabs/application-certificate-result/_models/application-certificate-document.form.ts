import { FormControl, FormGroup } from "@angular/forms";

export class ApplicationCertificateDocumentForm {
  public group: FormGroup;

  public documentContent: FormControl;

  constructor() {
    this.documentContent = new FormControl(null);

    this.group = new FormGroup({
      documentContent: this.documentContent,
    });
  }
}
