import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { ContactComponent } from './contact/contact.component';
import { MyAppsComponent } from './my/my-apps/my-apps.component';
import { MyFamilyComponent } from './my/my-family/my-family.component';
import { MyClientsComponent } from './my/my-clients/my-clients.component';
import { MyProjectsComponent } from './my/my-projects/my-projects.component';
import { AboutComponent } from './about/about.component';
import { RadarComponent } from './x/radar/radar.component';
import { MyResumesComponent } from './my/my-resumes/my-resumes.component';
import { MyDesignsComponent } from './my/my-designs/my-designs.component';
import { OpenCloseComponent } from './x/open-close/open-close.component';
import { MyGeoTargetComponent } from './my/my-geo-target/my-geo-target.component';
import { WebEventLogViewerComponent } from './analytics/web-event-log-viewer/web-event-log-viewer.component';
import { AnalytMainComponent } from './analytics/analyt-main/analyt-main.component';
import { OcrDemoComponent } from './ocr-demo/ocr-demo.component';
import { UserDetailComponent } from './analytics/user-detail/user-detail.component';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent, data: { animation: 'HomePage' } },

  { path: 'my-projects', component: MyProjectsComponent, data: { animation: 'MyProjectsPage' } },
  { path: 'my-family', component: MyFamilyComponent },
  { path: 'my-clients', component: MyClientsComponent },
  { path: 'my-resumes', component: MyResumesComponent },
  { path: 'my-geo-target', component: MyGeoTargetComponent },
  { path: 'welv', component: WebEventLogViewerComponent },
  { path: 'radar', component: RadarComponent },
  { path: 'open-close', component: OpenCloseComponent },

  { path: 'usr/:id', component: UserDetailComponent },
  { path: 'about__', component: AboutComponent, data: { animation: 'AboutPage' } },
  { path: 'my-apps', component: MyAppsComponent, data: { animation: 'MyAppsPage' } },
  { path: 'ocrdemo', component: OcrDemoComponent, data: { animation: 'OcrDemoPage' } },
  { path: 'contact', component: ContactComponent, data: { animation: 'ContactPage' } },
  { path: 'designs', component: MyDesignsComponent, data: { animation: 'MyDesignsPage' } },
  { path: 'analtcs', component: AnalytMainComponent }
];

@NgModule({
  imports: [BrowserModule, BrowserAnimationsModule, RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule {}
