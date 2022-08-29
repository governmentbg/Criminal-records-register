import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../../../../../@core/services/rest/cais-crud.service";
import { ApplicationCertificateResultBulletionPreviewModel } from "../_model/application-certificate-result-bulletion-preview.model";


@Injectable({ providedIn: "root" })
export class ApplicationCertificateResultBulletionPreviewService extends CaisCrudService<ApplicationCertificateResultBulletionPreviewModel, string> {
  constructor(injector: Injector) {
    super(ApplicationCertificateResultBulletionPreviewModel, injector, "bulletins");
  }

 
}
