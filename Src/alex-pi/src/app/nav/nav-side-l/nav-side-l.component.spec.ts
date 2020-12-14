import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { NavSideLComponent } from './nav-side-l.component';

describe('NavSideLComponent', () => {
  let component: NavSideLComponent;
  let fixture: ComponentFixture<NavSideLComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ NavSideLComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NavSideLComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
