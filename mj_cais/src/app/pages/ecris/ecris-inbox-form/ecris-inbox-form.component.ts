import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { EcrisInboxResolverData } from "./_data/ecris-inbox.resolver";
import { EcrisInboxService } from "./_data/ecris-inbox.service";
import { EcrisInboxModel } from "./_models/ecris-inbox.model";

@Component({
  selector: "cais-ecris-inbox-form",
  templateUrl: "./ecris-inbox-form.component.html",
  styleUrls: ["./ecris-inbox-form.component.scss"],
})
export class EcrisInboxFormComponent
  extends CrudForm<
    EcrisInboxModel,
    null,
    EcrisInboxResolverData,
    EcrisInboxService
  >
  implements OnInit
{
  constructor(service: EcrisInboxService, public injector: Injector) {
    super(service, injector);
    this.setDisplayTitle("входящо съобщение");
  }

  public model: EcrisInboxModel;

  ngOnInit(): void {
    this.model = this.dbData.element as any;
    //this.model.xmlMessage = xmlBeautify(this.model.xmlMessage);
    //this.model.xmlMessageTraits = xmlBeautify(this.model.xmlMessageTraits);
    //this.formFinishedLoading.emit();
  }

  buildFormImpl(): FormGroup {
    return null;
  }

  createInputObject(object: EcrisInboxModel) {
    return object;
  }

  // response: HighlightResult;

  // onHighlight(e) {
  //   this.response = {
  //     language: e.language,
  //     r: e.r,
  //     second_best: '{...}',
  //     top: '{...}',
  //     value: '{...}'
  //   };
  // }
}
