import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { OcrDemoComponent } from './ocr-demo.component';
import { MaterialModule } from '../material/material.module';
import { RouterTestingModule } from '@angular/router/testing';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('OcrDemoComponent', () => {
  let component: OcrDemoComponent;
  let fixture: ComponentFixture<OcrDemoComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [OcrDemoComponent],
    imports: [MaterialModule, RouterTestingModule, BrowserAnimationsModule],
    providers: [provideHttpClient(withInterceptorsFromDi())]
}).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OcrDemoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
