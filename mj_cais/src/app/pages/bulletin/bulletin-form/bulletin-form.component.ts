import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { BulletinForm } from "./models/bulletin.form";
import { BulletinModel } from "./models/bulletin.model";
import { BulletinResolverData } from "./data/bulletin.resolver";
import { BulletinService } from "./data/bulletin.service";

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
    this.validateAndSave(this.fullForm);
  };
}
