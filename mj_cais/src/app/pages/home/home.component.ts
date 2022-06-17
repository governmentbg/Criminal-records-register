import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { RoleNameEnum } from "../../@core/constants/role-name.enum";
import { CrudForm } from "../../@core/directives/crud-form.directive";
import { HomeResolverData } from "./_data/home.resolver";
import { HomeService } from "./_data/home.service";
import { ApplicationCountModel } from "./_models/application-count.model";
import { BulletinCountModel } from "./_models/bulletin-count.model";
import { BulletinEventCountModel } from "./_models/bulletin-event-count.model";
import { EcrisCountModel } from "./_models/ecris-count.model";
import { IsinCountModel } from "./_models/isin-count.model";
import { ObjectCountModel } from "./_models/object-count.model";

@Component({
  selector: "cais-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.scss"],
})
export class HomeComponent
  extends CrudForm<ObjectCountModel, null, HomeResolverData, HomeService>
  implements OnInit
{
  public applications: ApplicationCountModel;
  public bulletins: BulletinCountModel;
  public bulletinEvents: BulletinEventCountModel;
  public ecris: EcrisCountModel;
  public isin: IsinCountModel;

  public RoleNameEnum = RoleNameEnum;
  constructor(service: HomeService, public injector: Injector) {
    super(service, injector);
  }

  ngOnInit(): void {
    this.applications = this.dbData.applications as any;
    this.bulletins = this.dbData.bulletins as any;
    this.bulletinEvents = this.dbData.bulletinEvents as any;
    this.ecris = this.dbData.ecris as any;
    this.isin = this.dbData.isin as any;
  }

  buildFormImpl(): FormGroup {
    return null;
  }

  createInputObject(object: ObjectCountModel) {
    return null;
  }
}
