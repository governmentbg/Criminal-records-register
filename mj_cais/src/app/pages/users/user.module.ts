import { NgModule } from "@angular/core";
import { UserRoutingModule } from "./user-routing.module";
import { CoreComponentModule } from "../../@core/components/core-component.module";
import { UsersFormComponent } from "./users-form/users-form.component";
import { UsersOverviewComponent } from "./users-overview/users-overview.component";

@NgModule({
  declarations: [UsersFormComponent, UsersOverviewComponent],
  imports: [CoreComponentModule, UserRoutingModule],
})
export class UserModule {}
