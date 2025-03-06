import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { MyAppsComponent } from './my-apps.component';
import { ReactiveFormsModule } from '@angular/forms';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('MyAppsComponent', () => {
  let component: MyAppsComponent;
  let fixture: ComponentFixture<MyAppsComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [MyAppsComponent],
    imports: [ReactiveFormsModule],
    providers: [provideHttpClient(withInterceptorsFromDi())]
}).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyAppsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
