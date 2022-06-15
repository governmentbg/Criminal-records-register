import { NgModule } from '@angular/core';
import { BulletinRoutingModule } from './bulletin-routing.module';
import { BulletinPersonInfoComponent } from '../../@core/components/shared/bulletin-person-info/bulletin-person-info.component';
import { BulletinFormComponent } from './bulletin-form/bulletin-form.component';
import { BulletinDecisionFormComponent } from './bulletin-form/tabs/bulletin-decision-form/bulletin-decision-form.component';
import { BulletinDocumentFormComponent } from './bulletin-form/tabs/bulletin-documents-form/bulletin-document-form.component';
import { BulletinOffencesFormComponent } from './bulletin-form/tabs/bulletin-offences-form/bulletin-offences-form.component';
import { BulletinSanctionsFormComponent } from './bulletin-form/tabs/bulletin-sanctions-form/bulletin-sanctions-form.component';
import { BulletinStatusHistoryOverviewComponent } from './bulletin-form/tabs/bulletin-status-history-overview/bulletin-status-history-overview.component';
import { BulletinActiveOverviewComponent } from './bulletin-overview/bulletin-active-overview/bulletin-active-overview.component';
import { BulletinForDestructionOverviewComponent } from './bulletin-overview/bulletin-fordestruction-overview/bulletin-fordestruction-overview.component';
import { BulletinForRehabilitationOverviewComponent } from './bulletin-overview/bulletin-forrehabilitation-overview/bulletin-forrehabilitation-overview.component';
import { BulletinNewEissOverviewComponent } from './bulletin-overview/bulletin-neweiss-overview/bulletin-neweiss-overview.component';
import { BulletinNewOfficeOverviewComponent } from './bulletin-overview/bulletin-newoffice-overview/bulletin-newoffice-overview.component';
import { CoreComponentModule } from '../../@core/components/core-component.module';
import { OffenceCategoryDialogComponent } from './bulletin-form/tabs/bulletin-offences-form/dialog/offence-category-dialog/offence-category-dialog.component';
import { BulletinEventsOverviewComponent } from '../bulletin-events/bulletin-events-overview/bulletin-events-overview.component';
import { BulletinEventsArticleOverviewComponent } from '../bulletin-events/bulletin-events-overview/tabs/bulletin-events-article-overview/bulletin-events-article-overview.component';
import { BulletinEventsDocumentOverviewComponent } from '../bulletin-events/bulletin-events-overview/tabs/bulletin-events-document-overview/bulletin-events-document-overview.component';

@NgModule({
  declarations: [
    BulletinFormComponent,
    BulletinOffencesFormComponent,
    BulletinSanctionsFormComponent,
    BulletinDecisionFormComponent,
    BulletinDocumentFormComponent,
    BulletinNewEissOverviewComponent,
    BulletinActiveOverviewComponent,
    BulletinForDestructionOverviewComponent,
    BulletinForRehabilitationOverviewComponent,
    BulletinPersonInfoComponent,
    BulletinNewOfficeOverviewComponent,
    BulletinStatusHistoryOverviewComponent, 
    OffenceCategoryDialogComponent, 
    BulletinEventsOverviewComponent,
    BulletinEventsDocumentOverviewComponent,
    BulletinEventsArticleOverviewComponent,
  ],
  imports: [
    CoreComponentModule,
    BulletinRoutingModule,
  ]
})
export class BulletinModule { }
