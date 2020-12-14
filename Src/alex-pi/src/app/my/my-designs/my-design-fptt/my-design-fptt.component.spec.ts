import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { MyDesignFpttComponent } from './my-design-fptt.component';
import { MaterialModule } from 'src/app/material/material.module';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('MyDesignFpttComponent', () => {
  let component: MyDesignFpttComponent;
  let fixture: ComponentFixture<MyDesignFpttComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [MaterialModule, RouterTestingModule, HttpClientModule, BrowserAnimationsModule],
      declarations: [MyDesignFpttComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyDesignFpttComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
