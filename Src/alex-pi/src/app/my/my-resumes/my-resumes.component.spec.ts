import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { MyResumesComponent } from './my-resumes.component';
import { MaterialModule } from '../../material/material.module';
import { RouterTestingModule } from '@angular/router/testing';

describe('MyResumesComponent', () => {
  let component: MyResumesComponent;
  let fixture: ComponentFixture<MyResumesComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [MaterialModule, RouterTestingModule],
      declarations: [MyResumesComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyResumesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
