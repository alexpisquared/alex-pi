import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { MyStatusBusyComponent } from './my-status-busy.component';

describe('MyStatusBusyComponent', () => {
  let component: MyStatusBusyComponent;
  let fixture: ComponentFixture<MyStatusBusyComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ MyStatusBusyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyStatusBusyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
