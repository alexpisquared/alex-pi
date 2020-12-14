import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { MyDesignStr2Component } from './my-design-str2.component';
import { MaterialModule } from 'src/app/material/material.module';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('MyDesignStr2Component', () => {
  let component: MyDesignStr2Component;
  let fixture: ComponentFixture<MyDesignStr2Component>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [MaterialModule, RouterTestingModule, HttpClientModule, BrowserAnimationsModule],
      declarations: [MyDesignStr2Component]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyDesignStr2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
