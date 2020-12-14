import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { MyDesignTrorComponent } from './my-design-tror.component';
import { MaterialModule } from 'src/app/material/material.module';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('MyDesignTrorComponent', () => {
  let component: MyDesignTrorComponent;
  let fixture: ComponentFixture<MyDesignTrorComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [MaterialModule, RouterTestingModule, HttpClientModule, BrowserAnimationsModule],
      declarations: [MyDesignTrorComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyDesignTrorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
