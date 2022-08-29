import { Component, Injector, Input, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { NgxSpinnerService } from "ngx-spinner";
import { CrudForm } from "../../../../../../../@core/directives/crud-form.directive";
import { BaseNomenclatureModel } from "../../../../../../../@core/models/nomenclature/base-nomenclature.model";
import { NomenclatureService } from "../../../../../../../@core/services/rest/nomenclature.service";
import { BulletinService } from "../../../../../../bulletin/bulletin-form/_data/bulletin.service";
import { ApplicationCertificateResultBulletionPreviewService } from "./_data/application-certificate-result-bulletion-preview.service";
import { ApplicationCertificateResultBulletionPreviewForm } from "./_model/application-certificate-result-bulletion-preview.form";
import { ApplicationCertificateResultBulletionPreviewModel } from "./_model/application-certificate-result-bulletion-preview.model";

@Component({
  selector: "cais-application-certificate-result-bulletion-preview",
  templateUrl:
    "./application-certificate-result-bulletion-preview.component.html",
  styleUrls: [
    "./application-certificate-result-bulletion-preview.component.scss",
  ],
})
export class ApplicationCertificateResultBulletionPreviewComponent
  extends CrudForm<
    ApplicationCertificateResultBulletionPreviewModel,
    ApplicationCertificateResultBulletionPreviewForm,
    null,
    ApplicationCertificateResultBulletionPreviewService
  >
  implements OnInit
{
  @Input()
  public model: ApplicationCertificateResultBulletionPreviewModel;

  public decisionTypes: BaseNomenclatureModel[];
  public decidingAuthorities: BaseNomenclatureModel[];
  public caseTypes: BaseNomenclatureModel[];

  public displayTitle: string = "Преглед на бюлетини";
  constructor(
    service: BulletinService,
    public injector: Injector,
    private loaderService: NgxSpinnerService,
    private nomenclatureService: NomenclatureService
  ) {
    super(service, injector);
    this.loadNomenclatures();
  }

  ngOnInit(): void {
    let locked = true;
    this.fullForm = new ApplicationCertificateResultBulletionPreviewForm(
      this.isEdit(),
      locked
    );
    this.fullForm.group.patchValue(this.model);
    this.fullForm.group.disable(); 
    this.formFinishedLoading.emit();
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: ApplicationCertificateResultBulletionPreviewModel) {
    return object;
  }

  private loadNomenclatures() {
    this.nomenclatureService.getDecisionTypes().subscribe((x) => {
      this.decisionTypes = x;
    });
    this.nomenclatureService
      .getDecidingAuthoritiesForBulletins()
      .subscribe((x) => {
        this.decidingAuthorities = x;
      });
    this.nomenclatureService.getCaseTypes().subscribe((x) => {
      this.caseTypes = x;
    });
  }
}
