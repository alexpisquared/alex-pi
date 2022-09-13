import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MyClientsComponent } from './my/my-clients/my-clients.component';
import { MyProjectsComponent } from './my/my-projects/my-projects.component';
import { MyFamilyComponent } from './my/my-family/my-family.component';
import { MyAppsComponent } from './my/my-apps/my-apps.component';
import { HomeComponent } from './home/home.component';
import { ContactComponent } from './contact/contact.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material/material.module';
import { AboutComponent } from './about/about.component';
import { RadarComponent } from './x/radar/radar.component';
import { MyResumesComponent } from './my/my-resumes/my-resumes.component';
import { MyDesignsComponent } from './my/my-designs/my-designs.component';
import { NavFooterComponent } from './nav/nav-footer/nav-footer.component';
import { NavHeaderComponent } from './nav/nav-header/nav-header.component';
import { NavSideLComponent } from './nav/nav-side-l/nav-side-l.component';
import { OpenCloseComponent } from './x/open-close/open-close.component';
import { MyStatusOpenComponent } from './my/my-status-open/my-status-open.component';
import { MyStatusBusyComponent } from './my/my-status-busy/my-status-busy.component';
import { MyGeoTargetComponent } from './my/my-geo-target/my-geo-target.component';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import { LocationStrategy, HashLocationStrategy } from '@angular/common'; // todo: Implementing the LocationStrategy interface now requires definition of getState().
import { WebEventLogViewerComponent } from './analytics/web-event-log-viewer/web-event-log-viewer.component';
import { AnalytMainComponent } from './analytics/analyt-main/analyt-main.component';
import { MyDesignRespComponent } from './my/my-designs/my-design-resp/my-design-resp.component';
import { MyDesignFpttComponent } from './my/my-designs/my-design-fptt/my-design-fptt.component';
import { MyDesignTrorComponent } from './my/my-designs/my-design-tror/my-design-tror.component';
import { MyDesignTytuComponent } from './my/my-designs/my-design-tytu/my-design-tytu.component';
import { MyDesignStr2Component } from './my/my-designs/my-design-str2/my-design-str2.component';
import { OcrDemoComponent } from './ocr-demo/ocr-demo.component';
import { WebsiteUserComponent } from './analytics/website-user/website-user.component';
import { VwEventUserUtcComponent } from './analytics/vw-event-user-utc/vw-event-user-utc.component';
import { VwUserHopsUtcComponent } from './analytics/vw-user-hops-utc/vw-user-hops-utc.component';
import { UserDetailComponent } from './analytics/user-detail/user-detail.component';
import { CompInteractService } from './serivce/comp-interact.service';

@NgModule({
  declarations: [
    AppComponent,
    AboutComponent,
    ContactComponent,
    HomeComponent,
    MyAppsComponent,
    MyClientsComponent,
    MyDesignsComponent,
    MyFamilyComponent,
    MyGeoTargetComponent,
    MyProjectsComponent,
    MyResumesComponent,
    MyStatusOpenComponent,
    MyStatusBusyComponent,
    NavFooterComponent,
    NavHeaderComponent,
    NavSideLComponent,
    OpenCloseComponent,
    RadarComponent,
    WebEventLogViewerComponent,
    AnalytMainComponent,
    MyDesignRespComponent,
    MyDesignFpttComponent,
    MyDesignTrorComponent,
    MyDesignTytuComponent,
    MyDesignStr2Component,
    OcrDemoComponent,
    WebsiteUserComponent,
    VwEventUserUtcComponent,
    VwUserHopsUtcComponent,
    UserDetailComponent
  ],
  imports: [
    FormsModule, // for ngModel .. which is obsolete....
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    MaterialModule,
    // viceWorkerModule.register('ngsw-worker.js', { enabled: environment.production }) // review the origins!!! as it does not work.
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production, registrationStrategy: 'registerImmediately' }) // try to fix SW - no go; need to find latest and do from scratch.
  ],
  providers: [CompInteractService, { provide: LocationStrategy, useClass: HashLocationStrategy }], // deep link fix #3 (https://stackoverflow.com/questions/38054707/angular-2-azure-deploy-refresh-error-the-resource-you-are-looking-for-has-been)

  bootstrap: [AppComponent]
})
export class AppModule { }