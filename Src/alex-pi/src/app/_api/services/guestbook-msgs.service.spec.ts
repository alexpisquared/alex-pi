import { TestBed } from '@angular/core/testing';

import { GuestbookMsgsService } from './guestbook-msgs.service';

describe('GuestbookMsgsService', () => {
  let service: GuestbookMsgsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GuestbookMsgsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
