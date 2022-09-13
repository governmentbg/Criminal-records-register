import { NgModule } from "@angular/core";
import { UserExternalRoutingModule } from "./user-external-routing.module";
import { CoreComponentModule } from "../../@core/components/core-component.module";
import { UsersExternalFormComponent } from "./users-external-form/users-external-form.component";
import { UsersExternalOverviewComponent } from "./users-external-overview/users-external-overview.component";
import { UsersExternalChangePasswordComponent } from "./users-external-change-password/users-external-change-password.component";

@NgModule({
  declarations: [UsersExternalFormComponent, UsersExternalOverviewComponent, UsersExternalChangePasswordComponent],
  imports: [CoreComponentModule, UserExternalRoutingModule],
})
export class UserExternalModule {}
