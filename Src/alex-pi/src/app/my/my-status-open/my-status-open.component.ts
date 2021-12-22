import { Component, OnInit, ViewChild, ElementRef, NgZone, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { slideBumpRightToLeftAnimation, trafficLightAnimation, slideBumpLeftToRightAnimation, flipinplaceAnimation } from 'src/app/animations';
import { GuestbookMsgsService } from 'src/app/_api/services/guestbook-msgs.service';

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
  msgCount = 26;
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

  constructor(private formBuilder: FormBuilder, private ngZone: NgZone, private svc: GuestbookMsgsService) { }

  ngOnInit() {
    setTimeout(() => {
      this.isLightOn = true;
    }, 750);

    const monthNames = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    this.startDate = monthNames[this.addDays(new Date(), 7).getMonth()];

    this.messageForm = this.formBuilder.group({
      textareaMsg: ['', Validators.required],
    });

    this.msgCount = this.randomIntFromInterval(25, 64);
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
      console.log('▄▀▄▀▄▀ onSubmit - this.messageForm.invalid');
      return;
    }

    console.log('▄▀▄▀▄▀ onSubmit - X - Posting now ...');

    this.svc.PostGuestbookMsg({ eventData: 'src: my-status-open.component.ts', message: this.messageForm.controls.textareaMsg.value }).subscribe(() => this.waitComplete());

    console.log('▄▀▄▀▄▀ onSubmit - Y - success Posting');

    this.success = true;
  }

  waitComplete(): void {
    console.log('▄▀▄▀▄▀ onSubmit - Z - success Receiving');
  }

  randomIntFromInterval(min, max) { 
    return Math.floor(Math.random() * (max - min + 1) + min)
  }
}
