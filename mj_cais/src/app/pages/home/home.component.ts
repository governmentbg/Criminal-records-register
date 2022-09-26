import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { RoleNameEnum } from "../../@core/constants/role-name.enum";
import { CrudForm } from "../../@core/directives/crud-form.directive";
import { HomeResolverData } from "./_data/home.resolver";
import { HomeService } from "./_data/home.service";
import { ApplicationCountModel } from "./_models/application-count.model";
import { BulletinCountModel } from "./_models/bulletin-count.model";
import { CentralAuthCountModel } from "./_models/central-auth-count.model";
import { InternalRequestCountModel } from "./_models/internal-request-count.model";

@Component({
  selector: "cais-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.scss"],
})
export class HomeComponent
  extends CrudForm<any, null, HomeResolverData, HomeService>
  implements OnInit
{
  public applications: ApplicationCountModel;
  public bulletins: BulletinCountModel;
  public centralAuth: CentralAuthCountModel;
  public internalRequests: InternalRequestCountModel;

  public RoleNameEnum = RoleNameEnum;
  constructor(service: HomeService, public injector: Injector) {
    super(service, injector);
  }

  ngOnInit(): void {
    this.applications = this.dbData.applications as any;
    this.bulletins = this.dbData.bulletins as any;
    this.centralAuth = this.dbData.centralAuth as any;
    this.internalRequests = this.dbData.internalRequests as any;
  }

  buildFormImpl(): FormGroup {
    return null;
  }

  createInputObject(object: any) {
    return null;
  }
}
