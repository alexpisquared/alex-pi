import { TestBed } from '@angular/core/testing';

import { GuestbookLoggerService } from './guestbook-logger.service';

describe('GuestbookLoggerService', () => {
  let service: GuestbookLoggerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GuestbookLoggerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
