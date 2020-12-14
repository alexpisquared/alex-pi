import { TestBed } from '@angular/core/testing';

import { WebEventLoggerService } from './web-event-logger.service';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';

describe('WebEventLoggerService', () => {
  beforeEach(() => TestBed.configureTestingModule({ imports: [HttpClientModule, RouterModule, RouterTestingModule] }));

  it('should be created', () => {
    const service: WebEventLoggerService = TestBed.get(WebEventLoggerService);
    expect(service).toBeTruthy();
  });
});
