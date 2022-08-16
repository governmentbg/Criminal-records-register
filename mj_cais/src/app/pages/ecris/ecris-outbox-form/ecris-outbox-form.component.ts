import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { EcrisOutboxResolverData } from "./_data/ecris-outbox.resolver";
import { EcrisOutboxService } from "./_data/ecris-outbox.service";
import { EcrisOutboxModel } from "./_models/ecris.outbox.model";

@Component({
  selector: "cais-ecris-outbox-form",
  templateUrl: "./ecris-outbox-form.component.html",
  styleUrls: ["./ecris-outbox-form.component.scss"],
})
export class EcrisOutboxFormComponent
  extends CrudForm<
    EcrisOutboxModel,
    null,
    EcrisOutboxResolverData,
    EcrisOutboxService
  >
  implements OnInit
{
  constructor(service: EcrisOutboxService, public injector: Injector) {
    super(service, injector);
    this.setDisplayTitle("изходящо съобщение");
  }

  public model: EcrisOutboxModel;

  ngOnInit(): void {
    this.model = this.dbData.element as any;
  }

  buildFormImpl(): FormGroup {
    return null;
  }

  createInputObject(object: EcrisOutboxModel) {
    return object;
  }
}
