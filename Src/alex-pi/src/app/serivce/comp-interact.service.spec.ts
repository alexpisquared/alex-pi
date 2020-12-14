import { TestBed } from '@angular/core/testing';

import { CompInteractService } from './comp-interact.service';

describe('CompInteractService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CompInteractService = TestBed.get(CompInteractService);
    expect(service).toBeTruthy();
  });
});
