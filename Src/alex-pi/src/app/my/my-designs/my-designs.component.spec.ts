import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { MyDesignsComponent } from './my-designs.component';
import { MaterialModule } from '../../material/material.module';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MyDesignRespComponent } from './my-design-resp/my-design-resp.component';
import { MyDesignFpttComponent } from './my-design-fptt/my-design-fptt.component';
import { MyDesignTrorComponent } from './my-design-tror/my-design-tror.component';
import { MyDesignStr2Component } from './my-design-str2/my-design-str2.component';
import { MyDesignTytuComponent } from './my-design-tytu/my-design-tytu.component';

describe('MyDesignsComponent', () => {
  let component: MyDesignsComponent;
  let fixture: ComponentFixture<MyDesignsComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [MaterialModule, RouterTestingModule, HttpClientModule, BrowserAnimationsModule],
      declarations: [MyDesignsComponent, MyDesignRespComponent, MyDesignFpttComponent, MyDesignTrorComponent, MyDesignStr2Component, MyDesignTytuComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyDesignsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
