import { Component, OnInit, ViewChild, ElementRef, NgZone, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { slideBumpRightToLeftAnimation, trafficLightAnimation, slideBumpLeftToRightAnimation, flipinplaceAnimation } from 'src/app/animations';

@Component({
  selector: 'app-my-status-open',
  templateUrl: './my-status-open.component.html',
  animations: [
    slideBumpRightToLeftAnimation,
    slideBumpLeftToRightAnimation,
    trafficLightAnimation,
    flipinplaceAnimation,
  ],
  styleUrls: ['./my-status-open.component.scss'],
})
export class MyStatusOpenComponent implements OnInit, OnDestroy {
  messageForm: FormGroup;
  submitted = false;
  success = false;
  biglink = 'mailto: alex.pigida@outlook.com?cc=pigida@gmail.com&subject=reaching you from alexPi.ca&body=Hi Alex,';
  h1Style = false;
  users: object;
  startDate = 'October';
  alexTinyLinkedIn = './assets/images/AlexTiny_LinkedIn.png';
  @ViewChild('canvas', { static: true }) canvas: ElementRef<HTMLCanvasElement>;

  ctx: CanvasRenderingContext2D;
  requestId;
  interval;

  isLightOn = false;
  isNumeric = false;

  toggleOnOff() {
    this.isLightOn = !this.isLightOn;
  }
  toggleAlpha() {
    this.isNumeric = !this.isNumeric;
  }

  constructor(private formBuilder: FormBuilder, private ngZone: NgZone) { }

  ngOnInit() {
    setTimeout(() => {
      this.isLightOn = true;
    }, 750);

    const monthNames = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    this.startDate = monthNames[this.addDays(new Date(), 7).getMonth()];

    this.messageForm = this.formBuilder.group({
      textareaMsg: ['', Validators.required],
    });
  }

  addDays(date, days) {
    const result = new Date(date);
    result.setDate(result.getDate() + days);
    return result;
  }
  ngOnDestroy() { }

  onDone($event) {
    this.toggleAlpha();
  }

  onSubmit() {
    this.submitted = true;
    if (this.messageForm.invalid) {
      return;
    }

    this.biglink = `mailto: alex.pigida@outlook.com?cc=pigida@gmail.com&subject=reaching you from alexPi.ca ... &body=${this.messageForm.controls.textareaMsg.value}`;

    this.success = true;
  }
}
