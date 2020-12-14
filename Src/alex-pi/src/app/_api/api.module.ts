/* tslint:disable */
import { NgModule, ModuleWithProviders } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { ApiConfiguration, ApiConfigurationInterface } from './api-configuration';

import { OcrService } from './services/ocr.service';
import { ValuesService } from './services/values.service';
import { VwEventUserUtcsService } from './services/vw-event-user-utcs.service';
import { VwUserHopsUtcsService } from './services/vw-user-hops-utcs.service';
import { WebEventLogsService } from './services/web-event-logs.service';
import { WebsiteUsersService } from './services/website-users.service';

/**
 * Provider for all Api services, plus ApiConfiguration
 */
@NgModule({
  imports: [
    HttpClientModule
  ],
  exports: [
    HttpClientModule
  ],
  declarations: [],
  providers: [
    ApiConfiguration,
    OcrService,
    ValuesService,
    VwEventUserUtcsService,
    VwUserHopsUtcsService,
    WebEventLogsService,
    WebsiteUsersService
  ],
})
export class ApiModule {
  static forRoot(customParams: ApiConfigurationInterface): ModuleWithProviders<ApiModule> {
    return {
      ngModule: ApiModule,
      providers: [
        {
          provide: ApiConfiguration,
          useValue: {rootUrl: customParams.rootUrl}
        }
      ]
    }
  }
}
