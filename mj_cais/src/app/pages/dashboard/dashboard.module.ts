import { NgModule } from "@angular/core";
import {
  NbActionsModule,
  NbButtonModule,
  NbCardModule,
  NbTabsetModule,
  NbUserModule,
  NbRadioModule,
  NbSelectModule,
  NbListModule,
  NbIconModule,
} from "@nebular/theme";

import { ThemeModule } from "../../@theme/theme.module";
import { DashboardComponent } from "./dashboard.component";
import { StatusCardComponent } from "./status-card/status-card.component";
import { ContactsComponent } from "./contacts/contacts.component";
import { TemperatureComponent } from "./temperature/temperature.component";
import { TemperatureDraggerComponent } from "./temperature/temperature-dragger/temperature-dragger.component";
import { KittenComponent } from "./kitten/kitten.component";
import { SecurityCamerasComponent } from "./security-cameras/security-cameras.component";
import { WeatherComponent } from "./weather/weather.component";
import { FormsModule } from "@angular/forms";

@NgModule({
  imports: [
    FormsModule,
    ThemeModule,
    NbCardModule,
    NbUserModule,
    NbButtonModule,
    NbTabsetModule,
    NbActionsModule,
    NbRadioModule,
    NbSelectModule,
    NbListModule,
    NbIconModule,
    NbButtonModule,
  ],
  declarations: [
    DashboardComponent,
    StatusCardComponent,
    TemperatureDraggerComponent,
    ContactsComponent,
    TemperatureComponent,
    KittenComponent,
    SecurityCamerasComponent,
    WeatherComponent,
  ],
})
export class DashboardModule {}
