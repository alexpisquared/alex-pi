import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { VwEventUserUtcComponent } from './vw-event-user-utc.component';
import { MaterialModule } from 'src/app/material/material.module';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientModule } from '@angular/common/http';
import { CompInteractService } from 'src/app/serivce/comp-interact.service';

describe('VwEventUserUtcComponent', () => {
  let component: VwEventUserUtcComponent;
  let fixture: ComponentFixture<VwEventUserUtcComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [MaterialModule, RouterTestingModule, HttpClientModule],
      declarations: [VwEventUserUtcComponent],
      providers: [CompInteractService]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VwEventUserUtcComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
