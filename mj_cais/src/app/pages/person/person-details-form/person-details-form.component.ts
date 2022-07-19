import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { PersonContextEnum } from "../../../@core/components/forms/person-form/_models/person-context-enum";
import { PersonModel } from "../../../@core/components/forms/person-form/_models/person.model";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { BulletinTypeConstants } from "../../bulletin/bulletin-form/_models/bulletin-type-constants";
import { PersonApplicationGridService } from "./grids/person-application-overview/_data/person-application-grid.service";
import { PersonBulletinGridService } from "./grids/person-bulletin-overview/_data/person-bulletin-grid.service";
import { PersonFbbcGridService } from "./grids/person-fbbc-overview/_data/person-fbbc-grid.service";
import { PersonDetailsResolverData } from "./_data/person-details.resolver";
import { PersonDetailsService } from "./_data/person-details.service";

@Component({
  selector: "cais-person-details-form",
  templateUrl: "./person-details-form.component.html",
  styleUrls: ["./person-details-form.component.scss"],
})
export class PersonDetailsFormComponent
  extends CrudForm<
    PersonModel,
    null,
    PersonDetailsResolverData,
    PersonDetailsService
  >
  implements OnInit
{
  public personId: string;
  public model: PersonModel;
  public PersonContextEnum = PersonContextEnum;
  public BulletinTypeConstants = BulletinTypeConstants;
  public tabs: any[];

  public bulletinsTabTitle = "Бюлетини";
  public applicationsTabTitle = "Документи на гише";
  public eApplicationsTabTitle = "Е-Свидетелства";
  public fbbcsTabTitle = "Сведения за осъждане в чужбина";
  public pidsTabTitle = "Идентификатори на лице";
  public showApplicationsTab: boolean = false;
  public showFbbcsTab: boolean = false;
  public showEApplicationsTab: boolean = false;
  public showPidsTab: boolean = false;

  constructor(
    service: PersonDetailsService,
    public injector: Injector,
    public bulletinsGridService: PersonBulletinGridService,
    public applicationGridService: PersonApplicationGridService,
    public fbbcGridService: PersonFbbcGridService,
  ) {
    super(service, injector);
  }

  ngOnInit(): void {
    this.model = this.dbData.element as any;
    this.personId = this.model.id;
  }

  buildFormImpl(): FormGroup {
    return null;
  }

  createInputObject(object: PersonModel) {
    return null;
  }

  onChangeTab(event) {
    let tabTitle = event.tabTitle;
    if (!this.showApplicationsTab) {
      this.showApplicationsTab = tabTitle == this.applicationsTabTitle;
    }

    if (!this.showFbbcsTab) {
      this.showFbbcsTab = tabTitle == this.fbbcsTabTitle;
    }

    if (!this.showEApplicationsTab) {
      this.showEApplicationsTab = tabTitle == this.eApplicationsTabTitle;
    }

    if (!this.showPidsTab) {
      this.showPidsTab = tabTitle == this.pidsTabTitle;
    }
  }
}
