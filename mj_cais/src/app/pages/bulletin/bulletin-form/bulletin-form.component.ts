import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { BulletinForm } from "./data/bulletin.form";
import { BulletinModel } from "./data/bulletin.model";
import { BulletinService } from "./data/bulletin.service";

@Component({
  selector: "cais-bulletin-form",
  templateUrl: "./bulletin-form.component.html",
  styleUrls: ["./bulletin-form.component.scss"],
})
export class BulletinFormComponent
  extends CrudForm<BulletinModel, BulletinForm, BulletinService>
  implements OnInit
{
  constructor(
    service: BulletinService,
    public injector: Injector //public toastr: CustomToastrService,
  ) {
    super(service, injector /*, toastr*/);
    debugger;
    this.overrideDefaultBehaviour = true;
    this.backUrl = "pages/bulletins";
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: BulletinModel) {
    return new BulletinModel(object);
  }

  ngOnInit(): void {
    debugger;
    let dbData = this.activatedRoute.snapshot.data["dbData"];
    super.ngOnInit();

    // forkJoin([
    //   this.nomenclatureService.GetGasDeviceTypes(),
    //   this.getElementData(),
    // ]).subscribe(
    //   async ([gasDeviceTypes, accessionApplication]: [
    //     BaseNomenclatureModel[],
    //     AccessionApplicationModel
    //   ]) => {
    //     this.accessionApplicationForm = new AccessionForm();

    //     if (this.isEdit()) {
    //       this.onIsPersonalChanged(accessionApplication.isPersonal);

    //       this.formFinishedLoading.emit();
    //       this.formIsLoading = false;
    //     } else {
    //       this.formIsLoading = false;
    //     }
    //   }
    // );
  }

  submitFunction = () => {
    this.validateAndSave(this.fullForm);
  };
}
