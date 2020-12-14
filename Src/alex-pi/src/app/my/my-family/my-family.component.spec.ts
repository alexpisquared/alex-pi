import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { MyFamilyComponent } from './my-family.component';

describe('MyFamilyComponent', () => {
  let component: MyFamilyComponent;
  let fixture: ComponentFixture<MyFamilyComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ MyFamilyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyFamilyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
