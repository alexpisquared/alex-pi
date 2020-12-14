import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { OpenCloseComponent } from './open-close.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('OpenCloseComponent', () => {
  let component: OpenCloseComponent;
  let fixture: ComponentFixture<OpenCloseComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [BrowserAnimationsModule], // karma fix
      declarations: [OpenCloseComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OpenCloseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
