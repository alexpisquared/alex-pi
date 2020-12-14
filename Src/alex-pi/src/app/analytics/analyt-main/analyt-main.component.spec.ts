import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { AnalytMainComponent } from './analyt-main.component';
import { VwEventUserUtcComponent } from '../vw-event-user-utc/vw-event-user-utc.component';
import { MaterialModule } from 'src/app/material/material.module';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientModule } from '@angular/common/http';
import { VwUserHopsUtcComponent } from '../vw-user-hops-utc/vw-user-hops-utc.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CompInteractService } from 'src/app/serivce/comp-interact.service';

describe('AnalytMainComponent', () => {
  let component: AnalytMainComponent;
  let fixture: ComponentFixture<AnalytMainComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [MaterialModule, RouterTestingModule, HttpClientModule, BrowserAnimationsModule], // karma fix
      declarations: [AnalytMainComponent, VwEventUserUtcComponent, VwUserHopsUtcComponent],
      providers: [CompInteractService]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AnalytMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
