import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { BulletinAdministrationService } from "./_data/bulletin-administration.service";
import { BulletinAdministrationModel } from "./_models/bulletin-administration.model";

@Component({
  selector: "cais-bulletin-administration-form",
  templateUrl: "./bulletin-administration-form.component.html",
  styleUrls: ["./bulletin-administration-form.component.scss"],
})
export class BulletinAdministrationFormComponent
  extends CrudForm<
    BulletinAdministrationModel,
    null,
    null,
    BulletinAdministrationService
  >
  implements OnInit
{
  constructor(
    service: BulletinAdministrationService,
    public injector: Injector
  ) {
    super(service, injector);
  }
  public model: BulletinAdministrationModel;

  ngOnInit(): void {
    let id = this.activatedRoute.snapshot.params["ID"];
    this.service.get(id).subscribe((response) => {
      this.model = response;
    });
  }

  buildFormImpl(): FormGroup {
    return null;
  }

  createInputObject(object: BulletinAdministrationModel) {
    return null;
  }

  onBackIsClicked() {
    this.router.navigateByUrl('pages/bulletin-administrations');
  }
}
