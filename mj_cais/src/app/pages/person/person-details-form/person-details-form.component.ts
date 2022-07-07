import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { PersonContextEnum } from "../../../@core/components/forms/person-form/_models/person-context-enum";
import { PersonModel } from "../../../@core/components/forms/person-form/_models/person.model";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
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
  constructor(
    service: PersonDetailsService,
    public injector: Injector,
    public bulletinsGridService: PersonBulletinGridService,
    public applicationGridService: PersonApplicationGridService,
    public fbbcGridService: PersonFbbcGridService,  
  ) {
    super(service, injector);
  }

  public personId: string;
  public model: PersonModel;
  public PersonContextEnum = PersonContextEnum;
  public tabs: any[];

  ngOnInit(): void {
    this.model = this.dbData.element as any;
    this.personId = this.model.id;
    this.bulletinsGridService.setPersonId(this.personId);
    this.applicationGridService.setPersonId(this.personId);
    this.fbbcGridService.setPersonId(this.personId);

    this.tabs = [
      {
        title: "Бюлетини",
        route: `./tab-bulletins`,
      },
      {
        title: "Свидетелства",
        route: `./tab-applications`,
      },
      {
        title: "Сведения за осъждане в чужбина",
        route: `./tab-fbbc`,
      },
    ];

    this.router.navigateByUrl(`pages/people/preview/${this.personId}/tab-bulletins`)  ;           
  }

  buildFormImpl(): FormGroup {
    return null;
  }

  createInputObject(object: PersonModel) {
    return null;
  }
}
