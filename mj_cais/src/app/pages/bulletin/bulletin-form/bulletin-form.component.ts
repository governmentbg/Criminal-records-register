import { Component, Injector, OnInit, ViewChild } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { BulletinForm } from "./models/bulletin.form";
import { BulletinModel } from "./models/bulletin.model";
import { BulletinResolverData } from "./data/bulletin.resolver";
import { BulletinService } from "./data/bulletin.service";
import { BulletinOffencesFormComponent } from "./tabs/bulletin-offences-form/bulletin-offences-form.component";
import { BulletinSanctionsFormComponent } from "./tabs/bulletin-sanctions-form/bulletin-sanctions-form.component";
import { BulletinDecisionFormComponent } from "./tabs/bulletin-decision-form/bulletin-decision-form.component";

@Component({
  selector: "cais-bulletin-form",
  templateUrl: "./bulletin-form.component.html",
  styleUrls: ["./bulletin-form.component.scss"],
})
export class BulletinFormComponent
  extends CrudForm<
    BulletinModel,
    BulletinForm,
    BulletinResolverData,
    BulletinService
  >
  implements OnInit
{
  @ViewChild("bulletineOffences", {
    read: BulletinOffencesFormComponent,
  })
  
  public bulletineOffencesForm: BulletinOffencesFormComponent;

  @ViewChild("bulletineSanctions", {
    read: BulletinSanctionsFormComponent,
  })
  
  public bulletineSanctionsForm: BulletinSanctionsFormComponent;

  @ViewChild("bulletineDecisions", {
    read: BulletinDecisionFormComponent,
  })
  
  public bulletineDescitionForm: BulletinDecisionFormComponent;

  constructor(service: BulletinService, public injector: Injector) {
    super(service, injector);
    this.backUrl = "pages/bulletins";
    this.setDisplayTitle("бюлетин");
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: BulletinModel) {
    return new BulletinModel(object);
  }

  ngOnInit(): void {
    this.fullForm = new BulletinForm();
    this.fullForm.group.patchValue(this.dbData.element);
    this.formFinishedLoading.emit();
  }

  submitFunction = () => { 
    let offancesTransactions =
    this.bulletineOffencesForm.offencesGrid.transactions.getAggregatedChanges(true);

    this.fullForm.offancesTransactions.setValue(offancesTransactions);

    let sanctionsTransactions =
    this.bulletineSanctionsForm.sanctionGrid.transactions.getAggregatedChanges(true);

    this.fullForm.sanctionsTransactions.setValue(sanctionsTransactions);

    let decisionsTransactions =
    this.bulletineDescitionForm.decisionsGrid.transactions.getAggregatedChanges(true);

    this.fullForm.decisionsTransactions.setValue(decisionsTransactions);
    
    this.validateAndSave(this.fullForm);
  };
}
