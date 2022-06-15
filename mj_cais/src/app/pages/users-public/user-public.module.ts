import { NgModule } from "@angular/core";
import { UserPublicRoutingModule } from "./user-public-routing.module";
import { CoreComponentModule } from "../../@core/components/core-component.module";
import { UsersCitizenOverviewComponent } from "./users-citizen-overview/users-citizen-overview.component";

@NgModule({
  declarations: [UsersCitizenOverviewComponent],
  imports: [CoreComponentModule, UserPublicRoutingModule],
})
export class UserPublicModule {}
