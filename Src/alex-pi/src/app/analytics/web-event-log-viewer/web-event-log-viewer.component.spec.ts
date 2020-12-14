import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { WebEventLogViewerComponent } from './web-event-log-viewer.component';
import { RouterTestingModule } from '@angular/router/testing';
import { MaterialModule } from '../../material/material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';

describe('WebEventLogViewerComponent', () => {
  let component: WebEventLogViewerComponent;
  let fixture: ComponentFixture<WebEventLogViewerComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [RouterTestingModule, MaterialModule, BrowserAnimationsModule, HttpClientModule],
      declarations: [WebEventLogViewerComponent]
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
