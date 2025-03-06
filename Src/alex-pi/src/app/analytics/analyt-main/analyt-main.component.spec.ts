import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { AnalytMainComponent } from './analyt-main.component';
import { VwEventUserUtcComponent } from '../vw-event-user-utc/vw-event-user-utc.component';
import { MaterialModule } from 'src/app/material/material.module';
import { RouterTestingModule } from '@angular/router/testing';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { VwUserHopsUtcComponent } from '../vw-user-hops-utc/vw-user-hops-utc.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CompInteractService } from 'src/app/serivce/comp-interact.service';

describe('AnalytMainComponent', () => {
  let component: AnalytMainComponent;
  let fixture: ComponentFixture<AnalytMainComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [AnalytMainComponent, VwEventUserUtcComponent, VwUserHopsUtcComponent],
    imports: [MaterialModule, RouterTestingModule, BrowserAnimationsModule],
    providers: [CompInteractService, provideHttpClient(withInterceptorsFromDi())]
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
