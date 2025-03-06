import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { VwEventUserUtcComponent } from './vw-event-user-utc.component';
import { MaterialModule } from 'src/app/material/material.module';
import { RouterTestingModule } from '@angular/router/testing';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { CompInteractService } from 'src/app/serivce/comp-interact.service';

describe('VwEventUserUtcComponent', () => {
  let component: VwEventUserUtcComponent;
  let fixture: ComponentFixture<VwEventUserUtcComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [VwEventUserUtcComponent],
    imports: [MaterialModule, RouterTestingModule],
    providers: [CompInteractService, provideHttpClient(withInterceptorsFromDi())]
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
