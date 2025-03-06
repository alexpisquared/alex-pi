import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { VwUserHopsUtcComponent } from './vw-user-hops-utc.component';
import { MaterialModule } from 'src/app/material/material.module';
import { RouterTestingModule } from '@angular/router/testing';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('VwUserHopsUtcComponent', () => {
  let component: VwUserHopsUtcComponent;
  let fixture: ComponentFixture<VwUserHopsUtcComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [VwUserHopsUtcComponent],
    imports: [MaterialModule, RouterTestingModule],
    providers: [provideHttpClient(withInterceptorsFromDi())]
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
