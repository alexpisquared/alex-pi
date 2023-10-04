import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TennisTimerComponent } from './tennis-timer.component';

describe('TennisTimerComponent', () => {
  let component: TennisTimerComponent;
  let fixture: ComponentFixture<TennisTimerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TennisTimerComponent]
    });
    fixture = TestBed.createComponent(TennisTimerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
