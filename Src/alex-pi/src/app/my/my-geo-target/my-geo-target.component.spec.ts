import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { MyGeoTargetComponent } from './my-geo-target.component';

describe('MyGeoTargetComponent', () => {
  let component: MyGeoTargetComponent;
  let fixture: ComponentFixture<MyGeoTargetComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ MyGeoTargetComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyGeoTargetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
