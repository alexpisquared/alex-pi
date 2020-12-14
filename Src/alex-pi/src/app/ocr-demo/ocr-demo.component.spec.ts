import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { OcrDemoComponent } from './ocr-demo.component';
import { MaterialModule } from '../material/material.module';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('OcrDemoComponent', () => {
  let component: OcrDemoComponent;
  let fixture: ComponentFixture<OcrDemoComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [MaterialModule, RouterTestingModule, HttpClientModule, BrowserAnimationsModule],
      declarations: [OcrDemoComponent]
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
