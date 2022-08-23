import { NgModule } from "@angular/core";
import { PagesComponent } from "./pages.component";
import { PagesRoutingModule } from "./pages-routing.module";
import { MiscellaneousModule } from "./miscellaneous/miscellaneous.module";
import { PagesMenu } from "./pages-menu";
import { HomeComponent } from "./home/home.component";
import { PostLoginComponent } from "./auth/post-login";
import { SharedModule } from "../shared.module";
import { HelpComponent } from './help/help.component';

@NgModule({
  imports: [
    PagesRoutingModule,
    MiscellaneousModule,
    SharedModule,
  ],
  declarations: [
    PagesComponent, 
    HomeComponent,
    PostLoginComponent,
    HelpComponent, 
  ],
  providers: [PagesMenu],
})
export class PagesModule {}
