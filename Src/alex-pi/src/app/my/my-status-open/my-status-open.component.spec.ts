import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { MyStatusOpenComponent } from './my-status-open.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; // karma fix

describe('MyStatusOpenComponent', () => {
  let component: MyStatusOpenComponent;
  let fixture: ComponentFixture<MyStatusOpenComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [BrowserAnimationsModule],
      declarations: [MyStatusOpenComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyStatusOpenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
