import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { VwUserHopsUtcComponent } from './vw-user-hops-utc.component';
import { MaterialModule } from 'src/app/material/material.module';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientModule } from '@angular/common/http';

describe('VwUserHopsUtcComponent', () => {
  let component: VwUserHopsUtcComponent;
  let fixture: ComponentFixture<VwUserHopsUtcComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [MaterialModule, RouterTestingModule, HttpClientModule],
      declarations: [VwUserHopsUtcComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VwUserHopsUtcComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
