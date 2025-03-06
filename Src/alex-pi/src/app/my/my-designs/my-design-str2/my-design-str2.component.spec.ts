import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { MyDesignStr2Component } from './my-design-str2.component';
import { MaterialModule } from 'src/app/material/material.module';
import { RouterTestingModule } from '@angular/router/testing';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('MyDesignStr2Component', () => {
  let component: MyDesignStr2Component;
  let fixture: ComponentFixture<MyDesignStr2Component>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [MyDesignStr2Component],
    imports: [MaterialModule, RouterTestingModule, BrowserAnimationsModule],
    providers: [provideHttpClient(withInterceptorsFromDi())]
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
