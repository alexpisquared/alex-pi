import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { NavHeaderComponent } from './nav-header.component';
import { RouterTestingModule } from '@angular/router/testing'; // Can't bind to 'formGroup' since it isn't a known property of 'form'
import { MaterialModule } from '../../material/material.module';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('NavHeaderComponent', () => {
  let component: NavHeaderComponent;
  let fixture: ComponentFixture<NavHeaderComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [NavHeaderComponent],
    imports: [MaterialModule, RouterTestingModule, BrowserAnimationsModule],
    providers: [provideHttpClient(withInterceptorsFromDi())]
}).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NavHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
