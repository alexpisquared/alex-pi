import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { WebEventLogViewerComponent } from './web-event-log-viewer.component';
import { RouterTestingModule } from '@angular/router/testing';
import { MaterialModule } from '../../material/material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('WebEventLogViewerComponent', () => {
  let component: WebEventLogViewerComponent;
  let fixture: ComponentFixture<WebEventLogViewerComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [WebEventLogViewerComponent],
    imports: [RouterTestingModule, MaterialModule, BrowserAnimationsModule],
    providers: [provideHttpClient(withInterceptorsFromDi())]
}).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WebEventLogViewerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
