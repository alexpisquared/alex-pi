import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { WebsiteUserComponent } from './website-user.component';
import { MaterialModule } from 'src/app/material/material.module';
import { RouterTestingModule } from '@angular/router/testing';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('WebsiteUserComponent', () => {
  let component: WebsiteUserComponent;
  let fixture: ComponentFixture<WebsiteUserComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [WebsiteUserComponent],
    imports: [MaterialModule, RouterTestingModule],
    providers: [provideHttpClient(withInterceptorsFromDi())]
}).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WebsiteUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
