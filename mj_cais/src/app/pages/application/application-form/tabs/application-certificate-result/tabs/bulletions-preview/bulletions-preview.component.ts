import { Component, Input, OnInit } from "@angular/core";
import { NbDialogRef } from "@nebular/theme";
import { BaseNomenclatureModel } from "../../../../../../../@core/models/nomenclature/base-nomenclature.model";
import { ApplicationCertificateResultBulletionPreviewModel } from "../application-certificate-result-bulletion-preview/_model/application-certificate-result-bulletion-preview.model";
import { AppCertResultBulletinsPreviewService } from "./_data/app-cert-result-bulletins-preview.service";

@Component({
  selector: "cais-bulletions-preview",
  templateUrl: "./bulletions-preview.component.html",
  styleUrls: ["./bulletions-preview.component.scss"],
})
export class BulletionsPreviewComponent implements OnInit {
  public certId: string;
  public bulletins: ApplicationCertificateResultBulletionPreviewModel[];
  constructor(
    private service: AppCertResultBulletinsPreviewService,
    protected ref: NbDialogRef<BulletionsPreviewComponent>
  ) {}

  ngOnInit(): void {
    this.service.getConvitionsOnly(this.certId).subscribe(result => {
      this.bulletins = result;
    })
  }
}
